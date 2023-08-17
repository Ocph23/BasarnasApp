using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasarnasApp.Shared.Models;
using FluentValidation;

namespace BasarnasApp.Client.Validators
{
    public class PenangananValidator : AbstractValidator<PenangananRequest>
    {
        public PenangananValidator()
        {
            RuleFor(x => x.Instansi).NotEmpty().WithMessage("Instansi Yang Menangani Tida Boleh Kosong");
            RuleFor(x => x.PihakTerkait).NotEmpty().WithMessage("Pihak Yang Menangani Tida Boleh Kosong");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<PenangananRequest>.CreateWithOptions((PenangananRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}