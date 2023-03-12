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

namespace PhoneValidatorAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

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

        // POST api/values
        private static string CreateCsvFileContent(PhoneNumberDetails phoneNumberDetails)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Is Valid,Is Possible,Phone Type,International Format");
            csv.AppendLine($"{phoneNumberDetails.IsValid},{phoneNumberDetails.IsPossible},{phoneNumberDetails.PhoneType},{phoneNumberDetails.InternationalFormat}");
            return csv.ToString();
        }

        // POST api/values
        public HttpResponseMessage ValidatePhoneNumber([FromBody] PhoneNumberDigits phoneNumber)
        {
            try
            {
                var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber.Number, phoneNumber.CountryCode);

                if (!phoneNumberUtil.IsValidNumber(parsedPhoneNumber))
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    responseMessage.Content = new StringContent("Invalid phone number.");
                    return responseMessage;
                }

                else if (parsedPhoneNumber == null)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    responseMessage.Content = new StringContent("Phone number and country code are required.");
                    return responseMessage; 
                }

                var phoneNumberDetails = new PhoneNumberDetails
                {
                    IsValid = phoneNumberUtil.IsValidNumber(parsedPhoneNumber),
                    IsPossible = phoneNumberUtil.IsPossibleNumber(parsedPhoneNumber),
                    PhoneType = phoneNumberUtil.GetNumberType(parsedPhoneNumber).ToString(),
                    InternationalFormat = phoneNumberUtil.Format(parsedPhoneNumber, PhoneNumberFormat.INTERNATIONAL)
                };

                // Create a CSV file with phone number details
                var csvContent = CreateCsvFileContent(phoneNumberDetails);

                // Create a new response message with the CSV content
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(Encoding.ASCII.GetBytes(csvContent))
                };

                // Set the content type header to "text/csv"
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");

                // Set the content disposition header to "attachment; filename=phone_details.csv"
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "phone_details.csv"
                };

                // Return the response message
                return response;

            }

            catch (NumberParseException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest); 
                response.Content = new StringContent("Invalid phone number.");
                return response;
            }
        }


        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
