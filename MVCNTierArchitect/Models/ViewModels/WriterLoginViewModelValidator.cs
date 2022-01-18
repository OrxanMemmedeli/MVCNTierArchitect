﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCNTierArchitect.Models.ViewModels
{
    public class WriterLoginViewModelValidator : AbstractValidator<WriterLoginViewModel>
    {
        public WriterLoginViewModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş ola bilməz")
                .EmailAddress().WithMessage("Email düzgün daxil edilməyib")
                .MinimumLength(3).WithMessage("Email minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Email maksimum 50 simvol ola biler");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol ola bilər. (nümunə Aa123!!)")
                .MaximumLength(20).WithMessage("Şifrə maksimum 20 simvol ola bilər. (nümunə Aa123!!)")
                .Matches("[A-Z]").WithMessage("Şifrədə ən azı 1 BÖYÜK simvol olmalıdır (nümunə Aa123!!)")
                .Matches("[a-z]").WithMessage("Şifrədə ən azı 1 KİÇİK simvol olmalıdır (nümunə Aa123!!)")
                .Matches("[0-9]").WithMessage("Şifrədə ən azı 1 RƏQƏM olmalıdır (nümunə Aa123!!)")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifrədə xüsusi simvollar olmalıdır. (nümunə Aa123!!)");
        }
    }
}