using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class CreateRegisterRequest
    {
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public int Telephone { get; set; }
        [Display(Name = "ID SP")]
        public int IdProduct { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        public CreateRegisterRequest()
        {
            DateCreated = DateTime.Now;
        }
    }
}
