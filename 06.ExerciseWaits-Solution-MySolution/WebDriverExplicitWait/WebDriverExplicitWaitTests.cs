using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebDriverExplicitWait
{
    public class WebDriverExplicitWaitTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
             
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        //1.Search existing Product with Implicit Wait
        [Test, Order(1)]
        public void SearchProduct_Keyboard_ShouldAddToCart()
        {
            //Fill in the search field textbox
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");

            //Click on search icon
            driver.FindElement(By.XPath("//input[@alt='Quick Find']")).Click();

            //Set the implicit wait to 0 before using explicit wait !!!
            // Note: Setting the implicit wait to zero before using explicit wait ensures that the implicit wait does not interfere with the explicit wait. This gives precise control over specific elements' wait times.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                //Create WebDriverWait object with timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                //Wait to identify thr Buy Now link using the LinkText property
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                //Set the implicit wait back to 10 seconds
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNowLink.Click();

                //Verify text
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page");
                Console.WriteLine("Scenario completed");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }

        [Test, Order(2)]

        public void SearchProduct_Junk_ShouldThrowNoSUchElementException()
        {
            //Fill in the search field textbox
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            //Click on the search icon 
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            // Set the implicit wait 0 before using explicit wait
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(0);

                //Enter Explicit wait for an object in {try-catch-finally}
            try
            {
                //Create WebDriverWait object with timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait (driver, TimeSpan.FromSeconds(10));

                //Wait to identify the 'Buy Now' link using LinkText property
                IWebElement buyNowLink = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));

                //If found fail the test as it should not exist
                buyNowLink.Click();
                Assert.Fail("The 'Buy Now' link was found for a non-existing product.");

            }
            catch (WebDriverTimeoutException)
            {
                //Expected exception for non-existing product
                Assert.Pass("Expected WebDriverTimeoutException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception:" + ex.Message);
            }
            finally
            {
                //Reset the implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }



        }
    }
}