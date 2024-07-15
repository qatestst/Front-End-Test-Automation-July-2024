using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HandleAlerts
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            // Add Implicit Wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [TearDown] 
        public void TearDown()
        { 
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void HandleBasicAlert()
        {
            // Launch the browser and open the URL
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Click on "Click fo JS Alert" button
            driver.FindElement(By.XPath("//button[contains(text(),'Click for JS Alert')]")).Click();

            // Switch to Alert
            IAlert alert = driver.SwitchTo().Alert();

            // Verify Alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected");

            // Accept the Alert
            alert.Accept();

            // Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected");


        }

        [Test, Order(2)]
        public void HandleConfirmAlert()
        {
            // Launch the browser and open the URL
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Click on "Click fo JS Alert" button
            driver.FindElement(By.XPath("//button[contains(text(),'Click for JS Confirm')]")).Click();

            // Switch to Alert
            IAlert alert = driver.SwitchTo().Alert();

            // Verify Alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            // Accept the Alert
            alert.Accept();

            // Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"), "Result message is not as expected");

            // Triger the alert again
            driver.FindElement(By.XPath("//button[contains(text(),'Click for JS Confirm')]")).Click();

            // Switch to Alert
            alert = driver.SwitchTo().Alert();

            // Verify Alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected");

            // Dismiss the Alert
            alert.Dismiss();

            // Verify the result
            resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"), "Result message is not as expected after dismissing the alert.");
        }

        [Test, Order(3)]
        public void HandlePromptAlert()
        {
            // Launch the browser and open the URL
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");

            // Click on "Click fo JS Alert" button
            driver.FindElement(By.XPath("//button[contains(text(),'Click for JS Prompt')]")).Click();

            // Switch to Alert
            IAlert alert = driver.SwitchTo().Alert();

            // Verify Alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected");

            // Send text to Alert
            string inputText = "Hello there!";
            alert.SendKeys(inputText);

            // Accept the Alert
            alert.Accept();

            // Verify the result message
            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputText), "Result message is not as expected");
        }


    }
}