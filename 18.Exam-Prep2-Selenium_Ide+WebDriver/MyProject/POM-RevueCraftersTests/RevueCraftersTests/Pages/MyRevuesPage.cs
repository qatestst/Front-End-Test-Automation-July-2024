using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevueCraftersTests.Pages
{
    public class MyRevuesPage : BasePage
    {
        public MyRevuesPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Revue/MyRevues";

        public IWebElement lastRevue => driver.FindElements(By.XPath("//div[@class='col-md-4']")).Last();
        public IWebElement lastRevueTitle => driver.FindElements(By.XPath("//div[@class='text-muted text-center']")).Last();
        public IWebElement SearchBar => driver.FindElement(By.Id("keyword"));
        public IWebElement SearchBarButton => driver.FindElement(By.Id("search-button"));
        public IWebElement SearchedResultsRevue => driver.FindElement(By.XPath("//div[@class='col-md-4']"));
        public IWebElement SearchedResultsRevueTitle => driver.FindElements(By.XPath("//div[@class='text-muted text-center']")).Last();
        public IWebElement LastRevueEditButton => driver.FindElements(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][text()='Edit']")).Last();


        //Edit Elements
        public IWebElement TitleInputField => driver.FindElement(By.Id("form3Example1c"));
        
        public IWebElement DescriptionInputField => driver.FindElement(By.Id("form3Example4cd"));

        public IWebElement EditButtonCreateRevueForm => driver.FindElement(By.XPath("//button[@type='submit']"));


        //Delete elements
        public IWebElement DeleteLastRevueButton => driver.FindElements(By.XPath("//a[@class='btn btn-sm btn-outline-secondary'][text()='Delete']")).Last();
        public IReadOnlyCollection<IWebElement> AllRevues => driver.FindElements(By.XPath("//div[@class='col-md-4']"));

        //Search message No Revues
        public IWebElement SearchMessageNoRevues => driver.FindElement(By.XPath("//span[@class='col-12 text-muted']"));
        



        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void SearchRevue(string revueTitle)
        {
            OpenPage();
            actions.ScrollToElement(SearchBar).Perform();
            SearchBar.SendKeys(revueTitle);
            SearchBarButton.Click();

        }

        public void EditLastCreatedRevue(string title, string description)
        {
            OpenPage();
            actions.ScrollToElement(LastRevueEditButton).Perform();
            LastRevueEditButton.Click();
            actions.ScrollToElement(EditButtonCreateRevueForm).Perform();

            TitleInputField.Clear();
            TitleInputField.SendKeys(title);

            DescriptionInputField.Clear();
            DescriptionInputField.SendKeys(description);

            EditButtonCreateRevueForm.Click();

        }



    }
}
