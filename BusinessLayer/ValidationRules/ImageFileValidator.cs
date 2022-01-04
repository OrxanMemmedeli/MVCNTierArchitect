using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ImageFileValidator : AbstractValidator<ImageFile>
    {
        public ImageFileValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlıq boş ola bilməz")
                .MinimumLength(3).WithMessage("Başlıq minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Başlıq maksimum 50 simvol ola biler");
            RuleFor(x => x.URL).NotEmpty().WithMessage("Sekil boş ola bilməz")
                .MinimumLength(3).WithMessage("Adres minimum 3 simvol ola biler");
        }
    }
}
