using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class ProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
