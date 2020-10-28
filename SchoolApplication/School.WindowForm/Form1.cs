using IRepository;
using Ninject;
using School.WindowForm.StudentForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School.WindowForm
{
    public partial class Form1 : Form
    {
        private readonly IUser _StudentRepo;
        private readonly StandardKernel Kernal;
        public Form1(StandardKernel kernal)
        {
            _StudentRepo = kernal.Get<IUser>();
            Kernal = kernal;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var user = _StudentRepo.ValidateUser(TxtUname.Text, TxtPassword.Text);
            if (user != null)
            {
                this.Hide();
                StudentList f = new StudentList(Kernal, user.Id);
                f.Show();
            }
            else
            {
                MessageBox.Show("userName or password is wrong");
            }
        }
    }
}
