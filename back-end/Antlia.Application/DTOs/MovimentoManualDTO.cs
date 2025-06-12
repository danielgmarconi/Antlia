using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antlia.Application.DTOs
{
    public sealed class MovimentoManualDTO
    {
        public int? Id { get; set; }
        public int? ProdutoId { get; set; }
        public int? ProdutoCosifId { get; set; }
        public int? Mes { get; set; }
        public int? Ano { get; set; }
        public int? NumeroLancamento { get; set; }
        public decimal? Valor { get; set; }
        public string? Descricao { get; set; }
        public ProdutoDTO? Produto { get; set; }
        public ProdutoCosifDTO? ProdutoCosif { get; set; }
    }
}
