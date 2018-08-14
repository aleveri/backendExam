using SB.Entities;
using SB.Interfaces;
using SB.Resources;
using System;
using System.Threading.Tasks;
using static SB.Entities.Enums;

namespace SB.Module
{
    public class UserEs : IUserEs
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unit;
        private readonly IResponseService _responseService;
        private readonly IExceptionHandler _exceptionHandler;

        public UserEs(IRepository<User> repository,
            IResponseService responseService,
            IExceptionHandler exceptionHandler,
            IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
            _responseService = responseService;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<IResponseService> Add(User entity)
        {
            try
            {
                entity.BirthDate = GenericUtils.GetDateZone(entity.BirthDate);
                _repository.Add(entity);
                await _unit.Commit();
                _responseService.EstablecerRespuesta(true);
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }

        public async Task<IResponseService> Delete(Guid id)
        {
            try
            {
                await _repository.Delete(id);
                await _unit.Commit();
                _responseService.EstablecerRespuesta(true);
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }

        public async Task<IResponseService> Get(Guid id)
        {
            try
            {
                _responseService.EstablecerRespuesta(true, await _repository.Get(id));
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }

        public async Task<IResponseService> List(Pagination param)
        {
            try
            {
                param = GenericUtils.ValidatePagination(param);
                _responseService.EstablecerRespuesta(true, await _repository.List(param.Page, param.PageSize));
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }

        public async Task<IResponseService> ListAsync(Pagination param)
        {
            try
            {
                param = GenericUtils.ValidatePagination(param);
                _responseService.EstablecerRespuesta(true, await _repository.List(x => x.Name.Contains(param.Criterion) ||
                    x.DocumentNumber.Contains(param.Criterion),
                    param.Page, 
                    param.PageSize));
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }

        public async Task<IResponseService> Update(User entity)
        {
            try
            {
                if (!await _repository.Any(x => x.Id.Equals(entity.Id)))
                    throw new ApplicationException(ErrorCodes.NotFoundEntity.ToString());
                entity.BirthDate = GenericUtils.GetDateZone(entity.BirthDate);
                _repository.Update(entity);
                await _unit.Commit();
                _responseService.EstablecerRespuesta(true);
                _unit.Dispose();
                return _responseService;
            }
            catch (Exception ex)
            {
                _responseService.Errores.Add(_exceptionHandler.GetMessage(ex));
                return _responseService;
            }
        }
    }
}
