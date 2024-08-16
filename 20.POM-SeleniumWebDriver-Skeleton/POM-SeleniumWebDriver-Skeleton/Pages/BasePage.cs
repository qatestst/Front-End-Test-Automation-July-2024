﻿using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class BasePage
    {
            protected IWebDriver driver;

            protected WebDriverWait wait;

            protected Actions actions;

            protected static string BaseUrl = "https://www.someUrl.com";

            public BasePage(IWebDriver driver)
            {
                this.driver = driver;
                this.actions = new Actions(driver);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }

        // WebElements here:
        // public IWebElement NavBarLoginLink => driver.FindElement(By.XPath("//a[@class='nav-link text-dark'][@href='/Users/Login#loginForm']"));
        // public IWebElement NavBarRegisterLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li[1]/a"));


    }
}
