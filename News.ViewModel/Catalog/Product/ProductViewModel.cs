using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Local { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Status { get; set; }

    }
}
