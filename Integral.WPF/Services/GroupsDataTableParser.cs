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
    public class GroupsDataTableParser : IDataParserService<IEnumerable<Group>, DataTable>
    {
        public DataTable? Parse(IEnumerable<Group> item)
        {
            if (item.Count() == 0)
                return null;

            DataTable dt = new();

            dt.Columns.Add("LeaderUsername");
            dt.Columns.Add("Name");
            dt.Columns.Add("Grade");
            dt.Columns.Add("GroupType");
            dt.Columns.Add("Id");

            foreach (Group group in item)
            {
                if(group.Leader is not null)
                {
                    DataRow rw = dt.NewRow();

                    rw["LeaderUsername"] = group.Leader.Username;
                    rw["Name"] = group.Name;
                    rw["Grade"] = group.Grade;
                    rw["GroupType"] = group.GroupType;
                    rw["Id"] = group.Id;

                    dt.Rows.Add(rw);
                }
            }

            return dt;
        }

        public IEnumerable<Group>? ParseBack(DataTable item)
        {
            if (item.Rows.Count == 0)
                return null;

            List<Group> groups = new();

            foreach (DataRow rw in item.Rows)
            {
                string? _grade = rw["Grade"].ToString();

                if (String.IsNullOrEmpty(_grade))
                    continue;

                string leaderUsername = rw["LeaderUsername"].ToString() ?? String.Empty;

                string name = rw["Name"].ToString() ?? String.Empty;
                int grade = Convert.ToInt32(_grade);

                string _groupType = rw["GroupType"].ToString() ?? String.Empty;

                if(Enum.TryParse<GroupType>(_groupType, true, out GroupType groupType))
                {
                    Group group = new(name, grade, groupType) 
                    { 
                        Leader = new(leaderUsername, String.Empty)
                    };

                    groups.Add(group);
                }
                
            }

            return groups;
        }
    }
}
