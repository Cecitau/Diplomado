using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OrdersTest3.Model;
using OpenQA.Selenium.Support.PageObjects;
using OrdersTest3.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrdersTest3.Page
{
    public class ProductPage
    {
        private IWebDriver driver;
        private string baseUri;
        private List<Products> productListUI;
        private List<Products> productListDB;

        [FindsBy(How=How.LinkText, Using = "Products")]
        private IWebElement linkProduct;

        [FindsBy(How=How.XPath, Using = "//tr/td[1]")]
        private IList<IWebElement> Ids;

        [FindsBy(How = How.XPath, Using = "//tr/td[2]")]
        private IList<IWebElement> Names;

        [FindsBy(How = How.XPath, Using = "//tr/td[4]")]
        private IList<IWebElement> Ranks;

        private IJavaScriptExecutor js;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            baseUri = "http://localhost:24548/";
            productListUI = new List<Products>();
            productListDB = new List<Products>();
            js = driver as IJavaScriptExecutor;
        }

       

        public void CompareList()
        {
            LogHelper.WriteLog("Method CompareList");

            bool result =productListDB.SequenceEqual(productListUI, new ComperProductTo());
            Assert.IsTrue(result, "test Failed");

        }

        public void CreateNewProductsDB(string name, int rank, int price, int cat )
        {
            using (OrdersEntities db = new OrdersEntities())
            {
                Products product = new Products()
                {
                    Name = name,
                    Rank = rank,
                    Price = price,
                    Category_Id = cat
                };
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void NavigateProducts()
        {
            LogHelper.WriteLog("Navigate to Product Page");
            driver.Navigate().GoToUrl(baseUri);
           
            linkProduct.Click();
        }

        

        public void GetDBList()
        {
            LogHelper.WriteLog("Method Get DB List");

            using (OrdersEntities db = new OrdersEntities())
            {
                productListDB = db.Products.ToList();
            }
        }

        public void GetUIList()
        {
            LogHelper.WriteLog("Method Get UI List");

            JSExecutorHelper.HighLight(Names[1], js);
            var idList = Ids.Select(x => x.Text).Where(x => x != "").ToList();
            var namesList = Names.Select(x => x.Text).Where(x => x != "").ToList();
            for (int i = 1; i < namesList.Count; i++)
            {
                Products p = new Products();
                p.Id = Convert.ToInt32(idList[i]);
                p.Name = namesList[i];
                p.Rank = GetStars(Ranks[i]);
                productListUI.Add(p);
            }
        }

        private int GetStars(IWebElement webElement)
        {
            LogHelper.WriteLog("Method Get Starts");

            return webElement.FindElements(By.TagName("span")).Count;
        }

        

        public void NavigateToProducts()
        {
            LogHelper.WriteLog("Navigate to Product Page");
            driver.Navigate().GoToUrl(baseUri);
            JSExecutorHelper.HighLight(linkProduct, js);

            linkProduct.Click();

        }

        public void UpdateProduct(string actualname, string modifiedname, int modifiedrank, decimal modifiedprice)
        {

                 

            IReadOnlyCollection<IWebElement> EditList =
             driver.FindElements(By.CssSelector("span.btn.btn-primary"));

            IReadOnlyCollection<IWebElement> Name =
            driver.FindElements(By.XPath("//tr/td[2]"));

            int index =
                Name.ToList().FindIndex(a => a.Text.Equals(actualname));
            EditList.ToList()[index-1].Click();

            IWebElement NameInput = driver.FindElement(By.XPath("(//input[@type='text'])[3]"));
            NameInput.Clear();
            NameInput.SendKeys(modifiedname);

            IWebElement RankInput = driver.FindElement(By.XPath("(//input[@type='text'])[5]"));
            RankInput.Clear();
            RankInput.SendKeys(Convert.ToString(modifiedrank));

            IWebElement PriceInput = driver.FindElement(By.XPath("(//input[@type='text'])[4]"));
            PriceInput.Clear();
            PriceInput.SendKeys(Convert.ToString(modifiedprice));

            IWebElement save =
                driver.FindElement(By.CssSelector("input.btn.btn-default"));
            save.Click();
        }

        public void UpdateRank(int actualrank, int modifiedrank)
        {
            throw new NotImplementedException();
        }
    }
}
