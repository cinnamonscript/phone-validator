using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.IO;
using PhoneNumbers;
using PhoneValidatorAPI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;

namespace PhoneValidatorAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values

        private readonly List<Country> _supportedCountries;
        public ValuesController()
        {
            _supportedCountries = new List<Country>
        {
            new Country { Name = "Australia", Code = "AU" },
            new Country { Name = "United States", Code = "US" },
            new Country { Name = "Canada", Code = "CA" }
        };
        }

        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        // GET api/values/5
        public IEnumerable<Country> GetSupportedCountries(int id)
        {
            return _supportedCountries;
        }

        // GET 
        [HttpGet]
        public IHttpActionResult DownloadCsv(string downloadId)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + downloadId + ".csv");

            if (!File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = File.ReadAllBytes(filePath);

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var content = new ByteArrayContent(fileBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = $"phone_details_{downloadId}.csv"
            };
            result.Content = content;

            return ResponseMessage(result);
        }


        // POST api/values
        private static string CreateCsvFileContent(PhoneNumberDetails phoneNumberDetails)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Is Valid,Is Possible,Phone Type,International Format");
            csv.AppendLine($"{phoneNumberDetails.IsValid},{phoneNumberDetails.IsPossible},{phoneNumberDetails.PhoneType},{phoneNumberDetails.InternationalFormat}");
            return csv.ToString();
        }

        private static string GetFormattedPhoneType(string phoneType)
        {
            string[] words = phoneType.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i][0] + words[i].Substring(1).ToLower();
            }
            return string.Join(" ", words);
        }

        // POST api/values
        public IHttpActionResult ValidatePhoneNumber([FromBody] PhoneNumberDigits phoneNumber)
        {
            try
            {
                var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber.Number, phoneNumber.CountryCode);

                var phoneNumberDetails = new PhoneNumberDetails
                {
                    IsValid = phoneNumberUtil.IsValidNumber(parsedPhoneNumber),
                    IsPossible = phoneNumberUtil.IsPossibleNumber(parsedPhoneNumber),
                    PhoneType = GetFormattedPhoneType(phoneNumberUtil.GetNumberType(parsedPhoneNumber).ToString()),
                    InternationalFormat = phoneNumberUtil.Format(parsedPhoneNumber, PhoneNumberFormat.INTERNATIONAL)
                };

                if (!phoneNumberUtil.IsValidNumber(parsedPhoneNumber))
                {
                    return BadRequest("Invalid phone number.");
                }

                else if (parsedPhoneNumber == null)
                {
                    return BadRequest("Phone number and country code are required.");
                }

                var csvContent = CreateCsvFileContent(phoneNumberDetails);

                // Generate a unique download ID
                var downloadId = Guid.NewGuid().ToString();

                // Save the CSV content to a file
                var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + downloadId + ".csv");
                File.WriteAllText(filePath, csvContent);

                // Generate the download URL
                var downloadUrl = "/api/downloadcsv/" + downloadId;

                // Create an object that contains both the CSV content and the phone number details
                var responseData = new
                {
                    CsvContent = csvContent,
                    PhoneNumberDetails = phoneNumberDetails,
                    DownloadUrl = downloadUrl
                };

                // Serialize the response object to JSON
                var jsonResponse = JsonConvert.SerializeObject(responseData);

                // Return the phone number details and CSV file
                return Ok(responseData);

                //return Ok(JsonConvert.SerializeObject(phoneNumberDetails));

            }

            catch (NumberParseException)
            {
                return BadRequest("Invalid phone number.");
            }
        }

    }
}
