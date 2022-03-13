using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Istifadeci Adi boş ola bilməz")
                .MinimumLength(3).WithMessage("Istifadeci Adi minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Istifadeci Adi maksimum 50 simvol ola biler");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email adresi boş ola bilməz")
                .MaximumLength(250).WithMessage("Email adresi maksimum 250 simvol ola biler")
                .EmailAddress().WithMessage("Email adresi düzgün daxil edilmeyib");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Basliq boş ola bilməz")
                .MaximumLength(50).WithMessage("Basliq maksimum 50 simvol ola biler");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj boş ola bilməz");
        }
    }
}
