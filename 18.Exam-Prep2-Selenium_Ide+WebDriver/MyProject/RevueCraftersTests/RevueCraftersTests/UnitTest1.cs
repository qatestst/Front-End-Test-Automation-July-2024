using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Text;

namespace RevueCraftersTests
{
    public class Tests
    {
        public WebDriver driver;
        private readonly static string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net";
        private Actions actions;

        private string? lastCreatedTitle;
        private string? lastCreatedDescription;

        private string? lastEditedTitle;
        private string? lastEditedDescription;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-search-engine-choice-screen");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            this.driver = new ChromeDriver(options);
            this.actions = new Actions(driver);

            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //Login
            driver.Navigate().GoToUrl("https://d3s5nxhwblsjbi.cloudfront.net/Users/Login");
            
                //Navigate to Login Form
            var loginForm = driver.FindElement(By.XPath("//form[@method='post']"));
            actions.ScrollToElement(loginForm).Perform();
                //Fill-in Login Form Email & Password              
            driver.FindElement(By.Id("form3Example3")).SendKeys("randomUser999@randomMail.com");
            driver.FindElement(By.Id("form3Example4")).SendKeys("123456");
                // Click Login Button
            driver.FindElement(By.XPath("//button[@type='submit']")).Click(); //driver.FindElement(By.CssSelector(".btn")).Click();

        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.driver.Quit();
            this.driver.Dispose();
        }

        [Test, Order(1)]
        public void CreateRevueWithInvalidDataTest()
        {
            driver.FindElement(By.XPath("//a[@href='/Revue/Create#createRevue']")).Click();
            driver.Navigate().GoToUrl("https://d3s5nxhwblsjbi.cloudfront.net/Revue/Create");

            var createRevueForm = driver.FindElement(By.XPath("//form[@class='mx-1 mx-md-4']"));
            actions.ScrollToElement(createRevueForm).Perform();

            //Fill-in Form fields
            driver.FindElement(By.Id("form3Example1c")).SendKeys("");

            driver.FindElement(By.Id("form3Example4cd")).SendKeys("");
            //Click [Create] button 
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/Create"), "User should reain on the same page!");

            //var errorMessage = driver.FindElement(By.XPath("//*[@id=\"createRevue\"]/div/div/div/div/div/div/div/div/ul/li"));
            var errorMessage = driver.FindElement(By.XPath("//*[contains(text(), 'Unable to create new Revue!')]"));

            Assert.That(errorMessage.Text, Is.EqualTo("Unable to create new Revue!"), "The error message for invalid data when creating Revue is not there");


        }

        [Test, Order(2)]
        public void CreateRandomRevueWithValidDataTest()
        {
            driver.FindElement(By.XPath("//a[@href='/Revue/Create#createRevue']")).Click();
            driver.Navigate().GoToUrl("https://d3s5nxhwblsjbi.cloudfront.net/Revue/Create");

            var createRevueForm = driver.FindElement(By.XPath("//form[@class='mx-1 mx-md-4']"));
            actions.ScrollToElement(createRevueForm).Perform();

            lastCreatedTitle = "Title" + GenerateRandomString(5);
            lastCreatedDescription = "Description" + GenerateRandomString(5);

            //Fill-in Form fields
            driver.FindElement(By.Id("form3Example1c")).SendKeys(lastCreatedTitle);

            driver.FindElement(By.Id("form3Example4cd")).SendKeys(lastCreatedDescription);
            //Click [Create] button 
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"), "User was not redirected to My Revues Page.");

                //Get only titles of the Revue cards
            ReadOnlyCollection<IWebElement> allRevues = driver.FindElements(By.XPath("//div[@class='text-muted text-center']"));
            var lastRevueTitle = allRevues.Last().Text;

            Assert.That(lastRevueTitle, Is.EqualTo(lastCreatedTitle));

        }

        [Test, Order(3)]
        public void SearchForRevueTitleTest()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");
            var searchForm = driver.FindElement(By.Id("keyword"));
            actions.ScrollToElement(searchForm).Perform();

            driver.FindElement(By.Id("keyword")).SendKeys(lastCreatedTitle);
            driver.FindElement(By.Id("search-button")).Click();

            
            var searchResultsForm = driver.FindElement(By.XPath("//div[@class='card mb-4 box-shadow']"));
            actions.ScrollToElement(searchResultsForm).Perform();


