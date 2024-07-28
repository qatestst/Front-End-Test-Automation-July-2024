using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace TestGestures_My
{
    public class DragAndDropTests
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
        public void DragAndDropUsingJavaScript()
        {
            // Tap on Views
            _driver.FindElement(MobileBy.AccessibilityId("Views")).Click();
            _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop")).Click();

            AppiumElement firstDot = _driver.FindElement(MobileBy.Id("drag_dot_1"));
            IWebElement secondDot = _driver.FindElement(MobileBy.Id("drag_dot_2"));

            //using JavaScript

            var scriptArgs = new Dictionary<string, object>
            {
                { "elementId", firstDot.Id },
                { "endX", secondDot.Location.X + (secondDot.Size.Width/2) },
                { "endY", secondDot.Location.Y + (secondDot.Size.Height/2)},
                { "speed", 2500}
            };

            _driver.ExecuteScript("mobile: dragGesture", scriptArgs);

            var droppedMessage = _driver.FindElement(By.Id("drag_result_text"));

            Assert.That(droppedMessage.Text, Is.EqualTo("Dropped!"), "The element was not dropped!");

        }

    }
}