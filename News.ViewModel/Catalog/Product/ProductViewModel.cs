using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class ProductViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name = "Khu vực")]
        public string Local { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Giá bán")]
        public string Price { get; set; }
        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

    }
}
