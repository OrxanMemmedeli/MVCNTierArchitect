using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContentValidator : AbstractValidator<Content>
    {
        public ContentValidator()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Text boş ola bilməz")
                .MinimumLength(3).WithMessage("Text minimum 3 simvol ola biler")
                .MaximumLength(1000).WithMessage("Text maksimum 1000 simvol ola biler");
            RuleFor(x => x.CreatedDate).NotEmpty().WithMessage("Yaranma tarixi boş ola bilməz");
        }
    }
}
