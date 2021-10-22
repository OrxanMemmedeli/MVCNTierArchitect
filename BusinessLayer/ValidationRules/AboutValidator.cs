using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.DetailsFirst).NotEmpty().WithMessage("Birinci Aciqlama boş ola bilməz")
                .MinimumLength(3).WithMessage("Birinci Aciqlama minimum 3 simvol ola biler")
                .MaximumLength(1000).WithMessage("Birinci Aciqlama maksimum 1000 simvol ola biler");
            RuleFor(x => x.DetailsSecond).MinimumLength(3).WithMessage("Birinci Aciqlama minimum 3 simvol ola biler")
                .MaximumLength(1000).WithMessage("Birinci Aciqlama maksimum 1000 simvol ola biler");
            RuleFor(x => x.İmageFirst).MaximumLength(250).WithMessage("Birinci Sekil Yolu maksimum 250 simvol ola biler");
            RuleFor(x => x.İmageSecond).MaximumLength(250).WithMessage("Ikinci Sekil Yolu maksimum 250 simvol ola biler");
        }
    }
}
