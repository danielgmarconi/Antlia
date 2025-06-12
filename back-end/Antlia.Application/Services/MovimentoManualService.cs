using Antlia.Application.Common;
using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Antlia.Application.Services
{
    public class MovimentoManualService : IMovimentoManualService
    {
        private readonly IMovimentoManualRepository _movimentoManualRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<MovimentoManualDTO> _validator;
        public MovimentoManualService(IMovimentoManualRepository movimentoManualRepository,
                                      IMapper mapper,
                                      IValidator<MovimentoManualDTO> validator)
        {
            _movimentoManualRepository = movimentoManualRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<MethodResponse> Create(MovimentoManualDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Create"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var entity = _mapper.Map<MovimentoManual>(model);
                entity.Validate();
                entity = await _movimentoManualRepository.Create(entity);
                result.Update(true, 201, "Created successfully", _mapper.Map<MovimentoManualDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
            }

        public async Task<MethodResponse> Get(int id)
        {
            var result = new MethodResponse();
            try
            {
                if (id <= 0)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<MovimentoManualDTO>(await _movimentoManualRepository.Get(id)));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Get(MovimentoManualDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<List<MovimentoManualDTO>>(await _movimentoManualRepository.Get(_mapper.Map<MovimentoManual>(model))));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Remove(int id)
        {
            var result = new MethodResponse();
            try
            {
                if (id <= 0)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                await _movimentoManualRepository.Remove(id);
                result.Update(true, 200, "Successfully executed");
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Update(MovimentoManualDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Update"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var entity = await _movimentoManualRepository.Get(model.Id.Value);
                entity.Update(model.ProdutoId.Value,
                              model.ProdutoCosifId,
                              model.Mes,
                              model.Ano,
                              model.NumeroLancamento,
                              model.Valor,
                              model.Descricao);
                await _movimentoManualRepository.Update(entity);
                result.Update(true, 200, "Created successfully", _mapper.Map<MovimentoManualDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
    }
}
