using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasarnasApp.Shared.Models;
using FluentValidation;

namespace BasarnasApp.Client.Validators
{
    public class JenisKejadianValidator : AbstractValidator<JenisKejadianRequest>
    {
        public JenisKejadianValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nama Tida Boleh Kosong");
            RuleFor(x => x.Instansis).Must(x=>x ==null || !x.Any()).WithMessage("Instansi Tida Boleh Kosong");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<JenisKejadianRequest>.CreateWithOptions((JenisKejadianRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}