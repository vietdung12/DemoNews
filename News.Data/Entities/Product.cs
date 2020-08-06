using System;
using System.Collections.Generic;
using System.Text;

namespace News.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }     
        public string Local { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }
        public List<Image> Images { get; set; }
       
    }
}
