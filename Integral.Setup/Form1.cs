using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.EntityFramework;
using Integral.EntityFramework.Services;

namespace Integral.Setup
{
    public partial class Setup : Form
    {
        private IPasswordHashService PasswordHasher;

        public Setup()
        {
            InitializeComponent();

            PasswordHasher = new PasswordHashService();
        }

        private async void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            User user = new User(txtUsername.Text, PasswordHasher.HashPassword(txtPassword.Text));

            user.UserRoles = new List<UserRole>();

            user.UserRoles.Add(new UserRole() { Role = Domain.Models.Enums.Role.Admin });

            IntegralDbContextFactory.ConnString = txtDbConnString.Text;

            IntegralDbContextFactory factory = new();

            using (IntegralDbContext context = factory.CreateDbContext())
            {
                if(cbxRecreateIfExists.Checked)
                    await context.Database.EnsureDeletedAsync();

                await context.Database.EnsureCreatedAsync();

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Integral");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string filePath = Path.Combine(dir, "IntegralDbConnectionString.txt");

            await File.WriteAllTextAsync(filePath, txtDbConnString.Text);

            MessageBox.Show("Task Completed Successfully.");
        }
    }
}