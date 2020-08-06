using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class RegisterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
