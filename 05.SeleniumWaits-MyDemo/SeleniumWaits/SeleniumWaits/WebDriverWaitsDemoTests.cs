using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebDriverWaitsDemo
{
    public class WebDriverWaitsDemoTests
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

        [Test]
        public void RedBoxInteractionTest()
        {
            driver.Navigate().GoToUrl("https://selenium.dev/selenium/web/dynamic.html");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement addButton =  driver.FindElement(By.XPath("//input[@id='adder']"));
            addButton.Click();

            IWebElement redBox = driver.FindElement(By.XPath("//div[@id='box0']"));

            Assert.True(redBox.Displayed);


        }

        [Test]
        public void RevealInputButtonTest()
        {
            driver.Navigate().GoToUrl("https://selenium.dev/selenium/web/dynamic.html");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IWebElement addButton = driver.FindElement(By.XPath("//input[@id='reveal']"));
            addButton.Click();

            IWebElement inputField = driver.FindElement(By.XPath("//input[@id='revealed']"));

            Assert.True(inputField.Enabled);
            
        }

        [Test]
        public void ExplicitWait_ElementCreatedButNotVisible_Test()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            //Add explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement finish = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));

            Assert.True(finish.Displayed);


        }

        [Test]
        public void ImplicitWait_Test()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
           
            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            //Add explicit wait
            var finish = driver.FindElement(By.XPath("//div[@class='example']//div[@id='finish']"));

            Assert.True(finish.Displayed);
        }

        [Test]
        public void PageLoadTimeout()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);

            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

            var startButton = driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button"));

            Assert.True(startButton.Displayed);

        }

        [Test]
        public void JavaScriptTiomeoutsTest()
        {
            //set JS timeouts to 60 seconds, because default JS timeouts is 30 seconds
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);

            //create dalay script 45 seconds but default JS timeout is 30 seconds
            string script = @"
                const start = new Date().getTime();
                const delay = 45000;
                while(new Date().getTime() < start + delay)
                {
                        // do something
                }
             console.log(""45 seconds of execution"")     
            ";


            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript(script);
                      
            
        }


        [Test]

        public void FluentWait_ElementCreatedButNotVisible() 
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);

            IWebElement finishDiv = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));

            Assert.True(finishDiv.Displayed);

        }

        //TO DO TESTS..... FLuentException


    }
}