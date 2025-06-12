using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antlia.Application.DTOs
{
    public sealed class ProdutoDTO
    {
        public int? Id { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set; }
    }
}
