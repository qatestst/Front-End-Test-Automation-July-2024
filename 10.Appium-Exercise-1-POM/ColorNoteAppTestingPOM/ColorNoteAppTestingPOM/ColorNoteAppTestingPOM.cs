using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteAppTestingPOM
{
    public class ColorNoteAppTestingPOM
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
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel7API",
                App = "D:\\SoftUni\\9 Front-End Test Automation - july 2024 - lections\\10.ApksFortesting\\Notepad.apk",
                PlatformVersion = "14",
            };
            
            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);
            
            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            try
            {
                var skipTutorialButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
                skipTutorialButton.Click();
            }
            catch (NoSuchElementException) 
            {
            }

        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();

        }

        [Test, Order(1)]
        public void Test_CreateNewNote()
        {
            IWebElement newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteButton.Click();

            IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createNoteText.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test1");

            IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();


            IWebElement createdNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(createdNote, Is.Not.Null, "Note was not created");
            Assert.That(createdNote.Text, Is.EqualTo("Test1"));

        }

        [Test, Order(2)]
        public void Test_EditNote()
        {
            IWebElement newNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            newNoteButton.Click();

            IWebElement createNoteText = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createNoteText.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test1");

            IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement editNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));
            editNote.Click();

            IWebElement editNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editNoteButton.Click();

            IWebElement editedNoteText = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            editedNoteText.Clear();
            editedNoteText.SendKeys("Edited Note");

            IWebElement acceptButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            acceptButton.Click();

            var result = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/view_note")).Text;

            Assert.That(result, Is.EqualTo("Edited Note"));

        }


        [Test, Order(3)]
        public void Test_DeleteNote()
        {
            _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1")).Click();
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")")).Click();
            
            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Note For Delete");

            IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();
            
            _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title")).Click();
            _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn")).Click();

            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")")).Click();
            _driver.FindElement(MobileBy.Id("android:id/button1")).Click();


            var result = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/note_list")).Text;

            Assert.That(result, Is.Empty);

        }
    }
}