using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Standard.Commands.UpdateStandard
{
    public class UpdateStandardCommand : IUpdateStandardCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IStandardRepository _standardRepository;

        public UpdateStandardCommand(IDatabaseService databaseService, IMapper mapper, IStandardRepository standardRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _standardRepository = standardRepository;
        }

        public async Task<Object> Execute(UpdateStandardDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _standardRepository.Update(id, _mapper.Map<StandardEntity>(model));

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Standard.CountAsync(item => item.standardId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateStandardDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

