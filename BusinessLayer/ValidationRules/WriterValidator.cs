using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator(IWriterService writerService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad boş ola bilməz")
                .MinimumLength(3).WithMessage("Ad minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Ad maksimum 50 simvol ola biler");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş ola bilməz")
                .MinimumLength(3).WithMessage("Soyad minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Soyad maksimum 50 simvol ola biler");
            RuleFor(x => x.ImageURL).MaximumLength(250).WithMessage("Fayl Yolu maksimum 250 simvol ola biler");
            RuleFor(x => x.Email).MaximumLength(250).WithMessage("Email maksimum 250 simvol ola biler");
            RuleFor(x => x.About).MaximumLength(100).WithMessage("Haqqinda maksimum 100 simvol ola biler")
                .MinimumLength(10).WithMessage("Haqqinda minimum 10 simvol ola biler");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol ola bilər. (nümunə Aa123!!)")
                .MaximumLength(20).WithMessage("Şifrə maksimum 20 simvol ola bilər. (nümunə Aa123!!)")
                .Matches("[A-Z]").WithMessage("Şifrədə ən azı 1 BÖYÜK simvol olmalıdır (nümunə Aa123!!)")
                .Matches("[a-z]").WithMessage("Şifrədə ən azı 1 KİÇİK simvol olmalıdır (nümunə Aa123!!)")
                .Matches("[0-9]").WithMessage("Şifrədə ən azı 1 RƏQƏM olmalıdır (nümunə Aa123!!)")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifrədə xüsusi simvollar olmalıdır. (nümunə Aa123!!)");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifrənin təkrarı boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol ola bilər. (nümunə Aa123!!)")
                .MaximumLength(20).WithMessage("Şifrə maksimum 20 simvol ola bilər. (nümunə Aa123!!)");

            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword).WithMessage("Şifrə ilə təkrarı arasında uyğunsuzluq var.")
                .When(x => !String.IsNullOrWhiteSpace(x.Password)).WithMessage("Şifrə ilə təkrarı arasında uyğunsuzluq var.");


        }
    }
}
