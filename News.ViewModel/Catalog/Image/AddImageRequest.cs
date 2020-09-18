using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Image
{
    public class AddImageRequest
    {  
        public int ProductId { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDefault { get; set; }
    } 
}
