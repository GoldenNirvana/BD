using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    enum RowState
    {
        Existed,
        New,
        Modifed,
        ModifedNew,
        Deleted
    }

    public partial class TeacherWindow : Form
    {
        int index;
        DataBase database = new DataBase();
        private Form f;
        public TeacherWindow(Form form)
        {
            f = form;
            f.Hide();
            InitializeComponent();
            string str = $"select ClassName from Classes";
            database.openConnection();
            SqlCommand command = new SqlCommand(str, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                classes.Items.Add(className);
            }
            database.closeConnection();
        }

        private void createColumns()
        {
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("n1", "Фамилия");
            dataGridView1.Columns.Add("n2", "Имя");
            dataGridView1.Columns.Add("n3", "Отчество");
        }

        private void readSingleRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3));
        }

        private void refrestDGW(DataGridView dgw, String indexClass)
        {
            dgw.Rows.Clear();
            string q = $"select Pupils.ID, Namee, Surname, Patronymic from PersonalData join Pupils on PersonalData.ID = Pupils.DataID where ClassID = '{indexClass}'";
            SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                readSingleRows(dgw, reader);
            }
            reader.Close();
        }

        private void exit(object sender, EventArgs e)
        {
            f.Show();
            Close();
        }

        private void classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = $"select id from Classes where ClassName = '{classes.Text}'";
            string classID;
            database.openConnection();
            SqlCommand command = new SqlCommand(q, database.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string indexClass = reader.GetValue(0).ToString();
                database.closeConnection();
                reader.Close();
                refrestDGW(dataGridView1, indexClass);
            }
            database.closeConnection();
            reader.Close();
        }

        private void TeacherWindow_Load(object sender, EventArgs e)
        {
            createColumns();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("gr", "Оценка");
            dataGridView2.Columns.Add("date", "Дата обновления");

            index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[index];
                nameBox.Text = row.Cells[1].Value.ToString();
                surnameBox.Text = row.Cells[2].Value.ToString();
                FatherBox.Text = row.Cells[3].Value.ToString();

                int id = int.Parse(row.Cells[0].Value.ToString());
                string q = $"select Grade, UpdateDate from HomeTasksAnswers where PupilID = '{id.ToString()}'";
                int count = 0;
                double sum = 0.0;
                SqlCommand command1 = new SqlCommand(q, database.GetSqlConnection());
                database.openConnection();
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    count++;
                    sum += reader1.GetInt32(0);
                    dataGridView2.Rows.Add(reader1.GetInt32(0), reader1.GetString(1));
                }
                if (count > 0)
                {
                    averadgeBox.Text = (sum / count).ToString();
                    database.closeConnection();
                    reader1.Close();
                }
            }
        }
    }
}