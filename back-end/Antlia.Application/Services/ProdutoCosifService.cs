using Antlia.Application.Common;
using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Antlia.Application.Services
{
    public class ProdutoCosifService : IProdutoCosifService
    {
        private readonly IProdutoCosifRepository _produtoCosifRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProdutoCosifDTO> _validator;
        public ProdutoCosifService(IProdutoCosifRepository produtoCosifRepository,
                                   IMapper mapper,
                                   IValidator<ProdutoCosifDTO> validator)
        {
            _produtoCosifRepository = produtoCosifRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<MethodResponse> Create(ProdutoCosifDTO model)
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
                var entity = _mapper.Map<ProdutoCosif>(model);
                entity.Validate();
                entity = await _produtoCosifRepository.Create(entity);
                result.Update(true, 201, "Created successfully", _mapper.Map<ProdutoCosifDTO>(entity));
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
                result.Update(true, 200, "Successfully executed", _mapper.Map<ProdutoCosifDTO>(await _produtoCosifRepository.Get(id)));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Get(ProdutoCosifDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<List<ProdutoCosifDTO>>(await _produtoCosifRepository.Get(_mapper.Map<ProdutoCosif>(model))));
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
                await _produtoCosifRepository.Remove(id);
                result.Update(true, 200, "Successfully executed");
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Update(ProdutoCosifDTO model)
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
                var entity = await _produtoCosifRepository.Get(model.Id.Value);
                entity.Update(model.ProdutoId,
                              model.Codigo,
                              model.Classificacao,
                              model.Status);
                await _produtoCosifRepository.Update(entity);
                result.Update(true, 200, "Created successfully", _mapper.Map<ProdutoCosifDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
    }
}
