using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AdressValidator : AbstractValidator<Adress>
    {
        public AdressValidator()
        {
            RuleFor(x => x.URL).NotEmpty().WithMessage("URL boş ola bilməz")
                .MinimumLength(5).WithMessage("URL minimum 3 simvol ola biler")
                .MaximumLength(500).WithMessage("URL maksimum 500 simvol ola biler");
        }
    }
}
