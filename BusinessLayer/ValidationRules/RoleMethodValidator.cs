using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class RoleMethodValidator : AbstractValidator<RoleMethod>
    {
        public RoleMethodValidator()
        {
            RuleFor(x => x.RoleID).NotEmpty().WithMessage("RoleID boş ola bilməz");
            RuleFor(x => x.MethodNameID).NotEmpty().WithMessage("MethodNameID boş ola bilməz");
        }
    }
}
