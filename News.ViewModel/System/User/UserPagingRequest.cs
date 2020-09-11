using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.System.User
{
    public class UserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
