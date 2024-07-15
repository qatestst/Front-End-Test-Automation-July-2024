using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace iFrameTesting
{
    public class iFrameTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order (1)]
        public void TestFrameByIndex()
        {
            // Go to webpage
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";

            // Create implcit wait 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Wait until the iframe is available and switch to it by finding the first iframe
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));


            // Click the dropdown button
            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropdown")));
            dropdownButton.Click();

            // Select the links inside the dropdown menu
            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            // Verify and print the link text
            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();

        }

        [Test, Order(2)]
        public void TestFrameById()
        {
            // Go to webpage
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";

            // Create implcit wait 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait until the iframe is available and switch to it by ID
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("result")));


            // Click the dropdown button
            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropdown")));
            dropdownButton.Click();

            // Select the links inside the dropdown menu
            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            // Verify and print the link text
            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();

        }


        [Test, Order(3)]
        public void TestFrameByElement()
        {
            // Go to webpage
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";

            // Create implcit wait 10 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Locate the frame element
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));
            // Switch to the frame by web element
            driver.SwitchTo().Frame(frameElement);

            // Click the dropdown button
            var dropdownButton = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropdown")));
            dropdownButton.Click();

            // Select the links inside the dropdown menu
            var dropdownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            // Verify and print the link text
            foreach (var link in dropdownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected");
            }

            driver.SwitchTo().DefaultContent();

        }
    }
}