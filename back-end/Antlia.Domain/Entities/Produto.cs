using Antlia.Domain.Validation;

namespace Antlia.Domain.Entities
{
    public sealed class Produto : Entity
    {
        public string? Descricao { get; set; }
        public string? Status { get; set; }
        public Produto() { }
        public Produto(string descricao,
                       string status)
        {
            Descricao = descricao;
            Status = status;
        }
        public void Update(string descricao,
                           string status)
        {
            DomainExceptionValidation.When(Id < 1, "Invalid Id.");
            Descricao = descricao;
            Status = status;
            DataCriacao = null;
            DataAlteracao = null;
            Validate();
        }
        public void Validate()
        {
            DomainExceptionValidation.When(Descricao == null, "Descricao is required.");
            DomainExceptionValidation.When(Descricao != null && Descricao.Length < 5, "The Descricao must be at least 5 characters long.");
            DomainExceptionValidation.When(Descricao != null && Descricao.Length > 30, "The Descricao must have a maximum of 30 characters.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Status), "Status is required.");
            DomainExceptionValidation.When(Status.Length > 1, "The Status must have a maximum of 1 characters.");
            DomainExceptionValidation.When(!(Status.Equals("A", StringComparison.OrdinalIgnoreCase) ||
                                             Status.Equals("I", StringComparison.OrdinalIgnoreCase)), "Status field must have value A = active or I = Inactive.");

        }
    }
}
