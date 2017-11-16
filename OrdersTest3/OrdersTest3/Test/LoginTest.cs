using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OrdersTest3.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Test
{
    [TestClass]
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        public LoginTest()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPage(driver);
        }

        [TestMethod]
        public void Login()
        {
            loginPage.NavigateToLoginPage();
            loginPage.PutCredentials();
            loginPage.ValidateLogin();
            loginPage.LogOff();
        }



    }
}
