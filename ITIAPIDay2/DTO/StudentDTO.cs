#nullable disable

using ITIAPIDay2.Models;

namespace ITIAPIDay2.DTO
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public string DepartmentName { get; set; }
        public string? SupervisorName { get; set; }
    }
}
