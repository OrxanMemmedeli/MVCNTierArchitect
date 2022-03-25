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
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator(IAdminService adminService)
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz")
                .MinimumLength(3).WithMessage("İstifadəçi adı minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("İstifadəçi adı maksimum 50 simvol ola biler");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş ola bilməz")
                .MinimumLength(3).WithMessage("Email minimum 3 simvol ola biler")
                .MaximumLength(50).WithMessage("Email maksimum 50 simvol ola biler")
                .EmailAddress().WithMessage("Email adrsi doğru yazılmayıb");
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
