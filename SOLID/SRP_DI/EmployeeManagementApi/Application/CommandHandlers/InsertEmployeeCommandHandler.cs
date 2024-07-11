using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Models;
using EmployeeManagementApi.Application.Commands;
using MediatR;

namespace EmployeeManagementApi.Application.CommandHandlers
{
    public class InsertEmployeeCommandHandler : IRequestHandler<InsertEmployeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMailService _mailService;

        public InsertEmployeeCommandHandler(IEmployeeRepository employeeRepo, IMailService mailService)
        {
            _employeeRepository = employeeRepo;
            _mailService = mailService;
        }
        public async Task<int> Handle(InsertEmployeCommand request, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.InsertEmployee(new FullTimeEmployee() { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email });
            // send mail to finance / insurance team           
            await _mailService.SendMail("finance@xyz.com", "Welcome", "Welcome To xyz");

            return result;
        }
    }
}
