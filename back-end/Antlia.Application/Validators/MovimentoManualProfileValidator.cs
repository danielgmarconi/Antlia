using Antlia.Application.DTOs;
using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using FluentValidation;

namespace Antlia.Application.Validators
{
    public class MovimentoManualProfileValidator : AbstractValidator<MovimentoManualDTO>
    {
        public MovimentoManualProfileValidator(IProdutoRepository produtoRepository,
                                               IProdutoCosifRepository produtoCosifRepository,
                                               IMovimentoManualRepository _movimentoManualRepository)
        {
            RuleSet("Create", () => {
                RuleFor(x => x.ProdutoId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("ProdutoId is required.")
                    .NotEmpty().WithMessage("ProdutoId is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid ProdutoId.");
                RuleFor(x => x.ProdutoCosifId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("ProdutoCosifId is required.")
                    .NotEmpty().WithMessage("ProduProdutoCosifIdtoId is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid ProdutoCosifId.");
                RuleFor(x => x.Mes)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Mes is required.")
                    .NotEmpty().WithMessage("Mes is required.")
                    .InclusiveBetween(1, 12).WithMessage("Invalid Mes.");
                RuleFor(x => x.Ano)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Ano is required.")
                    .NotEmpty().WithMessage("Ano is required.")
                    .InclusiveBetween(1950, 2200).WithMessage("Invalid Ano.");
                RuleFor(x => x.NumeroLancamento)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("NumeroLancamento is required.")
                    .NotEmpty().WithMessage("NumeroLancamento is required.")
                    .GreaterThanOrEqualTo(11950).WithMessage("Invalid NumeroLancamento.");
                RuleFor(x => x.Valor)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Valor is required.")
                    .NotEmpty().WithMessage("Valor is required.")
                    .GreaterThanOrEqualTo(1).WithMessage("Invalid Valor, Valor must be greater than 1.");
                RuleFor(x => x.Descricao)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Descricao is required.")
                    .NotEmpty().WithMessage("Descricao is required.")
                    .MinimumLength(5).WithMessage("The Descricao must be at least 5 characters long.")
                    .MaximumLength(30).WithMessage("The Descricao must have a maximum of 50 characters.");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoId(produtoRepository, x.ProdutoId)).WithMessage("ProdutoId does not exist");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoCosifId(produtoCosifRepository, x.ProdutoId, x.ProdutoCosifId)).WithMessage("ProdutoCosifId does not exist");
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
                RuleFor(x => x.ProdutoCosifId)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("ProdutoCosifId is required.")
                    .NotEmpty().WithMessage("ProduProdutoCosifIdtoId is required.")
                    .Must(x => !x.HasValue || x > 0).WithMessage("Invalid ProdutoCosifId.");
                RuleFor(x => x.Mes)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Mes is required.")
                    .NotEmpty().WithMessage("Mes is required.")
                    .InclusiveBetween(1, 12).WithMessage("Invalid Mes.");
                RuleFor(x => x.Ano)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Ano is required.")
                    .NotEmpty().WithMessage("Ano is required.")
                    .InclusiveBetween(1950, 2200).WithMessage("Invalid Ano.");
                RuleFor(x => x.NumeroLancamento)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("NumeroLancamento is required.")
                    .NotEmpty().WithMessage("NumeroLancamento is required.")
                    .GreaterThanOrEqualTo(11950).WithMessage("Invalid NumeroLancamento.");
                RuleFor(x => x.Valor)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Valor is required.")
                    .NotEmpty().WithMessage("Valor is required.")
                    .GreaterThanOrEqualTo(1).WithMessage("Invalid Valor, Valor must be greater than 1.");
                RuleFor(x => x.Descricao)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("Descricao is required.")
                    .NotEmpty().WithMessage("Descricao is required.")
                    .MinimumLength(5).WithMessage("The Descricao must be at least 5 characters long.")
                    .MaximumLength(30).WithMessage("The Descricao must have a maximum of 50 characters.");
                RuleFor(x => x)
                    .Must(x => ValidateExistsId(_movimentoManualRepository, x.Id)).WithMessage("Id does not exist");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoId(produtoRepository, x.ProdutoId)).WithMessage("ProdutoId does not exist");
                RuleFor(x => x)
                    .Must(x => ValidateExistsProdutoCosifId(produtoCosifRepository, x.ProdutoId, x.ProdutoCosifId)).WithMessage("ProdutoCosifId does not exist");
            });
        }
        private bool ValidateExistsProdutoId(IProdutoRepository produtoRepository, int? id)
        {
            if (id != null && id.Value > 0)
            {
                var result = produtoRepository.Get(new Produto() { Id = id, Status = "A" }).Result;
                if (result == null || result.Count == 0)
                    return false;
            }
            return true;
        }
        private bool ValidateExistsProdutoCosifId(IProdutoCosifRepository produtoCosifRepository, int? produtoId, int? produtoCosifId)
        {
            if ((produtoId != null && produtoCosifId != null))
            {
                if (produtoId.Value > 0 && produtoCosifId.Value > 0)
                { 
                    var result = produtoCosifRepository.Get(new ProdutoCosif() { Id = produtoCosifId, ProdutoId = produtoId, Status = "A" }).Result;
                    if (result == null || result.Count == 0)
                        return false;
                }
            }
            return true;
        }
        private bool ValidateExistsId(IMovimentoManualRepository _movimentoManualRepository, int? id)
        {
            if (id != null && id.Value > 0)
            {
                var result = _movimentoManualRepository.Get(id.Value).Result;
                if (result == null)
                    return false;
            }
            return true;
        }
    }
}
