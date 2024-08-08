﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaProjectTests.Pages
{
    public class CreateIdeaPage : BasePage
    {
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement ImageInput => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement CreateButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));
        public IWebElement MainMessage => driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li"));
        public IWebElement TitleErrorMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']//li"))[0];
        public IWebElement DescriptionErrorMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']//li"))[1];

        public void CreateIdea(string title, string imageUrl,string description)
        {
            TitleInput.SendKeys(title);
            ImageInput.SendKeys(imageUrl);
            DescriptionInput.SendKeys(description);
            CreateButton.Click();

        }

        public void AssertErrorMessage()
        {
            Assert.True(MainMessage.Text.Equals("Unable to create new Idea!"), "Main message is not as expected!");
            Assert.True(TitleErrorMessage.Text.Equals("The title field is required."), "Title message is not as expected!");
            Assert.True(DescriptionErrorMessage.Text.Equals("The Description field is required."), "Description message is not as expected!");

        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }




    }
}
