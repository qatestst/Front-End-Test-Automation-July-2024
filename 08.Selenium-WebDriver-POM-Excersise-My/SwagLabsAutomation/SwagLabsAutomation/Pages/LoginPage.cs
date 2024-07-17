using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By userNameField = By.XPath("//input[@id='user-name']");
        private readonly By passwordField = By.XPath("//input[@id='password']");
        private readonly By loginButton = By.XPath("//input[@id='login-button']");
        private readonly By errorMessage = By.XPath("//div[@class='error-message-container error']//h3");


        public void FillUserName(string username)
        { 
            Type(userNameField, username);        
        }

        public void FillPassword(string password)
        { 
            Type(passwordField, password);
        }

        public void ClickLoginButton()
        {
            Click(loginButton);
        }

        public string GetErrorMessage()
        {
            return GetText(errorMessage);
        }

        public void LoginUser(string username, string password)
        {
            FillUserName(username);
            FillPassword(password);
            ClickLoginButton();
        }



        public LoginPage(IWebDriver driver) : base(driver)
        {
            
        }
    }
}
