using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasarnasApp.Shared.Models;
using FluentValidation;

namespace BasarnasApp.Client.Validators
{
    public class InstansiValidator : AbstractValidator<InstansiRequest>
    {
        public InstansiValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nama Tida Boleh Kosong");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<InstansiRequest>.CreateWithOptions((InstansiRequest)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}