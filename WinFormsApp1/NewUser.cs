using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class NewUser : Form
    {
        DataBase dataBase = new DataBase();
        public NewUser()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isOk())
            {
                if (tabControl1.TabIndex == 1)
                {
                    string newPd = addNewPD(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, dataBase);
                    addNewUser("0", newPd, textBox7.Text, textBox6.Text, dataBase);
                    addNewPupil(newPd, textBox5.Text, dataBase);
                    MessageBox.Show("Пользователь успешно добавлен.");

                }
                if (tabControl1.TabIndex == 0)
                {
                    string newPd = addNewPD(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, dataBase);
                    addNewUser("1", newPd, textBox7.Text, textBox6.Text, dataBase);
                    MessageBox.Show("Пользователь успешно добавлен.");
                }
            }
            else
            {
                MessageBox.Show("Данные некорректны.");
            }
        }

        private bool isOk()
        {
            bool a = textBox7.Text != "" && textBox6.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "";
            if (tabControl1.TabIndex == 1)
            {
                return a && textBox5.Text != "";
            }
            if (tabControl1.TabIndex == 0)
            {
                return a && (checkedListBox1.CheckedItems.Count > 0);
            }
            return false;
        }

        private static string addNewPD(string name, string surn, string pat, string ph, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "insertPD";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@name",
                Value = name
            };

            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@sur",
                Value = surn
            };
            SqlParameter a3 = new SqlParameter
            {
                ParameterName = "@fat",
                Value = pat
            };

            SqlParameter a4 = new SqlParameter
            {
                ParameterName = "@ph",
                Value = ph
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            command.Parameters.Add(a3);
            command.Parameters.Add(a4);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table.Rows[0]["id"].ToString();
        }

        private static void addNewUser(string role, string pdId, string log, string pwd, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "inserNewUser";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@login",
                Value = log
            };

            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@pwd",
                Value = pwd
            };
            SqlParameter a3 = new SqlParameter
            {
                ParameterName = "@role",
                Value = role
            };

            SqlParameter a4 = new SqlParameter
            {
                ParameterName = "@pdId",
                Value = pdId
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            command.Parameters.Add(a3);
            command.Parameters.Add(a4);
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        private static void addNewPupil(string pdId, string classId, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "addNewPupil";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@pdId",
                Value = pdId
            };

            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@classId",
                Value = classId
            };
            command.Parameters.Add(a1);
            command.Parameters.Add(a2);
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }
    }
}
