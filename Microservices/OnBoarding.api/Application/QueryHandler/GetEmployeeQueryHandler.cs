using AutoMapper;
using MediatR;
using OnBoarding.api.Application.Queries;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Dto;
using OnBoarding.api.Models;
using OnBoarding.api.Repositories;

namespace OnBoarding.api.Application.QueryHandler
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, ApiResponse<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetEmployeeQueryHandler> logger;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<GetEmployeeQueryHandler> logger)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.Get();

            if (employees == null)
            {
                return new ApiResponse<IEnumerable<EmployeeDto>>("No employee exist", true, null, StatusCodes.Status400BadRequest);
            }

            var response = new ApiResponse<IEnumerable<EmployeeDto>>(mapper.Map<IEnumerable<EmployeeDto>>(employees), StatusCodes.Status200OK);
            return response;
        }
    }
}
