using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Tests
{
    public class MovieCatalogueTests : BaseTest
    {
        string lastCreatedMovieTitle;
        string lastCreatedMovieDescription;

        string lastEditedMovieTitle;
        string lastEditedMovieDescription;




        [Test, Order(1)]
        public void AddMovieWithoutTitleTest()
        {
            addMoviePage.AddMovie("", "description");
            Assert.That(addMoviePage.ErrorMessageAddMovie.Text.Trim(), Is.EqualTo("The Title field is required."));

        }

        [Test, Order(2)]
        public void AddMovieWithoutDescriptionTest()
        {
            addMoviePage.AddMovie("title", "");
            Assert.That(addMoviePage.ErrorMessageAddMovie.Text.Trim(), Is.EqualTo("The Description field is required."));
        }

        [Test, Order(3)]
        public void AddMovieWithRandomTitleTest()
        {
            lastCreatedMovieTitle = GenerateRandomTitle();
            lastCreatedMovieDescription = GenerateRandomDescription();

            addMoviePage.AddMovie(lastCreatedMovieTitle, lastCreatedMovieDescription);
            allMoviesPage.LastPage.Click();

            Assert.That(allMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastCreatedMovieTitle));
        }

        [Test, Order(4)]
        public void EditLastAddedMovieTest()
        {
            lastEditedMovieTitle = "EDITED" + GenerateRandomTitle(); 
            lastEditedMovieDescription = "EDITED" + GenerateRandomTitle(); 

            allMoviesPage.EditMovie(lastEditedMovieTitle, lastEditedMovieDescription);
            Assert.That(allMoviesPage.MessageEditMovieForm.Text.Trim(), Is.EqualTo("The Movie is edited successfully!")); 

            allMoviesPage.LastPage.Click();
            Assert.That(allMoviesPage.LastMovieTitle.Text.Trim(), Is.EqualTo(lastEditedMovieTitle));

        }

        [Test, Order(5)]
        public void MarkedAsWatchedLastAddedMovieTest()
        {
            allMoviesPage.MarkAsWatchedLastAddedMovie();

            Assert.That(watchedMoviesPage.GetLastWatchedMovieTitle(), Is.EqualTo(lastEditedMovieTitle));
            Assert.That(watchedMoviesPage.GetLastWatchedMovieTitle(), Is.EqualTo(allMoviesPage.GetLastAddedMovieTitle()));

        }

        [Test, Order(6)]
        public void DeleteLastAddedMovieTest()
        {
            allMoviesPage.DeleteLastMovie();
            Assert.That(allMoviesPage.MessageDeletedSuccessfully.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"));

        }


    }
}
