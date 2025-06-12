using Antlia.Application.DTOs;
using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using FluentValidation;

namespace Antlia.Application.Validators
{
    public class ProdutoCosifProfileValidator : AbstractValidator<ProdutoCosifDTO>
    {
        public ProdutoCosifProfileValidator(IProdutoRepository produtoRepository,
                                            IProdutoCosifRepository produtoCosifRepository)
        {
            RuleSet("Create", () => {
                RuleFor(x => x.ProdutoId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("ProdutoId is required.")
                    .NotEmpty().WithMessage("ProdutoId is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid ProdutoId.");
                RuleFor(x => x.Codigo)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Codigo is required.")
                    .NotEmpty().WithMessage("Codigo is required.")
                    .MinimumLength(1).WithMessage("The Codigo must be at least 1 characters long.")
                    .MaximumLength(11).WithMessage("The Codigo must have a maximum of 11 characters.");
                RuleFor(x => x.Classificacao)
                    .Cascade(CascadeMode.Stop)
                    .MinimumLength(1).WithMessage("The Codigo must be at least 1 characters long.")
                    .MaximumLength(6).WithMessage("The Codigo must have a maximum of 6 characters.")
                    .When(x => !string.IsNullOrWhiteSpace(x.Classificacao));
                RuleFor(x => x.Status)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Status is required.")
                    .NotEmpty().WithMessage("Status is required.")
                    .Must(x => x != null && (x.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             x.Equals("I", StringComparison.OrdinalIgnoreCase))).WithMessage("Status field must have value A = active or I = Inactive.");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoId(produtoRepository, x.ProdutoId)).WithMessage("ProdutoId does not exist");

            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Id is required.")
                    .NotEmpty().WithMessage("Id is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid Id.");
                RuleFor(x => x.ProdutoId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("ProdutoId is required.")
                    .NotEmpty().WithMessage("ProdutoId is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid ProdutoId.");
                RuleFor(x => x.Codigo)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Codigo is required.")
                    .NotEmpty().WithMessage("Codigo is required.")
                    .MinimumLength(1).WithMessage("The Codigo must be at least 1 characters long.")
                    .MaximumLength(11).WithMessage("The Codigo must have a maximum of 11 characters.");
                RuleFor(x => x.Classificacao)
                    .Cascade(CascadeMode.Stop)
                    .MinimumLength(1).WithMessage("The Codigo must be at least 1 characters long.")
                    .MaximumLength(6).WithMessage("The Codigo must have a maximum of 6 characters.")
                    .When(x => !string.IsNullOrWhiteSpace(x.Classificacao));
                RuleFor(x => x.Status)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Status is required.")
                    .NotEmpty().WithMessage("Status is required.")
                    .Must(x => x != null && (x.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             x.Equals("I", StringComparison.OrdinalIgnoreCase))).WithMessage("Status field must have value A = active or I = Inactive.");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoId(produtoRepository, x.ProdutoId)).WithMessage("ProdutoId does not exist");
                RuleFor(x => x)
                    .Must(x => ValidateExistsId(produtoCosifRepository, x.Id)).WithMessage("Id does not exist");
            });
        }
        private bool ValidateExistsProdutoId(IProdutoRepository produtoRepository, int? id)
        {
            if (id != null && id.Value > 0)
            {
                var result = produtoRepository.Get(new Produto() { Id = id, Status= "A"}).Result;
                if (result == null || result.Count == 0)
                    return false;
            }
            return true;
        }
        private bool ValidateExistsId(IProdutoCosifRepository produtoCosifRepository, int? id)
        {
            if (id != null && id.Value > 0)
            {
                var result = produtoCosifRepository.Get(id.Value).Result;
                if (result == null)
                    return false;
            }
            return true;
        }
    }
}
