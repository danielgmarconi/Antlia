using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Application.Common;
using Antlia.Application.DTOs;
using Antlia.Domain.Entities;

namespace Antlia.Application.Interfaces;

public interface IProdutoCosifService
{
    Task<MethodResponse> Get(int id);
    Task<MethodResponse> Get(ProdutoCosifDTO model);
    Task<MethodResponse> Create(ProdutoCosifDTO model);
    Task<MethodResponse> Update(ProdutoCosifDTO model);
    Task<MethodResponse> Remove(int id);
}
