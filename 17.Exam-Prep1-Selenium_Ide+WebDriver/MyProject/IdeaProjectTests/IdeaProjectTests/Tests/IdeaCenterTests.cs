using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaProjectTests.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        public string lastCreatedIdeaTitle;
        public string lastCreatedIdeaDescription;

        [Test, Order(1)]
        public void CreateIdeaWithInvalidDataTest()
        {
            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea("", "", "");
            createIdeaPage.CreateButton.Click();
            createIdeaPage.AssertErrorMessage();
        }

        [Test, Order(2)]
        public void CreateIdeaWithValidDataTest()
        {
            lastCreatedIdeaTitle = "Idea" + GenerateRandomString(5);
            lastCreatedIdeaDescription = "Description" + GenerateRandomString(5);

            
            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", lastCreatedIdeaDescription);
            
            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "URL not correct!");
            Assert.That(myIdeasPage.DeleteButtonLastIdea.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Descriptions do not match!");


        }

        [Test, Order(3)]
        public void ViewLastCreatedIdea()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle), "Title does not match!");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Description does not match!");

        }

        [Test, Order(4)]
        public void EditLastCreatedIdea()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string editedTitle = "Edited Title" + lastCreatedIdeaTitle;
            ideasEditPage.TitleInput.Clear();
            ideasEditPage.TitleInput.SendKeys(editedTitle);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirected!");

            myIdeasPage.ViewButtonLastIdea.Click();
            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(editedTitle), "Edited title does not match!");

            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Description does not match!");

        }


        [Test, Order(5)]
        public void EditLastCreatedIdeaDescription()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string editedDescription = "Edited Description" + lastCreatedIdeaDescription;
            ideasEditPage.DescriptionInput.Clear();
            ideasEditPage.DescriptionInput.SendKeys(editedDescription);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirected!");

            myIdeasPage.ViewButtonLastIdea.Click();
           
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(editedDescription), "Description does not match!");
            

        }

        [Test, Order(6)]
        public void DeleteLastCreatedIdea()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.DeleteButtonLastIdea.Click();

            bool isIdeaDeleted = myIdeasPage.IdeasCards.All(card => card.Text.Contains(lastCreatedIdeaDescription));

            Assert.IsFalse(isIdeaDeleted, "The idea was not deleted!");


        }

    }
}
