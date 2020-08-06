using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class CreateRegisterRequest
    {        
        public string Name { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public DateTime DateCreated { get; set; }

        public CreateRegisterRequest()
        {
            DateCreated = DateTime.Now;
        }
    }
}
