using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace TestGestures_My
{
    public class SwipeTests
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
                App = @"D:\ApiDemos-debug.apk"

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
        public void Swipe()
        {
            // Tap on Views
            _driver.FindElement(MobileBy.AccessibilityId("Views")).Click();
            _driver.FindElement(MobileBy.AccessibilityId("Gallery")).Click();
            _driver.FindElement(MobileBy.AccessibilityId("1. Photos")).Click();

            var firstImage = _driver.FindElements(By.ClassName("android.widget.ImageView"))[0];
            
            Actions actions = new Actions(_driver);

            var swipe = actions.ClickAndHold(firstImage)
                .MoveByOffset(-200, 0)
                .Release()
                .Build();
            swipe.Perform();

            var thirdImage = _driver.FindElements(By.ClassName("android.widget.ImageView"))[2];
            Assert.That(thirdImage, Is.Not.Null, "Third Image Is Not Visible!");

        }



       

    }
}