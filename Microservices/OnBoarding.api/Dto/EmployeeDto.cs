namespace OnBoarding.api.Dto
{
    public class EmployeeDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool VerificationDone { get; set; }
    }

    public class EmployeeCollectionDto
    {
        public EmployeeCollectionDto()
        {
             Employees = new List<EmployeeDto>();
        }

        public IEnumerable<EmployeeDto> Employees { get; set; }
    }
}
