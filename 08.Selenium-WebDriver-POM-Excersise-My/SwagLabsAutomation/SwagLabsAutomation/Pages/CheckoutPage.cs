using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Pages
{
    public class CheckoutPage : BasePage
    {
        // Create locators
        private readonly By firstNameField = By.Id("first-name");
        private readonly By lastNameField = By.Id("last-name");
        private readonly By postalCodeField = By.Id("postal-code");
        private readonly By continueButton = By.CssSelector(".cart_button");
        private readonly By finishButton = By.Id("finish");
        private readonly By completeHeader = By.CssSelector(".complete-header");

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;   
        }

        //Create Methods

        public void EnterFirstName(string firstName)
        {
            Type(firstNameField, firstName);
        }

        public void EnterLastName(string lastName)
        {
            Type(lastNameField, lastName);
        }

        public void EnterPostalCode(string postalCode)
        {
            Type(postalCodeField, postalCode);
        }

        public void ClickContinue()
        {
            Click(continueButton);
        }

        public void ClickFinish()
        {
            Click(finishButton);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") || driver.Url.Contains("checkout-step-two.html");
        }

        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
