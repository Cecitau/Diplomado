using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using OrdersAPI_Test3.Helpers;
using OrdersAPI_Test3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI_Test3
{
    public class ApiLogicCategory
    {
        private string _baseUri = ConfigurationManager.AppSettings["BaseUri"] ?? "http://localhost:33991";
        private string _username = ConfigurationManager.AppSettings["username"] ?? "user100@gmail.com";
        private string _passsword = ConfigurationManager.AppSettings["password"] ??"Password1.";

        private List<Categories> _categoryListApi;
        private List<Categories> _categoryListDB;

        private ClientApiHelper _clientApiHelper;

        private OrdersEntities db;
        public ApiLogicCategory()
        {
            _clientApiHelper = new ClientApiHelper(_baseUri);
            db = new OrdersEntities();
        }

        public void GenerateToken()
        {
            _clientApiHelper.GetToken(_username, _passsword);
        }

        public void ValidateNewCategory(Categories categoryApi)
        {
            Categories categoryDB = db.Categories.Find(categoryApi.Id);
            if (categoryDB == null)
                Assert.Fail("Category was not found in DB");

            Assert.IsTrue(categoryApi.Description.Equals(categoryDB.Description), "Description is not equal");

        }

       
        public void DeleteCategoryDB(Categories categoryApi)
        {

            db.Categories.Remove(db.Categories.Find(categoryApi.Id));
            db.SaveChanges();
        }

        public Categories InsertNewCategoryAPI()
        {
            Categories _newCategory = new Categories()
            { Description = "newCategory" + DateTime.Now.Millisecond };

            Dictionary<string, string> paramenters =
                new Dictionary<string, string>();
                paramenters.Add("Description", _newCategory.Description);

            dynamic data = _clientApiHelper.PostAsync("api/Categories", paramenters, true);

            Categories categoryResult = new Categories();
            categoryResult.Id = data["Id"];
            categoryResult.Description = data["Description"];
            return categoryResult;
        }

        public List<Categories> GetCategoriesList_Api()
        {
            JArray result = _clientApiHelper.GetAsync("api/Categories");
            _categoryListApi = new List<Categories>();
            foreach (var item in result)
            {
                _categoryListApi.Add(new Categories()
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Description = item["Description"].ToString()
                });
            }
            return _categoryListApi;
        }

        public List<Categories> GetActegoriesList_DB()
        {
            _categoryListDB = db.Categories.ToList();
            return _categoryListDB;
        }

        public bool ValidateCategories_Api_DB()
        {
            return _categoryListApi.SequenceEqual(_categoryListDB, new CompareCategoryTo());
        }
        
        }
  }
