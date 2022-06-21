using Aranda.Product.Infraestructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Aranda.Product.Infraestructure.Filter
{
    public class SearchFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public Expression<Func<Models.Product, bool>> Contains()
        {
            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(Category)) return null;

           Expression<Func<Models.Product, bool>> query = x => false;

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Or(x=> x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                query = query.Or(x=> x.Description.Contains(Description));
            }
            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Or(x => x.Category.Name.Contains(Category));
            }

            return query;
        }        
    }
}
