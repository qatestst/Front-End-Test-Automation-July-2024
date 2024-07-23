using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AppiumTestsExercise1
{
    public class SummatorAppPagePOMTests
    {
        protected AndroidDriver _driver;
        protected AppiumLocalService _appiumLocalService;

        protected SummatorPage _summatorPage;

        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions()
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel7API",
                App = "D:\\SoftUni\\9 Front-End Test Automation - july 2024 - lections\\10.ApksFortesting\\com.example.androidappsummator.apk",
                PlatformVersion = "14",
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
            _summatorPage = new SummatorPage(_driver);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
            _appiumLocalService?.Dispose();
        }


        [Test]
        public void TestWithValidData()
        {
            var result = _summatorPage.Calculate("1", "2");
            Assert.That(result, Is.EqualTo("3"));
        }

        [Test]
        public void TestWithInvalidDataFillOnlyFirstField()
        {
            _summatorPage.ClearFields();
            _summatorPage.calcButton.Click();

            Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithInvalidDataFillOnlySecondField()
        {
            _summatorPage.ClearFields();
            _summatorPage.field2.SendKeys("3");
            Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
        }


        [Test]
        public void TestWithInvalidDataFirstAndSecondField()
        {
            _summatorPage.ClearFields();
            _summatorPage.field1.SendKeys("3");
            Assert.That(_summatorPage.resultField.Text, Is.EqualTo("error"));
        }

        [TestCase("10", "10", "20")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "1000", "1000")]
        [TestCase("10.9", "10.1", "21.0")]
        [TestCase("-10", "10", "0")]

        public void TestWithValidData_Parametrized(string input1, string input2, string expectedResult)
        {
            var result = _summatorPage.Calculate(input1,input2);
                         
            Assert.That(result, Is.EqualTo(expectedResult));
        }



    }
}
