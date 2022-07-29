namespace Integral.Domain.Models
{
    public class Student : DomainObject
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string? ThirdName { get; set; }

        public ICollection<Group>? Groups { get; set; }

        public Student(string firstName, string secondName, string? thirdName)
        {
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
        }
    }
}
