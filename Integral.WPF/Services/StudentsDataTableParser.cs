using Integral.Domain.Models;
using Integral.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class StudentsDataTableParser : IDataParserService<IEnumerable<Student>, DataTable>
    {
        public DataTable? Parse(IEnumerable<Student> item)
        {
            if (item.Count() == 0)
                return null;

            DataTable dt = new();

            dt.Columns.Add("FirstName");
            dt.Columns.Add("SecondName");
            dt.Columns.Add("ThirdName");
            dt.Columns.Add("Id");
            
            foreach (Student student in item)
            {
                DataRow rw = dt.NewRow();

                rw["FirstName"] = student.FirstName;
                rw["SecondName"] = student.SecondName;
                rw["ThirdName"] = student.ThirdName;
                rw["Id"] = student.Id;

                dt.Rows.Add(rw);
            }

            return dt;
        }

        public IEnumerable<Student>? ParseBack(DataTable item)
        {
            if (item.Rows.Count == 0)
                return null;

            List<Student> students = new();

            foreach (DataRow rw in item.Rows)
            {
                string firstName = rw["FirstName"].ToString() ?? String.Empty;
                string secondName = rw["SecondName"].ToString() ?? String.Empty;
                string? thirdName = rw["ThirdName"].ToString();

                Student student = new(firstName, secondName, thirdName);

                students.Add(student);
                
            }

            return students;
        }
    }
}
