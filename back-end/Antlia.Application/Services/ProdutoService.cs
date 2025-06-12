using Antlia.Application.Common;
using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Antlia.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProdutoDTO> _validator;
        public ProdutoService(IProdutoRepository produtoRepository,
                              IMapper mapper,
                              IValidator<ProdutoDTO> validator)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<MethodResponse> Create(ProdutoDTO model)
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
                var entity = _mapper.Map<Produto>(model);
                entity.Validate();
                entity = await _produtoRepository.Create(entity);
                result.Update(true, 201, "Created successfully", _mapper.Map<ProdutoDTO>(entity));
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
                result.Update(true, 200, "Successfully executed", _mapper.Map<ProdutoDTO>(await _produtoRepository.Get(id)));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Get(ProdutoDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed", _mapper.Map<List<ProdutoDTO>>(await _produtoRepository.Get(_mapper.Map<Produto>(model))));
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
                await _produtoRepository.Remove(id);
                result.Update(true, 200, "Successfully executed");
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }

        public async Task<MethodResponse> Update(ProdutoDTO model)
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
                var entity = await _produtoRepository.Get(model.Id.Value);
                entity.Update(model.Descricao,
                              model.Status);
                await _produtoRepository.Update(entity);
                result.Update(true, 200, "Created successfully", _mapper.Map<ProdutoDTO>(entity));
            }
            catch (Exception e)
            {
                result.Update(500, "Error", e.Message);
            }
            return result;
        }
    }
}
