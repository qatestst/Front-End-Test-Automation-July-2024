

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverWaitExercise
{
    
    public class SeleniumWebDriverImplicitTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {

            driver = new ChromeDriver();
            
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            // Implicit Wait
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

            try
            {
                //Click on Buy Now Link
                driver.FindElement(By.LinkText("Buy Now")).Click();

                //Verify text
                Assert.IsTrue(driver.PageSource.Contains("keyboard"), "The product 'keyboard' was not found in the cart page");
                Console.WriteLine("Scenario completed");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }


        //2.Search non existing Product with Implicit Wait and catch NoSuchElementExeption
        [Test, Order(2)]
        public void SearchProduct_Junk_ShouldThrowNoSuchElementException()
        {
            //Fill in the search field textbox
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            //Click on search icon
            driver.FindElement(By.XPath("//input[@alt='Quick Find']")).Click();

            try
            {
                //Click on Buy Now Link
                driver.FindElement(By.LinkText("Buy Now")).Click();
                               
            }
            catch (NoSuchElementException ex)
            {
                //Verify the exception for non-existing product
                Assert.Pass("Expected NoSuchElementException was thrown");
                Console.WriteLine("Timeout - " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }




    }
}