using IRepository;
using Ninject;
using School.WinForm.StudentForms;
using System;

using System.Windows.Forms;

namespace School.WinForm
{
    public partial class LoginPage : Form
    {
        private readonly IUser _UserRoleRepo;
        private readonly StandardKernel Kernal;
        public LoginPage(StandardKernel kernal)
        {
            _UserRoleRepo = kernal.Get<IUser>();
            Kernal = kernal;
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            var user = _UserRoleRepo.ValidateUser(textBoxUname.Text, TxtPassword.Text);
            if (user != null)
            {
                this.Hide();
                StudentList studentform = new StudentList(Kernal);
                studentform.ShowDialog();

            }
            else
            {
                MessageBox.Show("user name or password is wrong");
            }

        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }
    }
}
