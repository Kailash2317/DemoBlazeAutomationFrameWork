using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_Project.Pages;
using Demo_Project.Utilities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace Demo_Project.Utilities
{
    public class LoginHelper
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private HomePage homePage;
        private WaitUtility waitUtility;
        public LoginHelper(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            waitUtility = new WaitUtility(driver);  
        }

        public void Login_WithValidCredentials(string userName , string userPassword)
        {

            homePage.ClickOnLogIn();
            loginPage.EnterUsername(userName);
            waitUtility.waitOfSeconds(1);
            loginPage.EnterPassword(userPassword);
            waitUtility.waitOfSeconds(1);
            loginPage.ClickLoginButton();
            
        }
    }

}
