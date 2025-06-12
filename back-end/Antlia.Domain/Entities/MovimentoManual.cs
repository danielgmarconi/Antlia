using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Domain.Validation;

namespace Antlia.Domain.Entities
{
    public sealed class MovimentoManual : Entity
    {
        public int? ProdutoId { get; set; }
        public int? ProdutoCosifId { get; set; }
        public int? Mes { get; set; }
        public int? Ano { get; set; }
        public int? NumeroLancamento { get; set; }
        public decimal? Valor { get; set; }
        public string? Descricao { get; set; }
        public Produto? Produto { get; set; }
        public ProdutoCosif? ProdutoCosif { get; set; }
        public MovimentoManual(){}
        public MovimentoManual(int? produtoId,
                               int? produtoCosifId,
                               int? mes,
                               int? ano,
                               int? numeroLancamento,
                               decimal? valor,
                               string? descricao)
        {
            ProdutoId = produtoId;
            ProdutoCosifId = produtoCosifId;
            Mes = mes;
            Ano = ano;
            NumeroLancamento = numeroLancamento;
            Valor = valor;
            Descricao = descricao;
        }
        public void Update(int? produtoId,
                           int? produtoCosifId,
                           int? mes,
                           int? ano,
                           int? numeroLancamento,
                           decimal? valor,
                           string descricao)
        {
            DomainExceptionValidation.When(Id < 1, "Invalid Id.");
            ProdutoId = produtoId;
            ProdutoCosifId = produtoCosifId;
            Mes = mes;
            Ano = ano;
            NumeroLancamento = numeroLancamento;
            Valor = valor;
            Descricao = descricao;
            DataCriacao = null;
            DataAlteracao = null;
            Produto = null;
            ProdutoCosif = null;
            Validate();
        }
        public void Validate()
        {
            DomainExceptionValidation.When(ProdutoId == null || ProdutoId < 1, "Invalid ProdutoId.");
            DomainExceptionValidation.When(ProdutoCosifId == null || ProdutoCosifId < 1, "Invalid ProdutoCosifId.");
            DomainExceptionValidation.When(Mes == null || !( Mes >= 1 && Mes <= 12), "Invalid Mes.");
            DomainExceptionValidation.When(Ano == null || !(Ano >= 1950 && Ano <= 2200), "Invalid Ano.");
            DomainExceptionValidation.When(NumeroLancamento == null || NumeroLancamento < 11950, "Invalid NumeroLancamento.");
            DomainExceptionValidation.When(Valor == null || Valor < 1, "Invalid Valor, Valor must be greater than 1.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Descricao), "Descricao is required.");
            DomainExceptionValidation.When(Descricao.Length < 5 || Descricao.Length > 50, "Descricao cannot be shorter than 5 characters or longer than 50 characters.");
        }

    }
}
