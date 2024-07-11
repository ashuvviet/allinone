using System.ComponentModel.DataAnnotations.Schema;

namespace OnBoarding.api.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
