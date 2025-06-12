using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Domain.Validation;

namespace Antlia.Domain.Entities
{
    public sealed class ProdutoCosif : Entity
    {
        public int? ProdutoId { get; set; }
        public string? Codigo { get; set; }
        public string? Classificacao { get; set; }
        public string? Status { get; set; }
        public Produto? Produto { get; set; }
        public ProdutoCosif() { }
        public ProdutoCosif(int? produtoId,
                            string codigo,
                            string classificacao,
                            string status)
        {
            ProdutoId = produtoId;
            Codigo = codigo;
            Classificacao = classificacao;
            Status = status;
        }
        public void Update(int? produtoId,
                           string codigo,
                           string classificacao,
                           string status)
        {
            DomainExceptionValidation.When(Id < 1, "Invalid Id.");
            ProdutoId = produtoId;
            Codigo = codigo;
            Classificacao = classificacao;
            Status = status;
            Produto = null;
            DataCriacao = null;
            DataAlteracao = null;
            Validate();
        }
        public void Validate()
        {
            DomainExceptionValidation.When(ProdutoId == null || ProdutoId < 1, "Invalid ProdutoId.");
            DomainExceptionValidation.When(Codigo == null, "Codigo is required.");
            DomainExceptionValidation.When(Codigo != null && Codigo.Length < 1, "The Codigo must be at least 1 characters long.");
            DomainExceptionValidation.When(Codigo != null && Codigo.Length > 11, "The Codigo must have a maximum of 11 characters.");
            DomainExceptionValidation.When(Classificacao != null && Classificacao.Length < 1, "The Classificacao must be at least 1 characters long.");
            DomainExceptionValidation.When(Classificacao != null && Classificacao.Length > 6, "The Classificacao must be at least 6 characters long.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Status), "Status is required.");
            DomainExceptionValidation.When(Status.Length > 1, "The Status must have a maximum of 1 characters.");
            DomainExceptionValidation.When(!(Status.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             Status.Equals("I", StringComparison.OrdinalIgnoreCase)), "Status field must have value A = active or I = Inactive.");

        }
    }
}
