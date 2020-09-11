using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class RegisterPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
