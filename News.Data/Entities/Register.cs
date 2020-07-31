using System;

namespace News.Data.Entities
{
    public class Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public DateTime DateCreated { get; set; }

        public Register()
        {
            DateCreated = DateTime.Now;
        }
    }
}
