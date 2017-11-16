using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrdersTest3.Page
{
    public class LoginPage
    {
        private IWebDriver driver;
        private string baseUri;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            baseUri = "http://localhost:24548/";
        }

        public void NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl(baseUri);
            driver.Manage().Window.Maximize();
            IWebElement linkLogin = driver.FindElement(By.Id("loginLink"));
            linkLogin.Click();
        }

       

        public void PutCredentials()
        {
            IWebElement email = driver.FindElement(By.Id("Email"));
            email.Clear();
            email.SendKeys("claudiomelendres@gmail.com");

            IWebElement password = driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("Password2.");

            IWebElement btnLogin = driver.FindElement(By.XPath("//*[@id='loginForm']/form/div[4]/div/input"));
            btnLogin.Click();
        }

        public void ValidateLogin()
        {
            IWebElement linkTest = driver.FindElement(By.LinkText("Hello claudiomelendres@gmail.com!"));
            Assert.IsTrue(linkTest.Text.Contains("claudiomelendres"));

        }

        public void LogOff()
        {
            IWebElement linkLogOff = driver.FindElement(By.LinkText("Log off"));
            linkLogOff.Click();

            driver.Close();
        }
    }
}
