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
    public class UsersDataTableParser : IDataParserService<IEnumerable<User>, DataTable>
    {
        public DataTable? Parse(IEnumerable<User> item)
        {
            if (item.Count() == 0)
                return null;

            DataTable dt = new();

            dt.Columns.Add("Id");
            dt.Columns.Add("Username");
            dt.Columns.Add("PasswordHash");
            dt.Columns.Add("Roles");

            foreach(User user in item)
            {
                DataRow rw = dt.NewRow();

                rw["Id"] = user.Id;
                rw["Username"] = user.Username;
                rw["PasswordHash"] = user.PasswordHash;

                IEnumerable<string>? roles = user?.UserRoles?.Select(x => x.Role.ToString());

                if(roles is not null)
                {
                    rw["Roles"] = String.Join(";", roles);
                }

                dt.Rows.Add(rw);
            }

            return dt;
        }

        public IEnumerable<User>? ParseBack(DataTable item)
        {
            if (item.Rows.Count == 0)
                return null;

            List<User> users = new();

            foreach(DataRow rw in item.Rows)
            {                
                string username = rw["Username"].ToString() ?? String.Empty;
                string passwordHash = rw["PasswordHash"].ToString() ?? String.Empty;
                
                User user = new(username, passwordHash);

                users.Add(user);
                
            }

            return users;
        }
    }
}
