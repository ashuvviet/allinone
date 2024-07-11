using MediatR;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Dto;

namespace OnBoarding.api.Application.Commands
{
    public class CreateEmployeeCommand : IRequest<ApiResponse<Guid>>
    {
        public EmployeeDto Employee { get; set; }
    }
}
