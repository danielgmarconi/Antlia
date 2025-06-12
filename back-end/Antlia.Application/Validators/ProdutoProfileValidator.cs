using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Application.DTOs;
using Antlia.Domain.Interfaces;
using FluentValidation;

namespace Antlia.Application.Validators
{
    public class ProdutoProfileValidator : AbstractValidator<ProdutoDTO>
    {
        public ProdutoProfileValidator(IProdutoRepository produtoRepository)
        {
            RuleSet("Create", () => {
                RuleFor(x => x.Descricao)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Descricao is required.")
                    .NotEmpty().WithMessage("Descricao is required.")
                    .MinimumLength(5).WithMessage("The Descricao must be at least 5 characters long.")
                    .MaximumLength(30).WithMessage("The Descricao must have a maximum of 30 characters.");
                RuleFor(x => x.Status)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Status is required.")
                    .NotEmpty().WithMessage("Status is required.")
                    .Must(x => x != null && (x.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             x.Equals("I", StringComparison.OrdinalIgnoreCase))).WithMessage("Status field must have value A = active or I = Inactive.");

            });
            RuleSet("Update", () => {
                RuleFor(x => x.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Id is required.")
                    .NotEmpty().WithMessage("Id is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid Id.");
                RuleFor(x => x.Descricao)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Descricao is required.")
                    .NotEmpty().WithMessage("Descricao is required.")
                    .MinimumLength(5).WithMessage("The Descricao must be at least 5 characters long.")
                    .MaximumLength(30).WithMessage("The Descricao must have a maximum of 30 characters.");
                RuleFor(x => x.Status)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Status is required.")
                    .NotEmpty().WithMessage("Status is required.")
                    .Must(x => x != null && (x.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             x.Equals("I", StringComparison.OrdinalIgnoreCase))).WithMessage("Status field must have value A = active or I = Inactive.");
                RuleFor(x => x)
                    .Must(x => ValidateExistsId(produtoRepository, x.Id)).WithMessage("Id does not exist");
            });
        }
        private bool ValidateExistsId(IProdutoRepository produtoRepository, int? id)
        {
            if (id != null && id.Value > 0)
            {
                var result = produtoRepository.Get(id.Value).Result;
                if (result == null)
                    return false;
            }
            return true;
        }
    }
}
