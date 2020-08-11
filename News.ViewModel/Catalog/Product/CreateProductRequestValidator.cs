using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Product
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequestModel>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Tiêu đề không được bỏ trống").MaximumLength(200).WithMessage("Tiêu đề không được vượt quá 200 kí tự");
            RuleFor(x => x.Local).NotEmpty().WithMessage("Khu vực không được bỏ trống");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả không được bỏ trống").MaximumLength(500).WithMessage("Mô tả không được vượt quá 200 kí tự");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Giá bán không được bỏ trống");
        }
    }
}
