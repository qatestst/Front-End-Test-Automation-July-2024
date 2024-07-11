using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTable
{
    public class WebTableTests
    {
        WebDriver driver;

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

        [Test]
        public void WorkingWithTableElements()
        {
            //locate the table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            // Find all rows in table
            //driver.FindElements(By.XPath("//div[@class='contentText']//table//tr"))
            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

            //Create CSV (comma separated values) file
            string path = System.IO.Directory.GetCurrentDirectory() + "/productInformation.csv";

            if (File.Exists(path))
            {
                File.Delete(path);
            };

            foreach (IWebElement tableRow in tableRows) 
            {
                ReadOnlyCollection<IWebElement> tableData = tableRow.FindElements(By.XPath(".//td"));
                foreach (var item in tableData)
                {
                    string data = item.Text;
                    string[] productInfo = data.Split("\n");

                    File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");
                }
            }

            Assert.IsTrue(File.Exists(path));
            Assert.IsTrue(new FileInfo(path).Length > 0);
        }
    }
}