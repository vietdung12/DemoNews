using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Image
{
    public class ImageVM
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDefault { get; set; }
        public int ProductId { get; set; }      
    }
}
