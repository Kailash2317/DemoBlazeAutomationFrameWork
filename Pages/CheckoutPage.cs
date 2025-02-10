using System;
using OpenQA.Selenium;
using Demo_Project.Utilities;

namespace Demo_Project.Pages
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        private WaitUtility waitUtility;

        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            waitUtility = new WaitUtility(driver);
        }

        // Element Locators
        private IWebElement PlaceOrder => driver.FindElement(By.XPath("//button[text()='Place Order']"));
        private IWebElement Name => driver.FindElement(By.XPath("//input[@id='name']"));
        private IWebElement Country => driver.FindElement(By.XPath("//input[@id='country']"));
        private IWebElement City => driver.FindElement(By.XPath("//input[@id='city']"));
        private IWebElement CreditCard => driver.FindElement(By.XPath("//input[@id='card']"));
        private IWebElement Month => driver.FindElement(By.XPath("//input[@id='month']"));
        private IWebElement Year => driver.FindElement(By.XPath("//input[@id='year']"));
        private IWebElement Purchase => driver.FindElement(By.XPath("//button[text()='Purchase']"));
        private IWebElement CloseButon => driver.FindElement(By.XPath("//button[text()='Close']")); 
        private IWebElement OrderConfirmMessage => driver.FindElement(By.XPath("//h2[text()='Thank you for your purchase!']"));
        private IWebElement OkButton => driver.FindElement(By.XPath("//button[text()='OK']"));

        public void ClickOnPlaceOrder()
        {
            PlaceOrder.Click(); 
        }

        private void EnterText(IWebElement element, string text)
        {
            element.Clear(); // Clear the field before entering text
            element.SendKeys(text);
        }
        public void EnterName(string name) => EnterText(Name, name);
        public void EnterCountry(string country) => EnterText(Country, country);
        public void EnterCity(string city) => EnterText(City, city);
        public void EnterCreditCardNumber(string creditCard) => EnterText(CreditCard, creditCard);
        public void EnterMonth(string month) => EnterText(Month, month);
        public void EnterYear(string year) => EnterText(Year, year);
        public void ClickOnPurchase()
        {
           Purchase.Click();    
        }
        public void ClickOnOkButton()
        {
            OkButton.Click();   
        }
        public void ClickOnClose()
        {
            CloseButon.Click();
        }
        public bool IsConfirmOrderMessageDisplayed()
        {
            return OrderConfirmMessage.Displayed;
        }
    }
}
