﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.ViewModel.System.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(200).WithMessage("First name has been exceed 200 characters limited");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
               .MaximumLength(200).WithMessage("Last name has been exceed 200 characters limited");

            RuleFor(x => x.DoB).NotNull().WithMessage("DoB is required")
              .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("DoB doesn't not allowed if greater than 100 years ago");

            RuleFor(x => x.Email).NotNull().WithMessage("Email is required")
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email format is not correct");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password need to be greater than 8 characters")
                .Matches("[A-Z]").WithMessage("Password need at least 1 Uppercase letter")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password needs at least 1 special character");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User is required");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.ConfirmPassword), "Confirmation password is not correct");
                }
            });
        }
    }
}
