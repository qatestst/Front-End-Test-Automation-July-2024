using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevueCraftersTests.Tests
{

    public class RevueCraftersTests : BaseTests
    {

        public string lastCreatedRevueTitle;
        public string lastCreatedRevueDescription;

        public string lastEditedRevueTitle;
        public string lastEditedRevueDescription;

        [Test,Order(1)]
        public void CreateRevueWithInvalidDataTest()
        {
            createRevuePage.CreateNewRevue("", "");
            Assert.That(driver.Url, Is.EqualTo(createRevuePage.Url));
            Assert.That(createRevuePage.ErrorMessageCreateRevue.Text.Trim(), Is.EqualTo("Unable to create new Revue!"), "Error message not expected!");

        }

        [Test, Order(2)]
        public void CreateRevueWithValidDataTest()
        {
            lastCreatedRevueTitle = GenerateRandomTitle();
            lastCreatedRevueDescription = GenerateRandomDescription();

            createRevuePage.CreateNewRevue(lastCreatedRevueTitle, lastCreatedRevueDescription);

            Assert.That(driver.Url, Is.EqualTo(myRevuePage.Url));

            actions.ScrollToElement(myRevuePage.lastRevueTitle).Perform();
            Assert.That(lastCreatedRevueTitle, Is.EqualTo(myRevuePage.lastRevueTitle.Text.Trim()));

            myRevuePage.SearchRevue(lastCreatedRevueTitle);
            actions.ScrollToElement(myRevuePage.SearchedResultsRevueTitle).Perform();

            Assert.That(myRevuePage.SearchedResultsRevueTitle.Text.Trim(), Is.EqualTo(lastCreatedRevueTitle));

        }

        [Test, Order(3)]
        public void EditLastCreatedRevueTitleTest()
        {
            lastEditedRevueTitle = "Edited" + GenerateRandomTitle();
            lastEditedRevueDescription = "Edited" + GenerateRandomDescription();
            myRevuePage.EditLastCreatedRevue(lastEditedRevueTitle, lastEditedRevueDescription);

            Assert.That(driver.Url, Is.EqualTo(myRevuePage.Url));

            actions.ScrollToElement(myRevuePage.lastRevueTitle).Perform();
            Assert.That(myRevuePage.lastRevueTitle.Text.Trim(), Is.EqualTo(lastEditedRevueTitle));

        }

        [Test, Order(4)]
        public void DeletelastCreatedRevueTitleTest()
        {
            int numberOfAllRevuesBeforeDeletion = myRevuePage.AllRevues.Count;
            
            myRevuePage.OpenPage();
            actions.ScrollToElement(myRevuePage.DeleteLastRevueButton).Perform();
            myRevuePage.DeleteLastRevueButton.Click();
                        
            Assert.That(driver.Url, Is.EqualTo(myRevuePage.Url));

            string lastRevue = myRevuePage.lastRevueTitle.Text;

            Assert.That(lastRevue, !Is.EqualTo(lastEditedRevueTitle));

            if (numberOfAllRevuesBeforeDeletion > 0)
            {
                int numberOfAllRevuesAfterDeletion = myRevuePage.AllRevues.Count;
                Assert.That(numberOfAllRevuesBeforeDeletion, Is.GreaterThan(numberOfAllRevuesAfterDeletion));
            }
            else
            {
                Assert.That(myRevuePage.AllRevues.Count, Is.EqualTo(0));
            }
            


        }

        [Test, Order(5)]
        public void SearchForDeletedRevueTitleTest()
        {
            myRevuePage.OpenPage();
            actions.ScrollToElement(myRevuePage.SearchBar).Perform();
            myRevuePage.SearchRevue(lastEditedRevueTitle);
            actions.ScrollToElement(myRevuePage.SearchMessageNoRevues).Perform();

            Assert.That(myRevuePage.SearchMessageNoRevues.Text.Trim(), Is.EqualTo("No Revues yet!"));
        }
    }
}
