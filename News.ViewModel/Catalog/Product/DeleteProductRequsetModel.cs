using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class DeleteProductRequsetModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
    }
}
