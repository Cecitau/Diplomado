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
    public class CategoryTest
    {
        private IWebDriver driver;
        private CategoryPage categoryPage;

        public CategoryTest()
        {
            driver = new ChromeDriver();
            categoryPage = new CategoryPage(driver);
        }

        [TestMethod]
        public void EditCategory()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 999);

            string newCat = "NewCategory" + randomNumber;
            string modCat = "ModifiedCategory"+randomNumber;
            categoryPage.CreateNewCategoryDB(newCat);
            categoryPage.NavigateToCategoryPage();
            categoryPage.EditCategory(newCat, modCat);

            //categoryPage.ValidateCategoryEdit(modCat);
            //categoryPage.DeleteCategoryDB(modCat);
            driver.Close();
        }

       
    }
}
