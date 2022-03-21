using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ControllerNameValidator : AbstractValidator<ControllerName>
    {
        public ControllerNameValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Cotroller adı boş ola bilməz")
                .MaximumLength(100).WithMessage("Cotroller adı maksimum 100 simvol ola biler");
        }
    }
}
