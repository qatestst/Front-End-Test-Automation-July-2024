using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace HandleWindows
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void HandleMultipleWindowsTests()
        {
            driver = new ChromeDriver();

        }

        [TearDown]
        public void Dispose() 
        { 
            driver.Quit();
            driver.Dispose();
        }
        

        [Test, Order(1)]
        public void HandleMultipleWindows()
        {
            //Launch the browser and open the URL 
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");

            // CLick on 'Click Here' link to open a new window
            driver.FindElement(By.LinkText("Click Here")).Click();

            // Get all window handles
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Ensure there ate at least two windows open
            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open");

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            // Verify the content of the new wondow
            string newWindowContent = driver.PageSource;
            Assert.IsTrue(newWindowContent.Contains("New Window"), "The content of the new window is not as expected");

            // Log the content of the new window
            string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "Window handle for new window: " + driver.CurrentWindowHandle + "\n\n");

            // CLose the new window
            driver.Close();

            // Switch back to the original window
            driver.SwitchTo().Window(windowHandles[0]);

            // Verify the content og the riginal window
            string originalWindowContent = driver.PageSource;
            Assert.IsTrue(originalWindowContent.Contains("Opening a new window"), "The content of the original window is not as expected");

            // Log the content of the original window
            File.AppendAllText(path, "Window handle for original window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");
        }

        [Test, Order(2)]
        public void HandleNoSuchWindowException()
        {
            //Launch the browser and open the URL 
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");

            // CLick on 'Click Here' link to open a new window
            driver.FindElement(By.LinkText("Click Here")).Click();

            // Get all window handles
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            // CLose the new window
            driver.Close();

            try
            {
                //Attemp to switch back to closed window
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                // Log the exception
                string path = Path.Combine(Directory.GetCurrentDirectory() + "windows.txt");
                File.AppendAllText(path, "NoSuchWindowException caught: " + ex.Message + "\n\n");
                Assert.Pass("NoSuchWindowException was correctly handled.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected exception wah thrown: " + ex.Message);
            }
            finally
            {
                // Switch back to the original window
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }
    }
}