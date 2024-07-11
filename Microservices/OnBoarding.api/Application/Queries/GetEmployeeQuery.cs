using MediatR;
using OnBoarding.api.Application.Responses;
using OnBoarding.api.Dto;
using OnBoarding.api.Models;

namespace OnBoarding.api.Application.Queries
{
    public class GetEmployeeQuery : IRequest<ApiResponse<IEnumerable<EmployeeDto>>>
    {
    }
}
