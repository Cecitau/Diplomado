﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAPI_Test3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI_Test3
{
    [TestClass]
    public class ApiTestProduct
    {
        private ApiLogicProduct _apiLogicProduct;


        public ApiTestProduct()
        {
            _apiLogicProduct = new ApiLogicProduct();
        }

         [TestMethod]
         public void InsertNewProduct()
        {
        _apiLogicProduct.GenerateToken();
        Products newProductApi = _apiLogicProduct.InsertNewProductAPI();
        //apiLogicProduct.ValidateNewCategory(newProductApi);

        }

}
}
