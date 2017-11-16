using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OrdersTest3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace OrdersTest3.Page
{
    public class CategoryPage
    {
        private IWebDriver driver;
        private string baseUri;

        public CategoryPage(IWebDriver driver)
        {
            this.driver = driver;
            baseUri = "http://localhost:24548/";

        }

        public void CreateNewCategoryDB(string description)
        {
            using (OrdersEntities db = new OrdersEntities())
            {
                Categories category = new Categories()
                {
                    Description = description
                };
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        public void DeleteCategoryDB(string modified)
        {
            using (OrdersEntities db = new OrdersEntities())
            {
                Categories category =
                    db.Categories.FirstOrDefault(c => c.Description == modified);

                if (category == null) {
                    //do something                    
                }else
                { 
                    db.Categories.Remove(category);
                    db.SaveChanges();
                }


            }
        }

        public void ValidateCategoryEdit(string modified)
        {
            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> Descriptions =
           driver.FindElements(By.XPath("//tr/td[2]"));
            Thread.Sleep(2000);
            var newDesc = Descriptions.ToList();
            newDesc.RemoveAll(TextIsEmpty);
            IWebElement categoryModified= 
                newDesc.FirstOrDefault(c => c.Text.Equals(modified));
            Assert.IsNotNull(categoryModified, "test failed");
        }

        private static bool TextIsEmpty(IWebElement s)
        {
            return s.Text == "";
        }

        public void EditCategory(string actual, string modified)
        {
            IReadOnlyCollection<IWebElement> EditList=
            driver.FindElements(By.CssSelector("span.btn.btn-primary"));

            IReadOnlyCollection<IWebElement> Descriptions=
            driver.FindElements(By.XPath("//tr/td[2]"));

            int index = 
                Descriptions.ToList().FindIndex(a => a.Text.Equals(actual));
            EditList.ToList()[index - 1].Click();

            IWebElement DescriptionInput = driver.FindElement(By.XPath("(//input[@type='text'])[2]"));
            DescriptionInput.Clear();
            DescriptionInput.SendKeys(modified);

            IWebElement save = 
                driver.FindElement(By.CssSelector("input.btn.btn-default"));
            save.Click();
        }

        public void NavigateToCategoryPage()
        {
            driver.Navigate().GoToUrl(baseUri);
            IWebElement linkCat = driver.FindElement(By.LinkText("Category"));
            linkCat.Click();
        }
    }
}
