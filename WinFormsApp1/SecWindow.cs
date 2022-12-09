using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class SecWindow : Form
    {

        Form form;
        string pdId;

        public SecWindow(Form form, string pdId)
        {
            this.form = form;
            this.pdId = pdId;
            form.Hide();
            InitializeComponent();
        }

        private void changeScheduleClick(object sender, EventArgs e)
        {

        }

        private void putSubjectToTeacher(object sender, EventArgs e)
        {

        }

        private void addOrDeletePupil(object sender, EventArgs e)
        {
            
        }

        private void addorDeleteHeadTeacher(object sender, EventArgs e)
        {

        }

        private void showAllTeachers(object sender, EventArgs e)
        {

        }

        private void showHeadTeachers(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            form.Show();
            Close();
        }

        private void SecWindow_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            NewUser user = new NewUser();
            user.Show();
        }
    }
}
