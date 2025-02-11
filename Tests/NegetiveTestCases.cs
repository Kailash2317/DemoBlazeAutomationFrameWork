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
    public class NegetiveTestCases : BaseTest
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
        public void SignUpWithAlreadyRegisteredUser()
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
            signUpPage.HandlePopUp();

        }

        [Test, Order(2)]
        public void Login_WithInValidCredentials()
        {
            // Log the test action to Extent Reports
            ExtentReporting.LogInfo("Starting the login with Invalid credentials.");

            // Access the login data from the loaded testData object
            string username = testData.login.username;
            string password = testData.login.password;

            // Perform the login using the helper
            loginHelper.Login_WithValidCredentials(username, password);
            waitUtility.waitOfSeconds(3);
            homePage.HandlePopUpForNegetiveTestCase();
           
        }
        // End the Extent Report after the test
        [TearDown]
        public void TearDown()
        {
            // Finalize Extent Report for the test
            ExtentReporting.EndReporting();
        }

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
