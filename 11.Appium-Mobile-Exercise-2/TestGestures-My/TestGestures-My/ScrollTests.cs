using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace TestGestures_My
{
    public class ScrollTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;

        
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
                AutomationName = "UiAutomator2",
                DeviceName = "Pixel7API",
                PlatformVersion = "14",
                App = @"D:\\SoftUni\\Front-End-Test-Automation-July-2024\\11.Appium-Mobile-Exercise-2\\ApiDemos-debug.apk"
                               
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }



        
        [Test]
        public void ScrollTest()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();
            
            ScrollToText("Lists");

            IWebElement listsButton = _driver.FindElement(MobileBy.AccessibilityId("Lists"));

            Assert.That(listsButton, Is.Not.Null, "The 'Lists' element is not visible!");

            listsButton.Click();

            IWebElement photosButton = _driver.FindElement(MobileBy.AccessibilityId("08. Photos"));
            Assert.That(photosButton, Is.Not.Null, "Photos button does not exist!");


        }

        
        //Write Methods always after Tests - this is the good practice!
        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator(
                $"new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"{text}\"))"));
        }


    }
}