using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MethodNameValidator : AbstractValidator<MethodName>
    {
        public MethodNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Metod adı boş ola bilməz")
                .MaximumLength(100).WithMessage("Metod adı maksimum 100 simvol ola biler");
        }
    }
}
