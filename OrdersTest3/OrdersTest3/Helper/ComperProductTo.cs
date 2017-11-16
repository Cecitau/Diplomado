using OrdersTest3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Helper
{
    public class ComperProductTo : IEqualityComparer<Products>
    {
        public bool Equals(Products x, Products y)
        {
            return x.Name.Equals(y.Name) && x.Id.Equals(y.Id)&&
                x.Rank.Equals(y.Rank) && x.Category_Id.Equals(y.Category_Id);
        }

        public int GetHashCode(Products obj)
        {
            throw new NotImplementedException();
        }
    }
}
