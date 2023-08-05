using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasarnasApp.Shared.Models;
using FluentValidation;

namespace BasarnasApp.Client.Validators
{
    public class PihakTerkaitValidator : AbstractValidator<PihakTerkaitRequest>
    {
        public PihakTerkaitValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nama Tida Boleh Kosong");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email Tida Boleh Kosong");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Keterangan Tida Boleh Kosong");
            RuleFor(x => x.Instansi).NotNull().WithMessage("Instansi Tida Boleh Kosong");
            RuleFor(x => x.District).NotNull().WithMessage("District Tida Boleh Kosong");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<PihakTerkaitRequest>.CreateWithOptions((PihakTerkaitRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}