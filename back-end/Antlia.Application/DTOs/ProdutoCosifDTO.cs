using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antlia.Application.DTOs
{
    public sealed class ProdutoCosifDTO
    {
        public int? Id { get; set; }
        public int? ProdutoId { get; set; }
        public string? Codigo { get; set; }
        public string? Classificacao { get; set; }
        public string? Status { get; set; }
        public ProdutoDTO? Produto { get; set; }
    }
}
