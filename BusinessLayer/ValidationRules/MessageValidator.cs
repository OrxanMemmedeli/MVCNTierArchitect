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
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.SenderEmail).NotEmpty().WithMessage("Göndərənin adresi boş ola bilməz")
                .MaximumLength(250).WithMessage("Göndərənin adresi maksimum 250 simvol ola biler")
                .EmailAddress().WithMessage("Göndərənin adresi düzgün yazılmayıb");

            RuleFor(x => x.ReceiverEmail).NotEmpty().WithMessage("Alıcının adresi boş ola bilməz")
                .MaximumLength(250).WithMessage("Alıcının adresi maksimum 250 simvol ola biler")
                .EmailAddress().WithMessage("Alıcının adresi düzgün yazılmayıb")
                .NotEqual(x => x.SenderEmail).WithMessage("Göndərən və alıcı eyni şəxs ola bilməz");

            RuleFor(x => x.Subject).NotEmpty().WithMessage("Başlıq boş ola bilməz")
                .MaximumLength(50).WithMessage("Başlıq maksimum 50 simvol ola biler")
                .MinimumLength(3).WithMessage("Başlıq minimum 3 simvol ola biler"); 
            
            RuleFor(x => x.MessageText).NotEmpty().WithMessage("Mesaj mətni boş ola bilməz")
                .MinimumLength(10).WithMessage("Mesaj mətni  minimum 10 simvol ola biler");
        }
    }
}
