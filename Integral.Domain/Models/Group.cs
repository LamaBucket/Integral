using Integral.Domain.Models.Enums;

namespace Integral.Domain.Models
{
    public class Group : DomainObject
    {
        public string Name { get; set; }

        public int Grade { get; set; }

        public GroupType GroupType { get; set; }


        public User? Leader { get; set; }

        public int LeaderId { get; set; }


        public ICollection<Student>? Students { get; set; }

        public Group(string name, int grade, GroupType groupType)
        {
            Name = name;
            Grade = grade;
            GroupType = groupType;
        }
    }
}
