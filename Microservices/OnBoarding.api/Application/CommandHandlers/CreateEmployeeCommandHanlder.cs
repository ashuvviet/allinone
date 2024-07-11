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
    public class CreateEmployeeCommandHanlder : IRequestHandler<CreateEmployeeCommand, ApiResponse<Guid>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMovieApiClient movieApiClient;
        private readonly IMapper mapper;
        private readonly IEventBus _bus;
        private readonly ILogger<CreateEmployeeCommandHanlder> logger;

        public CreateEmployeeCommandHanlder(IEmployeeRepository employeeRepository, IMovieApiClient movieApiClient, IMapper mapper, IEventBus bus, ILogger<CreateEmployeeCommandHanlder> logger)
        {
            this.employeeRepository = employeeRepository;
            this.movieApiClient = movieApiClient;
            this.mapper = mapper;
            this._bus = bus;
            this.logger = logger;
        }

        public async Task<ApiResponse<Guid>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var empoyee = mapper.Map<Employee>(request.Employee);
            var empId = await employeeRepository.AddEmployee(empoyee);


            //_bus.Publish(new AddNewEmplyeeEvent() { Email = empoyee.Email, Id = Guid.NewGuid().ToString(), Name = empoyee.FirstName });

            //var moviesDetails = await movieApiClient.GetMovieByCity("cebu");

            return new ApiResponse<Guid>(empId, StatusCodes.Status200OK);
        }
    }
}
