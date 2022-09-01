using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class MeetingsDataTableParser : IDataParserService<IEnumerable<Meeting>, DataTable>
    {
        public DataTable? Parse(IEnumerable<Meeting> item)
        {
            if (item.Count() == 0)
                return null;

            DataTable dt = new();

            dt.Columns.Add("GroupGrade");
            dt.Columns.Add("GroupName");
            dt.Columns.Add("Theme");
            dt.Columns.Add("Note");
            dt.Columns.Add("Date");
            dt.Columns.Add("Id");

            foreach (Meeting meeting in item)
            {
                if(meeting.Group is not null)
                {
                    DataRow rw = dt.NewRow();

                    rw["GroupGrade"] = meeting.Group.Grade;
                    rw["GroupName"] = meeting.Group.Name;
                    rw["Theme"] = meeting.Theme;
                    rw["Note"] = meeting.Note;
                    rw["Date"] = meeting.MeetingDate.ToString();
                    rw["Id"] = meeting.Id;

                    dt.Rows.Add(rw);
                }
            }

            return dt;
        }

        public IEnumerable<Meeting>? ParseBack(DataTable item)
        {
            if (item.Rows.Count == 0)
                return null;

            List<Meeting> meetings = new();

            foreach (DataRow rw in item.Rows)
            {
                string? _groupGrade = rw["GroupGrade"].ToString();

                if (String.IsNullOrEmpty(_groupGrade))
                    continue;

                int groupGrade = Convert.ToInt32(_groupGrade);
                string groupName = rw["GroupName"].ToString() ?? String.Empty;

                string theme = rw["Theme"].ToString() ?? String.Empty;
                string? note = rw["Note"].ToString();

                string _dateTime = rw["Date"].ToString() ?? String.Empty;
                
                if(DateTime.TryParse(_dateTime, out DateTime dateTime))
                {
                    Meeting meeting = new(theme, note, dateTime)
                    {
                        Group = new(groupName, groupGrade, GroupType.Class)
                    };

                    meetings.Add(meeting);
                }
                
            }

            return meetings;
        }
    }
}
