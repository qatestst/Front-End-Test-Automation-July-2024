using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogueTests.Tests
{
    public class MovieCatalogueTests : BaseTests
    {
        private string lastMovieTitle;
        private string lastMovieDescription;

        private string lastEditedTitle;
        private string lastEditedDescription;
        
        [Test, Order(1)]
        public void AddMovieWithoutTitleTest()
        {
            addMoviePage.OpenPage();
            addMoviePage.CreateMovie("", "some description");

            addMoviePage.AssertEmptyTitleMessage();
        }

        [Test, Order(2)]
        public void AddMovieWithoutDescriptionTest()
        {
            addMoviePage.OpenPage();

            addMoviePage.CreateMovie("Title", "");

            addMoviePage.AssertEmptyDescriptionMessage();
        }

        [Test, Order(3)]
        public void AddMovieWithValidDataTest()
        {
            lastMovieTitle = "Title" + addMoviePage.GenerateRandomString(5);
            lastMovieDescription = "Description" + addMoviePage.GenerateRandomString(10);
            
            addMoviePage.OpenPage();

            addMoviePage.CreateMovie(lastMovieTitle, lastMovieDescription);

            string createdMovieTitle = allMoviesPage.GetLastMovieTitle();
            Assert.That(lastMovieTitle.ToUpper(), Is.EqualTo(allMoviesPage.GetLastMovieTitle()));

        }

        [Test, Order(4)]
        public void EditLastCreatedMovieTest()
        {
            lastEditedTitle = "Edited" + lastMovieTitle;
            lastEditedDescription = "Edited" + lastMovieDescription;

            allMoviesPage.OpenPage();
            
            allMoviesPage.EditLastCreatedMovie(lastEditedTitle, lastEditedDescription);

            allMoviesPage.AssertSuccessfullyEditedMovieMessage();
            
            Assert.That(allMoviesPage.GetLastMovieTitle(), Is.EqualTo(lastEditedTitle.ToUpper()));


        }

        [Test, Order(5)]
        public void MarkAsWatchedLastEditedMovieTest()
        {
            allMoviesPage.MarkMovieAsWatched();
            
            Assert.That(watchedMoviesPage.GetLastWatchedMovieTitle, Is.EqualTo(lastEditedTitle.ToUpper()));
            
        }

        [Test, Order(6)]
        public void DeleteLastAddedMovieTest()
        {
            allMoviesPage.DeleteLastAddedMovie();
            allMoviesPage.AssertSuccessfullyDeletedMovieMessage();
        }






    }
}
