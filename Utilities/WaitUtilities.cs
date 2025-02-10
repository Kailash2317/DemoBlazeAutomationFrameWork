using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

public class WaitUtility
{
    private IWebDriver driver;
    private WebDriverWait wait;

    public WaitUtility(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    // Wait for an element to be visible
    public void WaitForElementToBeVisible(By element)
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(element));
    }

    // Wait for an element to be clickable
    public void WaitForElementToBeClickable(IWebElement element)
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
    }
    public void waitOfSeconds(int timeInSeconds)
    {
        // Convert the time in seconds to milliseconds and pause the execution
        Thread.Sleep(timeInSeconds * 1000);
    }

    // Optionally, add more wait methods like presence, invisibility, etc.
}
