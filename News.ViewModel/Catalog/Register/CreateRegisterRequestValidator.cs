using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.Catalog.Register
{
    public class CreateRegisterRequestValidator : AbstractValidator<CreateRegisterRequest>
    {
        public CreateRegisterRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên không được bỏ trống").MaximumLength(100).WithMessage("Tên không vượt quá 100 kí tự");
            RuleFor(x => x.Telephone).NotEmpty().WithMessage("Số điện thoại không được bỏ trống");
            RuleFor(x => x.IdProduct).NotEmpty().WithMessage("ID SP không được bỏ trống");
        }
    }
}
