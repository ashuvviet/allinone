using AutoMapper;
using MediatR;
using OnBoarding.api.Application.Commands;
using OnBoarding.api.Application.QueryHandler;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Models;
using OnBoarding.api.Repositories;

namespace OnBoarding.api.Application.CommandHandlers
{
    public class UpdateEmpVerificationCommandHanlder : IRequestHandler<UpdateEmpVerificationCommand, ApiResponse<int>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateEmpVerificationCommandHanlder> logger;

        public UpdateEmpVerificationCommandHanlder(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<UpdateEmpVerificationCommandHanlder> logger)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ApiResponse<int>> Handle(UpdateEmpVerificationCommand request, CancellationToken cancellationToken)
        {
            await employeeRepository.UpdateVerification(request.Message.Email, request.Message.Status);
            return new ApiResponse<int>(1, StatusCodes.Status200OK);
        }
    }
}
