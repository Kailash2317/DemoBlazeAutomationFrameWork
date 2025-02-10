using System;
using Demo_Project.Reports;
using OpenQA.Selenium;

namespace Demo_Project.Pages
{
    public class SignUpPage
    {
        private IWebDriver driver;

        public SignUpPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement UserName => driver.FindElement(By.XPath("//input[@id='sign-username']"));
        public IWebElement PassWord => driver.FindElement(By.XPath("//input[@id='sign-password']"));
        public IWebElement SignUpButton => driver.FindElement(By.XPath("//button[text()='Sign up']"));
        public IWebElement CloseButton => driver.FindElement(By.XPath("(//button[text()='Close'])[2]"));

        public void EnterUserName(string userName)
        {
            UserName.SendKeys(userName);
        }

        public void EnterPassword(string passWord)
        {
            PassWord.SendKeys(passWord);
        }

        public void ClickOnSignUpButton()
        {
            SignUpButton.Click();
        }

        public void ClickOnCloseButton()
        {
            CloseButton.Click();
        }

        public void HandlePopup()
        {
            try
            {
                string currentWindowHandle = driver.CurrentWindowHandle;
               
                if (IsAlertPresent())
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    var alertText = alert.Text;
                    alert.Accept();
                    driver.SwitchTo().Window(currentWindowHandle);
                    Assert.AreEqual("Sign up successful.", alertText);
                    ExtentReporting.LogInfo("Succesfully Registered");
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
        public void HandlePopUp()
        {
            try
            {
                string currentWindowHandle = driver.CurrentWindowHandle;

                if (IsAlertPresent())
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    var alertText = alert.Text;
                    alert.Accept();
                    driver.SwitchTo().Window(currentWindowHandle);
                    Assert.AreEqual("This user already exist.", alertText);
                    ExtentReporting.LogInfo("This user already exist.");
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
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
    }
}
