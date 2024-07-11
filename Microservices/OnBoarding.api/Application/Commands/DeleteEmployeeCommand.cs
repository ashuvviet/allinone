using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.api.Application.Responses;

namespace OnBoarding.api.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest<ApiResponse<bool>>
    {
        public string Id { get; set; }
    }
}