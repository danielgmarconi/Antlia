using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antlia.Domain.Entities
{
    public class Entity
    {
        public int? Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
