using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş ola bilməz")
                .MinimumLength(3).WithMessage("Ad minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Ad maksimum 50 simvol ola biler");
            RuleFor(x => x.Description).MaximumLength(50).WithMessage("Ad maksimum 250 simvol ola biler");
        }
    }
}
