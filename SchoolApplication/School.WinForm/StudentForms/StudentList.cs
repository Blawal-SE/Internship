using IRepository;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School.WinForm.StudentForms
{
    public partial class StudentList : Form
    {
        private readonly IStudent _StudentRepo;
        private readonly StandardKernel Kernal;
        public StudentList(StandardKernel kernal)
        {
            _StudentRepo = kernal.Get<IStudent>();
            Kernal = kernal;
            InitializeComponent();
        }
        private void StudentList_Load(object sender, EventArgs e)
        {
            StudentGridView.DataSource = _StudentRepo.GetAll();
        }
    }
}
