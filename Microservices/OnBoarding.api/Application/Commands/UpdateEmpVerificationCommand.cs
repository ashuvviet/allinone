using MediatR;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Helper.HostedServices;

namespace OnBoarding.api.Application.Commands
{
    public class UpdateEmpVerificationCommand : IRequest<ApiResponse<int>>
    {
        public EmployeeValidationDetails Message { get; set; }
    }
}
