using OrdersTest3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Helpers
{
    public class ComperProductTo : IEqualityComparer<Products>
    {
        public bool Equals(Products x, Products y)
        {
            return x.Name.Equals(y.Name) && x.Id.Equals(y.Id) &&
                x.Rank.Equals(y.Rank);
        }

        public int GetHashCode(Products obj)
        {
            throw new NotImplementedException();
        }
    }
}
