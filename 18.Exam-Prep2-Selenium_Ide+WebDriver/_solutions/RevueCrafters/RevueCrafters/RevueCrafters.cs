using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace RevueCrafters

{
    [TestFixture]
    public class RevueCraftersTests
    {
        protected IWebDriver driver;
        private static readonly string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net/";
        private static string? lastCreatedRevueTitle;
        private static string? lastCreatedRevueDescription;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Log in to the application
            driver.Navigate().GoToUrl($"{BaseUrl}Users/Login#loginForm");
            var loginForm = driver.FindElement(By.Id("loginForm"));

            // Scroll to the login form using Actions class
            Actions actions = new Actions(driver);
            actions.MoveToElement(loginForm).Perform();

            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("form3Example3")).SendKeys("revue@revue.com");
            driver.FindElement(By.Id("form3Example4")).SendKeys("123456");
            driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-block.mb-4")).Click();
        }

        [Test, Order(1)]
        public void CreateRevueWithInvalidDataTest()
        {
            string invalidRevueTitle = "";
            string invalidRevueDescription = "";

            driver.Navigate().GoToUrl($"{BaseUrl}Revue/Create#createRevue");

            var createForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(createForm).Perform();

            driver.FindElement(By.Id("form3Example1c")).SendKeys(invalidRevueTitle);
            driver.FindElement(By.Id("form3Example4cd")).SendKeys(invalidRevueDescription);
            driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg")).Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/Create#createRevue"), "The page should remain on the creation page with invalid data.");

            var mainErrorMessage = driver.FindElement(By.CssSelector(".validation-summary-errors li"));
            Assert.That(mainErrorMessage.Text.Trim(), Is.EqualTo("Unable to create new Revue!"), "The main error message is not displayed as expected.");
        }

        [Test, Order(2)]
        public void CreateRandomRevueTest()
        {
            lastCreatedRevueTitle = "Revue N: " + GenerateRandomString(5);
            lastCreatedRevueDescription = "Revue Description: " + GenerateRandomString(10);

            driver.Navigate().GoToUrl($"{BaseUrl}Revue/Create#createRevue");

            var createForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(createForm).Perform();

            driver.FindElement(By.Id("form3Example1c")).SendKeys(lastCreatedRevueTitle);
            driver.FindElement(By.Id("form3Example4cd")).SendKeys(lastCreatedRevueDescription);
            driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg")).Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues#createRevue"), "The page should redirect to My Revues.");

            var revues = driver.FindElements(By.CssSelector("div.card.mb-4.box-shadow"));
            var lastRevueTitleElement = revues.Last().FindElement(By.CssSelector("div.text-muted.text-center"));

            string actualRevueTitle = lastRevueTitleElement.Text.Trim();
            Assert.That(actualRevueTitle, Is.EqualTo(lastCreatedRevueTitle), "The last created revue title does not match the expected value.");
        }

        [Test, Order(3)]
        public void SearchForRevueTest()
        {

            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");

            var searchField = driver.FindElement(By.CssSelector(".input-group.mb-xl-5"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(searchField).Perform();

            var searchInput = driver.FindElement(By.Name("keyword"));
            searchInput.SendKeys(lastCreatedRevueTitle);
            var searchButton = driver.FindElement(By.Id("search-button"));
            searchButton.Click();

            var revueTitle = driver.FindElement(By.CssSelector(".text-muted.text-center")).Text;
            
            // Assert that the revue name matches the searched name
            Assert.That(revueTitle, Is.EqualTo(lastCreatedRevueTitle), "The revue title in the search results does not match the searched name.");

        }

        [Test, Order(4)]
        public void EditLastCreatedRevueTitleTest()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues#myRevues");

            var revues = driver.FindElements(By.CssSelector("div.card.mb-4.box-shadow"));
            Assert.IsTrue(revues.Count > 0, "No revues were found on the page.");

            var lastRevueElement = revues.Last();
            Actions actions = new Actions(driver);
            actions.MoveToElement(lastRevueElement).Perform();

            var editButton = lastRevueElement.FindElement(By.CssSelector("a[href*='/Revue/Edit']"));
            editButton.Click();

            var editForm = driver.FindElement(By.CssSelector("div.card-body.p-md-5"));
            actions.MoveToElement(editForm).Perform();

            var titleInput = driver.FindElement(By.Id("form3Example1c"));
            string newTitle = "Changed Title - " + lastCreatedRevueTitle;
            titleInput.Clear();
            titleInput.SendKeys(newTitle);

            var saveChangesButton = driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-lg"));
            saveChangesButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues"), "The page should redirect to My Revues.");

            revues = driver.FindElements(By.CssSelector("div.card.mb-4.box-shadow"));
            var lastRevueTitleElement = revues.Last().FindElement(By.CssSelector("div.text-muted.text-center"));

            string actualRevueTitle = lastRevueTitleElement.Text.Trim();
            Assert.That(actualRevueTitle, Is.EqualTo(newTitle), "The last created revue title does not match the expected value.");
        }

       
        [Test, Order(5)]
        public void DeleteLastCreatedRevueTitleTest()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues#myRevues");

            var revues = driver.FindElements(By.CssSelector("div.card.mb-4.box-shadow"));
            Assert.IsTrue(revues.Count > 0, "No revues were found on the page.");

            var lastRevueElement = revues.Last();
            Actions actions = new Actions(driver);
            actions.MoveToElement(lastRevueElement).Perform();

            var editButton = lastRevueElement.FindElement(By.CssSelector("a[href*='/Revue/Delete']"));
            editButton.Click();

            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}Revue/MyRevues"), "The page should be My Revues.");

            revues = driver.FindElements(By.CssSelector("div.card.mb-4.box-shadow"));
            var lastRevueTitleElement = revues.Last().FindElement(By.CssSelector("div.text-muted.text-center"));

            string actualRevueTitle = lastRevueTitleElement.Text.Trim();
            Assert.That(actualRevueTitle, Is.Not.EqualTo(lastCreatedRevueTitle), "The last created revue title does not match the expected value.");
        }


        [Test, Order(6)]
        public void SearchForDeletedRevueTest()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}Revue/MyRevues");
            var searchField = driver.FindElement(By.CssSelector(".input-group.mb-xl-5"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(searchField).Perform();

            var searchInput = driver.FindElement(By.Name("keyword"));
            searchInput.SendKeys(lastCreatedRevueTitle);
            var searchButton = driver.FindElement(By.Id("search-button"));
            searchButton.Click();

            // Assert that the message "No Revues yet!" is displayed
            var noRevuesMessage = driver.FindElement(By.CssSelector(".col-12.text-muted"));
            Assert.That(noRevuesMessage.Text.Trim(), Is.EqualTo("No Revues yet!"), "The 'No Revues yet!' message is not displayed as expected.");

           }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}