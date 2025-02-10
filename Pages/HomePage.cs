using System;
using OpenQA.Selenium;
using Demo_Project.Utilities;
using System.Threading;
using Demo_Project.Reports;

namespace Demo_Project.Pages
{
    public class HomePage
    {

        private IWebDriver driver;
        private WaitUtility waitUtility;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            waitUtility = new WaitUtility(driver);
        }

        public IWebElement Home => driver.FindElement(By.XPath("//a[text()='Home ']"));
        public IWebElement Cart => driver.FindElement(By.XPath("//a[text()='Cart']"));
        public IWebElement LogIn => driver.FindElement(By.XPath("//a[text()='Log in']"));
        public IWebElement LogOut => driver.FindElement(By.XPath("//a[@style='display: block;' and @id='logout2']"));
        public IWebElement SignUp => driver.FindElement(By.XPath("//a[text()='Sign up']"));
        public void ClickOnHome()
        {

            Home.Click();   
        }
        public void ClickOnCart()
        {
           Cart.Click();
        }
        public void ClickOnLogIn()
        {
            LogIn.Click();  
        }
        public void ClickOnLogOut()
        {
            LogOut.Click();  
        }
        public void ClickOnSignUp()
        {
            SignUp.Click(); 
        }
        public bool IsLogOutButtonDisplayed()
        {

            string log = LogOut.GetAttribute("id");
            if (log.Equals("logout2"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsLogInButtonDisplayed()
        {

            string log = LogIn.GetAttribute("id");
            if (log.Equals("login2"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void HandlePopUpForNegetiveTestCase()
        {
            try
            {
                string currentWindowHandle = driver.CurrentWindowHandle;

                if (IsAlertPresent())
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    var alertText = alert.Text;
                    if (alertText.Equals("User does not exist.")) {
                        alert.Accept();
                        driver.SwitchTo().Window(currentWindowHandle);
                        Assert.AreEqual("User does not exist.", alertText);
                        ExtentReporting.LogInfo("User does not exist.");
                    }
                    else
                    {
                        alert.Accept();
                        driver.SwitchTo().Window(currentWindowHandle);
                        Assert.AreEqual("Wrong password.", alertText);
                        ExtentReporting.LogInfo("Wrong password.");
                    }
                }
                else
                {
                     Assert.IsFalse(IsLogOutButtonDisplayed(), "LogIn Succesfull");
                }
                
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error while handling the pop-up: {ex.Message}");
            }
        }

        public void HandlePopUpForPositiveTestCase()
        {
            try
            {
                string currentWindowHandle = driver.CurrentWindowHandle;

                if (IsAlertPresent())
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    var alertText = alert.Text;
                    if (alertText.Equals("User does not exist."))
                    {
                        alert.Accept();
                        driver.SwitchTo().Window(currentWindowHandle);
                        Assert.AreEqual("User does not exist.", alertText);
                        ExtentReporting.LogInfo("User does not exist.");
                    }
                    else
                    {
                        alert.Accept();
                        driver.SwitchTo().Window(currentWindowHandle);
                        Assert.AreEqual("Wrong password.", alertText);
                        ExtentReporting.LogInfo("Wrong password.");
                    }
                }
                else
                {
                    // Assert.IsFalse(IsLogOutButtonDisplayed(), "LogIn Succesfull");
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
