using AutoMapper;
using MediatR;
using Messaging.Core;
using OnBoarding.api.Application.Commands;
using OnBoarding.api.Application.Events;
using OnBoarding.api.Application.QueryHandler;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Helper.Clients;
using OnBoarding.api.Models;
using OnBoarding.api.Repositories;

namespace OnBoarding.api.Application.CommandHandlers
{
    public class DeleteEmployeeCommandHanlder : IRequestHandler<DeleteEmployeeCommand, ApiResponse<bool>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMovieApiClient movieApiClient;
        private readonly IMapper mapper;
        private readonly IEventBus _bus;
        private readonly ILogger<CreateEmployeeCommandHanlder> logger;

        public DeleteEmployeeCommandHanlder(IEmployeeRepository employeeRepository, IMovieApiClient movieApiClient, IMapper mapper, IEventBus bus, ILogger<CreateEmployeeCommandHanlder> logger)
        {
            this.employeeRepository = employeeRepository;
            this.movieApiClient = movieApiClient;
            this.mapper = mapper;
            this._bus = bus;
            this.logger = logger;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await employeeRepository.Delete(request.Id);
            return new ApiResponse<bool>(true, StatusCodes.Status200OK);
        }
    }
}
