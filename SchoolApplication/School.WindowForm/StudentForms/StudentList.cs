using IRepository;
using Ninject;
using School.Dto.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School.WindowForm.StudentForms
{
    public partial class StudentList : Form
    {
        // private string emptyimage = @"\";
        // private string emptyimage = Path.Combine("download(2).jpg", @"D:\Internship\SchoolApplication\School.WindowForm\Resources\download(2).jpg", Path.GetFileName("download(2).jpg"));

        private readonly IStudent _StudentRepo;
        private readonly ICourse _CourseRepo;
        private readonly StandardKernel Kernal;
        private readonly int USER_ID;
        public StudentList(StandardKernel kernal, int userid)
        {
            _StudentRepo = kernal.Get<IStudent>();
            _CourseRepo = kernal.Get<ICourse>();
            Kernal = kernal;
            USER_ID = userid;
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            RefreshStudentTable();
            listBox1.DataSource = _CourseRepo.GetAll();
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "CourseId";
            listBox1.FormattingEnabled = true;
            txtImageUrl.Text = "";
            pbStudent.Image = Image.FromFile(@"D:\Internship\SchoolApplication\School.WindowForm\Resources\emptyimage.jpg");

        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {

            var selectedcourse = listBox1.CheckedItems;
            var properties = selectedcourse.GetType().GetProperties();
            AddEditStudentDto model = new AddEditStudentDto();
            model.Courses = new List<int>();
            foreach (var item in listBox1.CheckedItems)
            {
                model.Courses.Add(Convert.ToInt32(item.GetType().GetProperty("CourseId").GetValue(item, null)));
            }
            model.ImageUrl = txtImageUrl.Text;
            model.Name = TxtName.Text;
            model.FName = TxtFName.Text;
            model.Email = TxtEmail.Text;
            model.Phone = TxtStudentPhone.Text;
            model.Password = TxtPassword.Text;
            model.ConfirmPassword = TxtConfirmPassword.Text;
            model.Dob = DtpStudetDob.Text.ToString();
            model.UserId = USER_ID;
            _StudentRepo.AddStudent(model);
            RefreshStudentTable();
            MessageBox.Show("Student Added Successfully");
            RefreshStudentForm();
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex; var dg = sender;
            var ci = e.ColumnIndex;
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 10)
                {
                    var dresult = MessageBox.Show("Are you Sure to delete Student", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (DialogResult.Yes == dresult)
                    {
                        DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                        if (DialogResult.OK == MessageBox.Show("record deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information))
                        {
                            _StudentRepo.Delete(Convert.ToInt32(row.Cells[0].Value));
                            RefreshStudentTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Student Record Is Safe", "Safe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    //gets a collection that contains all the rows
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    var studentid = Convert.ToInt32(row.Cells[0].Value);
                    EditStudent f = new EditStudent(Kernal, studentid, USER_ID, this);
                    f.UpdateEventHandler += f2_UpdateStudentGridHandler;
                    f.ShowDialog();
                }
            }
        }

        #region RefreshMethods of form data
        public void RefreshStudentTable()
        {
            Pager pager = new Pager();
            pager.length = 100000000;
            pager.start = 0;
            pager.UserId = USER_ID;
            dataGridView1.AutoGenerateColumns = false;
            var model = _StudentRepo.GetStudents(pager);
            foreach (var item in model)
            {
                var StudentModel = (StudentDtoPost)(item);
                if (StudentModel != null)
                {
                    var i = 0;
                    foreach (var coursedto in StudentModel.StudentCourses)
                    {
                        if (i < StudentModel.StudentCourses.Count - 1)
                        {
                            StudentModel.CoursesStr += coursedto.Name + ",";
                        }
                        else
                        {

                            StudentModel.CoursesStr += coursedto.Name;
                        }
                        i++;
                    }
                }
            }
            dataGridView1.DataSource = model;
        }
        private void f2_UpdateStudentGridHandler(object sender, EditStudent.UpdateEventArgs args)
        {
            RefreshStudentTable();

        }
        public void RefreshStudentForm()
        {
            TxtName.Text = "";
            TxtFName.Text = "";
            TxtEmail.Text = "";
            TxtStudentPhone.Text = "";
            TxtPassword.Text = "";
            TxtConfirmPassword.Text = "";
            DtpStudetDob.Text = "";
            List<int> index = new List<int>();
            foreach (var item in listBox1.Items)
            {
                index.Add(Convert.ToInt32(listBox1.Items.IndexOf(item)));

            }
            foreach (var item in index)
            {
                listBox1.SetItemChecked(item, false);
            }
        }
        #endregion

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    //  filename = Guid.NewGuid() + Path.GetExtension(open.FileName);
                    pbStudent.Image = new Bitmap(open.FileName);
                    var path = Path.Combine(open.FileName, @"D:\Internship\SchoolApplication\School.WindowForm\Content\Images\", Path.GetFileName(open.FileName));
                    txtImageUrl.Text = path;
                    File.Copy(open.FileName, path, true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
