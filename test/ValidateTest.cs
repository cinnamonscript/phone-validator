using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit;

namespace PhoneValidatorAPI.test
{
    public class ValidateTest
    {
        [Test]
        public void ShouldFormatPhoneNumberCorrectlyForAustralia()
        {
            // Arrange
            var countryCodeSelect = new HtmlSelect();
            countryCodeSelect.Attributes.Add("id", "country-code-input");
            var option = new ListItem("Australia", "AU");
            countryCodeSelect.Items.Add(option);

            var phoneNumberInput = new HtmlInputText();
            phoneNumberInput.Attributes.Add("id", "phone-number-input");
            phoneNumberInput.Value = "0412345678";

            var page = new Page();
            page.Controls.Add(countryCodeSelect);
            page.Controls.Add(phoneNumberInput);

            // Act
            ValidatePhoneNumber();

            // Assert
            Assert.AreEqual("+61412345678", phoneNumberInput.Value);
        }
    }
}