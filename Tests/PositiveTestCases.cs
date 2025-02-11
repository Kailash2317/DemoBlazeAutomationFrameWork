using NUnit.Framework;
using Demo_Project.Pages;
using Demo_Project.Utilities;
using OpenQA.Selenium;
using System;
using Demo_Project.Reports;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Demo_Project.Tests
{
    [TestFixture]
    public class PositiveTestCases : BaseTest
    {
        private LoginHelper loginHelper;
        private HomePage homePage;
        private SignUpPage signUpPage;
        private ItemsCategory itemsCategory;
        private CheckoutPage checkoutPage;
        private WaitUtility waitUtility;

        // Start Extent Reporting for each test
        [SetUp]
        public void SetUp()
        {
            loginHelper = new LoginHelper(driver);
            homePage = new HomePage(driver);
            signUpPage = new SignUpPage(driver);
            itemsCategory = new ItemsCategory(driver);
            checkoutPage = new CheckoutPage(driver);
            waitUtility = new WaitUtility(driver);
           
        }

        [Test]
        [Order(1)]
        public void SignUp()
        {
            ExtentReporting.LogInfo("Click On Sign Up button.");

            // Access the signup data from the loaded testData object
            string username = testData.signup.username;
            string password = testData.signup.password;
            homePage.ClickOnSignUp();
            ExtentReporting.LogInfo("Enter User Name");
            signUpPage.EnterUserName(username);
            ExtentReporting.LogInfo("Enter Password");
            signUpPage.EnterPassword(password);
            ExtentReporting.LogInfo("Click on signUp Button.");
            signUpPage.ClickOnSignUpButton();
            waitUtility.waitOfSeconds(3);
            signUpPage.HandlePopup();
            
        }

        [Test,Order(2)]
        public void Login_WithValidCredentials()
        {
            // Log the test action to Extent Reports
            ExtentReporting.LogInfo("Starting the login with valid credentials.");

            // Access the login data from the loaded testData object
            string username = testData.login.username;
            string password = testData.login.password;

            // Perform the login using the helper
            loginHelper.Login_WithValidCredentials(username, password);
            homePage.HandlePopUpForPositiveTestCase();
            //homePage.IsLogOutButtonDisplayed(); 
            Assert.IsTrue(homePage.IsLogOutButtonDisplayed(), "Error While Login");
            ExtentReporting.LogInfo("Login with valid credentials successful.");


        }

        [Test,Order(3)]
        public void orderItem()
        {
            ExtentReporting.LogInfo("Starting the login with valid credentials.");
            // Access the login data from the loaded testData object
            string username = testData.login.username;
            string password = testData.login.password;
            string name = "Tester";
            string city = "jodhpur";
            string country = "India";
            string creditCard = "1234546";
            string month = "june";
            string year = "2025";
            loginHelper.Login_WithValidCredentials(username, password);
            homePage.HandlePopUpForPositiveTestCase();
            waitUtility.waitOfSeconds(3);   
            ExtentReporting.LogInfo("Succesfully loged in.");
            itemsCategory.ClickOnPhonesCategories();
            itemsCategory.SelectMobileItem();
            itemsCategory.ClickOnAddToCart();
            waitUtility.waitOfSeconds(2);   
            itemsCategory.HandlePopup();
            CheckoutProcess(name,country,city,creditCard,month,year);
            Assert.IsTrue(checkoutPage.IsConfirmOrderMessageDisplayed(), "Order Placed Succussfully");
            waitUtility.waitOfSeconds(2);
            checkoutPage.ClickOnOkButton();
            ExtentReporting.LogInfo("Succesfully ordered the item.");

        }

        [Test,Order(4)]
        public void Logout()
        {
            // Access the login data from the loaded testData object
            string username = testData.login.username;
            string password = testData.login.password;
            ExtentReporting.LogInfo("Starting the login with valid credentials.");
            loginHelper.Login_WithValidCredentials(username, password);
            homePage.HandlePopUpForPositiveTestCase(); 
            ExtentReporting.LogInfo("Login with valid credentials successful.");
            homePage.ClickOnLogOut();
            Assert.IsTrue(homePage.IsLogInButtonDisplayed(), "LogOut Unsuccesfull");
            ExtentReporting.LogInfo("Succesfully logged out");
            waitUtility.waitOfSeconds(1);
           
        }


        public void CheckoutProcess(string name, string country, string city, string creditCard, string month, string year)
        {
            homePage.ClickOnCart();
            waitUtility.waitOfSeconds(3);
            checkoutPage.ClickOnPlaceOrder();
            checkoutPage.EnterName(name);
            checkoutPage.EnterCountry(country);
            checkoutPage.EnterCity(city);
            checkoutPage.EnterCreditCardNumber(creditCard);
            checkoutPage.EnterMonth(month);
            checkoutPage.EnterYear(year);
            checkoutPage.ClickOnPurchase();
           
        }

        // End the Extent Report after the test
        [TearDown]
        public void TearDown()
        {
            // Finalize Extent Report for the test
            ExtentReporting.EndReporting();
        }

        // This method will be executed after all tests in the class have finished
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Send email with the final report after all tests are complete
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\results\index.html");
            
            // Email configuration
            string recipientEmail = "kailash.8619552317@gmail.com";  // Change to actual recipient
            string subject = "Test Report - All Test Cases Completed";
            string body = "Please find the attached Extent Test Report for the completed test run.";

            // Send the report as an email attachment
            EmailSender.SendEmailWithAttachment(recipientEmail, subject, body, reportPath);
        }
    }
}
