using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class CreateProductRequestModel
    {
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Khu vực")]
        public string Local { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Giá bán")]
        public string Price { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        public CreateProductRequestModel()
        {
            DateCreated = DateTime.Now;
        }
    }
}
