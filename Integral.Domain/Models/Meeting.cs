namespace Integral.Domain.Models
{
    public class Meeting : DomainObject
    {
        public string Theme { get; set; }

        public string? Note { get; set; }

        public DateTime MeetingDate { get; set; }

        public Group? Group { get; set; }

        public int GroupId { get; set; }


        public Meeting(string theme, string? note, DateTime meetingDate)
        {
            Theme = theme;
            Note = note;
            MeetingDate = meetingDate;
        }
    }
}
