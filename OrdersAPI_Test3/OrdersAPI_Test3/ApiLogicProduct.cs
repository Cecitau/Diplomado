using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAPI_Test3.Helpers;
using OrdersAPI_Test3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace OrdersAPI_Test3
{
    public class ApiLogicProduct
    {
        private string _baseUri = ConfigurationManager.AppSettings["BaseUri"] ?? "http://localhost:33991";
        private string _username = ConfigurationManager.AppSettings["username"] ?? "user100@gmail.com";
        private string _passsword = ConfigurationManager.AppSettings["password"] ?? "Password1.";

        //private List<Products> _productListApi;
        //private List<Products> _productListDB;

        private ClientApiHelper _clientApiHelper;

        private OrdersEntities db;
        public ApiLogicProduct()
        {
            _clientApiHelper = new ClientApiHelper(_baseUri);
            db = new OrdersEntities();
        }

        public void GenerateToken()
        {
            _clientApiHelper.GetToken(_username, _passsword);
        }

        internal Products InsertNewProductAPI()
        {
            Products _newProduct = new Products()
            { Name = "Name" + DateTime.Now.Millisecond,
              Price = 50,
              Rank = 5,
              Category_Id = 2
            };

            Dictionary<string, string> paramenters =
                new Dictionary<string, string>();
            paramenters.Add("Name", _newProduct.Name);
            paramenters.Add("Price", Convert.ToString(_newProduct.Price));
            paramenters.Add("Rank", Convert.ToString(_newProduct.Rank));
            paramenters.Add("Category_Id", Convert.ToString(_newProduct.Category_Id));
            dynamic data = _clientApiHelper.PostAsync("api/Products", paramenters, true);

            Products productResult = new Products();
            productResult.Id = data["Id"];
            productResult.Name = data["Name"];
            productResult.Price = data["Price"];
            productResult.Rank = data["Rank"];
            productResult.Category_Id = data["Category_Id"];
            return productResult;
        }

    }
}