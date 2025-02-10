using System;
using OpenQA.Selenium;
using Demo_Project.Utilities;

namespace Demo_Project.Pages
{
    public class LoginPage
    {

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement UserName => driver.FindElement(By.XPath("//input[@id='loginusername']"));
        public IWebElement Password => driver.FindElement(By.XPath("//input[@id='loginpassword']"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//button[text()='Log in']"));
        public IWebElement CloseButton => driver.FindElement(By.XPath("//button[text()='Close']"));

        public void ClickOnCloseButton()
        {
            CloseButton.Click();    
        }
        public void EnterUsername(string username)
        {
           UserName.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            LoginButton.Click();
        }

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

                    //Console.WriteLine("Alert accepted, switched back to the main window.");
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

        public bool IsLoginSuccessful()
        {
            // Wait for the login success message to appear
            return true;
        }

        public void Logout()
        {
            // Implement logout functionality if necessary
        }
    }
}
