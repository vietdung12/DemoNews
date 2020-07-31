using System;
using System.Collections.Generic;
using System.Text;

namespace News.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDefault { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public Image()
        {
            DateCreated = DateTime.Now;
        }

    }
}
