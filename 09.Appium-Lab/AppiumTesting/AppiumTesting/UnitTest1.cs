using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Internal;

namespace AppiumTesting
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;

        [SetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder()
                .WithIPAddress ("127.0.0.1")
                .UsingPort(4723)
                .Build();

            AppiumOptions options = new AppiumOptions();
            options.App = @"D:\SoftUni\9 Front-End Test Automation - july 2024 - lections\09.AppForTesting\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.DeviceName = "Pixel7API";
            options.AutomationName = "UIAutomator2";

            driver = new AndroidDriver(service, options);
        }

        [TearDown]
        public void TearDown() 
        {
            driver?.Dispose();
        }

        [Test]
        public void TestValidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("5");

            var calculation = driver.FindElement(MobileBy.ClassName("android.widget.Button"));

            calculation.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum")).Text;

            Assert.That(result, Is.EqualTo("7"), "summation ius incorrect");

            
        }

        [Test]
        public void TestValidSummationTenTimes()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            

            var calculation = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
                        
            for (int i = 1; i < 10; i++)
            {
                firstInput.SendKeys(i.ToString());
                secondInput.SendKeys(i.ToString());

                calculation.Click();

                var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum")).Text;

                Assert.That(result, Is.EqualTo((i+i).ToString()), "summation ius incorrect");

                firstInput.Clear();
                secondInput.Clear();
            }
        }

        [Test]
        public void TestInvalidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("2");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("string");

            var calculation = driver.FindElement(MobileBy.ClassName("android.widget.Button"));

            calculation.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"), "summation is incorrect");


        }
    }
}