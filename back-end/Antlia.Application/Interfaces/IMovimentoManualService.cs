using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Application.Common;
using Antlia.Application.DTOs;
using Antlia.Domain.Entities;

namespace Antlia.Application.Interfaces;

public interface IMovimentoManualService
{
    Task<MethodResponse> Get(int id);
    Task<MethodResponse> Get(MovimentoManualDTO model);
    Task<MethodResponse> Create(MovimentoManualDTO model);
    Task<MethodResponse> Update(MovimentoManualDTO model);
    Task<MethodResponse> Remove(int id);
}
