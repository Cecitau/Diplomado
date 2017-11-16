using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OrdersTest3.Helpers;
using OrdersTest3.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Test
{
    [TestClass]
    public class ProductTest
    {
        private IWebDriver driver;
        private ProductPage productPage;
        private string randomNumber;
       

        public ProductTest()
        {
            driver = new ChromeDriver();
            productPage = new ProductPage(driver);
            PageFactory.InitElements(driver, productPage);
        }

        [TestMethod]
        public void ProductsVerify()
        {
            LogHelper.WriteLog("======================Init ProductsVerify Test");

            productPage.NavigateToProducts();
            productPage.GetUIList();
            productPage.GetDBList();
            productPage.CompareList();
        }

        [TestMethod]
        public void UpDateProducts()
        {

            Random random = new Random();
            int randomNumber = random.Next(1, 10);
            
            string newname = "NewName" + randomNumber;
            string modname = "ModifiedName" + randomNumber;
            
                        
            productPage.CreateNewProductsDB(newname, randomNumber, randomNumber, randomNumber);
            productPage.NavigateProducts();
            productPage.UpdateProduct(newname, modname, randomNumber+5, randomNumber+50);
        }
    }
}
