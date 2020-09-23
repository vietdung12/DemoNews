using News.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Image
{
    public class SetDefaultImageRequest
    {
        public int ProductId { get; set; }
        public List<SelectItem> Images { get; set; } = new List<SelectItem>();
    }
}
