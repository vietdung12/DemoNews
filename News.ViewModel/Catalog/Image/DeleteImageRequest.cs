using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.ViewModel.Catalog.Image
{
    public class DeleteImageRequest
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
    }
}
