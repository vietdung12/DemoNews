using System;

namespace News.Data.Entities
{
    public class Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int IdProduct { get; set; }
        public DateTime DateCreated { get; set; }
        
    }
}
