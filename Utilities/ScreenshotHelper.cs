using AventStack.ExtentReports;
using Demo_Project.Reports;
using OpenQA.Selenium;
using System;
using System.IO;

public class ScreenshotHelper
{
    private readonly IWebDriver driver;
    protected ExtentTest extentTest;

    public ScreenshotHelper(IWebDriver driver)
    {
        this.driver = driver;
        extentTest = ExtentReporting.extentTest;
    }

    public void CaptureScreenshot()
    {
        try
        {
            // Get the project root directory (not the bin folder)
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            // Define the screenshot folder relative to the project root
            string screenshotFolder = Path.Combine(projectRoot, "Screenshots");

            // Check if the folder exists; if not, create it
            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }

            // Generate the full path for the screenshot with dynamic naming
            string testName = TestContext.CurrentContext.Test.Name;
            string screenshotFilePath = Path.Combine(screenshotFolder, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            // Capture the screenshot
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotFilePath);

            // Log the screenshot path into the console (for debugging purposes)
            Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

            // Capture the screenshot in the Extent report using a relative path
            string relativeScreenshotPath = Path.Combine("Screenshots", $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            // Attach the screenshot to the Extent report
            extentTest.AddScreenCaptureFromPath(screenshotFilePath);  // Attach screenshot to the report

            // Log the failure message and attach the screenshot to ExtentReports
            ExtentReporting.LogScreenshot("Test failed. Screenshot captured.", relativeScreenshotPath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to capture screenshot: " + e.Message);
        }
    }
}
