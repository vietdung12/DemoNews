using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class RegisterViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Tên Khách hàng")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Telephone { get; set; }
        [Display(Name = "Id SP")]
        public int? IdProduct { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }
    }
}
