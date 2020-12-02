using IRepository;
using Ninject;
using School.Dto.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School.WindowForm.StudentForms
{
    public partial class EditStudent : Form
    {
        private string emptyImage = @"D:\Internship\SchoolApplication\School.WindowForm\Resources\emptyimage.jpg";
        private readonly IStudent _StudentRepo;
        private readonly ICourse _CourseRepo;
        private readonly StandardKernel Kernal;
        private readonly int USER_ID;
        private readonly Form StudentForm;
        private readonly int StudentId;
        public EditStudent(StandardKernel kernal, int studentid, int userId, Form Studentform)
        {
            Kernal = kernal;
            StudentId = studentid;
            USER_ID = userId;
            _StudentRepo = kernal.Get<IStudent>();
            _CourseRepo = kernal.Get<ICourse>();
            StudentForm = Studentform;

            InitializeComponent();
        }

        public delegate void UpdateDelegate(object sender, UpdateEventArgs args);
        public event UpdateDelegate UpdateEventHandler;
        public class UpdateEventArgs : EventArgs
        {
            public String Data { get; set; }
        }
        protected void RefreshStudentGridView()
        {
            UpdateEventArgs args = new UpdateEventArgs();
            UpdateEventHandler.Invoke(this, args);
        }
        private void BtnAddStudent_Click(object sender, EventArgs e)
        {

        }
        private void EditStudent_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = _CourseRepo.GetAll();
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "CourseId";
            listBox1.FormattingEnabled = true;

            var model = _StudentRepo.FindStudent(StudentId, USER_ID);
            TxtName.Text = model.Name;
            TxtFName.Text = model.FName;
            TxtStudentPhone.Text = model.Phone;
            DtpStudetDob.Text = model.Dob;
            TxtEmail.Text = model.Email;
            TxtPassword.Text = model.Password;
            if (model.ImageUrl != null)
            {
                pbStudent.Load(model.ImageUrl);
            }
            else
            {
                pbStudent.Load(emptyImage);
            }
            TxtConfirmPassword.Text = model.ConfirmPassword;
            List<int> indexes = new List<int>();
            foreach (var item in listBox1.Items)
            {
                foreach (var course in model.StudentCourses)
                {
                    if (Convert.ToInt32(item.GetType().GetProperty("CourseId").GetValue(item, null)) == course.CourseId)
                    {
                        indexes.Add(listBox1.Items.IndexOf(item));
                    }
                }
            }
            if (indexes != null)
            {
                foreach (var index in indexes)
                {
                    listBox1.SetItemChecked(index, true);
                }
            }
        }

        private void BtnEditStudent_Click(object sender, EventArgs e)
        {
            AddEditStudentDto model = new AddEditStudentDto();
            model.Courses = new List<int>();
            foreach (var item in listBox1.CheckedItems)
            {
                model.Courses.Add(Convert.ToInt32(item.GetType().GetProperty("CourseId").GetValue(item, null)));
            }
            model.Id = StudentId;
            model.Name = TxtName.Text;
            model.FName = TxtFName.Text;
            model.Email = TxtEmail.Text;
            model.Phone = TxtStudentPhone.Text;
            model.Password = TxtPassword.Text;
            model.ConfirmPassword = TxtConfirmPassword.Text;
            model.Dob = DtpStudetDob.Text.ToString();
            model.UserId = USER_ID;
            model.ImageUrl = txtImageUrl.Text;
            _StudentRepo.UpdateStudent(model);
            RefreshStudentGridView();
            this.Close();
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var x = MessageBox.Show("Are you Sure to delete Student", "caption", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (DialogResult.Yes == x)
            {
                _StudentRepo.Delete(StudentId);
                RefreshStudentGridView();
                this.Close();
            }
        }

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
