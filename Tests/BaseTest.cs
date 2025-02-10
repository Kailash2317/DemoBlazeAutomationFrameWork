using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.IO;
using Demo_Project.Reports;
using Demo_Project.Utilities;

namespace Demo_Project.Tests
{
    [TestFixture]
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;
        protected ExtentTest extentTest;
        protected TestData testData;
        protected ScreenshotHelper screenshotHelper;    

        [SetUp]
        public void Setup()
        {
            // Initialize ChromeDriver with options
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito"); // Launch Chrome in Incognito mode

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

            // Navigate to the test URL
            driver.Navigate().GoToUrl("https://www.demoblaze.com/");

            // Start ExtentReporting and create a test instance
            ExtentReporting.CreateTest(TestContext.CurrentContext.Test.Name);
            extentTest = ExtentReporting.extentTest; // Reference to the current test instance

            // Log test information in the report
            ExtentReporting.LogInfo("Test Started: " + TestContext.CurrentContext.Test.Name);

            // Load test data
            string dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Utilities\data.json");
            testData = TestData.LoadData(dataFilePath);

            screenshotHelper = new ScreenshotHelper(driver);
        }

        [TearDown]
        public void TearDown()
        {
            // Capture the status of the test
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = "Test finished with status: " + status;

            // Handle test failure, success, or skipped scenarios
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                // If the test failed, log the failure and capture the screenshot
                ExtentReporting.LogFail(message);  // Log failure to ExtentReports
                screenshotHelper.CaptureScreenshot();  // Capture screenshot for the failed test
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                // If the test passed, log the success
                ExtentReporting.LogPass(message);
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Skipped)
            {
                // If the test was skipped, log the skipped status
                ExtentReporting.LogInfo("Test was skipped.");
            }

            // Finalize the report
            ExtentReporting.EndReporting();

            // Dispose WebDriver
            Dispose();
        }

        // Capture screenshot if the test fails
       

        // Dispose of WebDriver
        public void Dispose()
        {
            driver?.Quit();
        }
    }
}
