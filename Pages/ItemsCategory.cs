using OpenQA.Selenium;
using Demo_Project.Utilities;
using System;

namespace Demo_Project.Pages
{
    public class ItemsCategory
    {
        private IWebDriver driver;
        private WaitUtility waitUtility;

        public ItemsCategory(IWebDriver driver)
        {
            this.driver = driver;
            waitUtility = new WaitUtility(driver);
        }

        public IWebElement PhonesCategories => driver.FindElement(By.XPath("//a[text()='Phones']"));
        public IWebElement LaptopesCategories => driver.FindElement(By.XPath("//a[text()='Laptops']"));
        public IWebElement MonitorsCategories => driver.FindElement(By.XPath("//a[text()='Monitors']"));
        public IWebElement MobileItem => driver.FindElement(By.XPath("//a[text()='Samsung galaxy s6']"));
        public IWebElement LaptopItem => driver.FindElement(By.XPath("//a[text()='Sony vaio i5']"));
        public IWebElement MonitorItem => driver.FindElement(By.XPath("//a[text()='Apple monitor 24']"));
        public IWebElement AddToCart => driver.FindElement(By.XPath("//a[text()='Add to cart']"));

        
        private void ClickElementWithScroll(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            waitUtility.WaitForElementToBeClickable(element);
            element.Click();
        }

        public void ClickElement(IWebElement element)
        {
            waitUtility.WaitForElementToBeClickable(element);
            element.Click();
        }

        public void ClickOnPhonesCategories() => ClickElementWithScroll(PhonesCategories);
        public void ClickOnLaptopsCategories() => ClickElementWithScroll(LaptopesCategories);
        public void ClickOnMonitorsCategories() => ClickElementWithScroll(MonitorsCategories);
        public void SelectMobileItem() => ClickElementWithScroll(MobileItem);
        public void SelectLaptopItem() => ClickElementWithScroll(LaptopItem);
        public void SelectMonitorItem() => ClickElementWithScroll(MonitorItem);
        public void ClickOnAddToCart() => ClickElement(AddToCart);
        public void HandlePopup()
        {
            try
            {
                // Save the current window handle
                string currentWindowHandle = driver.CurrentWindowHandle;

                // Check if an alert is present
                if (IsAlertPresent())
                {
                    // Switch to the alert
                    IAlert alert = driver.SwitchTo().Alert();

                    // Accept the alert
                    alert.Accept();

                    // After accepting the alert, switch back to the main window
                    driver.SwitchTo().Window(currentWindowHandle);
}
                else
                {
                    //Console.WriteLine("No alert is present.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error while handling the pop-up: {ex.Message}");
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                // Try switching to the alert to check if it's present
                driver.SwitchTo().Alert();
                return true;  // If an alert is found
            }
            catch (NoAlertPresentException)
            {
                // If no alert is present, return false
                return false;
            }
        }

    }
}