            var searchedTitleResult = driver.FindElement(By.CssSelector(".text-muted.text-center")).Text; 

            Assert.That(lastCreatedTitle, Is.EqualTo(searchedTitleResult));

        }

        [Test, Order(4)]
        public void EditLastCreatedRevueTitleTest()
        {

            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");
            var allRevuesForm = driver.FindElements(By.CssSelector(".card.mb-4")).Last();
            actions.ScrollToElement(allRevuesForm).Perform();

            //Press Edit Button on Last created Revue
            
            driver.FindElement(By.XPath($"//div[text()='{lastCreatedTitle}']/..//a[text()='Edit']")).Click();

            var editRevueForm = driver.FindElement(By.XPath("//form[@class='mx-1 mx-md-4']"));
            actions.ScrollToElement(editRevueForm).Perform();

            lastEditedTitle = "Edited Title" + GenerateRandomString(5);
            lastEditedDescription = "Edited Description" + GenerateRandomString(10);

            //Fill-in Form fields
            driver.FindElement(By.Id("form3Example1c")).Clear();
            driver.FindElement(By.Id("form3Example1c")).SendKeys(lastEditedTitle);

            driver.FindElement(By.Id("form3Example4cd")).Clear();
            driver.FindElement(By.Id("form3Example4cd")).SendKeys(lastEditedDescription);

            //Click [Create] button 
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var currentUrl = driver.Url.ToString();
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"));

            //actions.ScrollToElement(allRevuesForm).Perform();

            ////Get only titles of all Revue cards
            ReadOnlyCollection<IWebElement> allRevues = driver.FindElements(By.XPath("//div[@class='text-muted text-center']"));
            var lastRevueTitle = allRevues.Last().Text;

            Assert.That(lastRevueTitle, Is.EqualTo(lastEditedTitle));


        }


        [Test, Order(5)]
        public void DeleteLastCreatedRevueTitleTest()
        {

            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");
            var allRevuesForm = driver.FindElements(By.CssSelector(".card.mb-4")).Last();
            actions.ScrollToElement(allRevuesForm).Perform();

            ReadOnlyCollection<IWebElement> allRevues = driver.FindElements(By.XPath("//div[@class='text-muted text-center']"));
            int revuesBeforeDeletion = allRevues.Count();

            //Press Delete Button on Last created Revue

            driver.FindElement(By.XPath($"//div[text()='{lastEditedTitle}']/..//a[text()='Delete']")).Click();

            //var lastRevue = allRevues.Last();
            //actions.ScrollToElement(lastRevue).Perform();

            var currentUrl = driver.Url.ToString();
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"));

           
            ////Get only titles of all Revue cards
            allRevues = driver.FindElements(By.XPath("//div[@class='text-muted text-center']"));
            int revuesAfterDeletion = allRevues.Count();

            Assert.That(revuesBeforeDeletion, Is.GreaterThan(revuesAfterDeletion), "The number of Revues did not decrease");

            // Assert last revue title with last edited title only if the number of all revues in the list is greater than zero
            
            if (revuesAfterDeletion > 0)
            {
                var lastRevueTitle = allRevues.Last().Text;

                Assert.That(lastRevueTitle, Does.Not.EqualTo(lastEditedTitle), "The last Revue is not present on the screen.");
                //The Same IS:
                //Assert.That(lastRevueTitle, !Is.EqualTo(lastCreatedTitle), "The last Revue is not present on the screen.");
            }

                   

        }

        [Test, Order(6)]
        public void SearchForDeletedRevueTitleTest()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");
            var searchForm = driver.FindElement(By.Id("keyword"));
            actions.ScrollToElement(searchForm).Perform();

            driver.FindElement(By.Id("keyword")).SendKeys(lastEditedTitle);
            driver.FindElement(By.Id("search-button")).Click();

            string resultMessageNoRevues = driver.FindElement(By.CssSelector(".col-12.text-muted")).Text;

            Assert.That(resultMessageNoRevues, Is.EqualTo("No Revues yet!"));

        }





        public static string GenerateRandomString(int length)
        {
            char[] chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            if (length <= 0)
            {
                throw new ArgumentException("Length must be greater than zero.", nameof(length));
            }

            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }

    }
}