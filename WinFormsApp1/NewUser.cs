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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class NewUser : Form
    {
        DataBase dataBase = new DataBase();
        List<int> classesId = new List<int>();
        public NewUser()
        {
            InitializeComponent();
            getClasses();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isOk())
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    string newPd = addNewPD(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, dataBase);
                    addNewUser("0", newPd, textBox7.Text, textBox6.Text, dataBase);
                    addNewPupil(newPd, classesId[comboBox1.SelectedIndex].ToString(), dataBase);
                    MessageBox.Show("Пользователь успешно добавлен.");

                }
                if (tabControl1.SelectedTab == tabPage2)
                {
                    string newPd = addNewPD(textBox2.Text, textBox1.Text, textBox3.Text, textBox4.Text, dataBase);
                    addNewUser("1", newPd, textBox7.Text, textBox6.Text, dataBase);
                    string teacherId = addNewTeacher(newPd, dataBase);
                    for (int i = 0; i < checkedListBox1.CheckedIndices.Count; i++)
                    {
                        addSpecs(teacherId, (checkedListBox1.CheckedIndices[i] + 1).ToString(), dataBase);
                    }
                    MessageBox.Show("Пользователь успешно добавлен.");
                }
            }
            else
            {
                MessageBox.Show("Данные некорректны.");
            }
        }
        private void getClasses()
        {
            string str = $"select ClassName, Id from Classes";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(str, dataBase.GetSqlConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string className = reader.GetValue(0).ToString();
                classesId.Add(int.Parse(reader.GetValue(1).ToString()));
                comboBox1.Items.Add(className);
            }
            reader.Close();
            dataBase.closeConnection();
        }

        private bool isOk()
        {
            bool a = textBox7.Text != "" && textBox6.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "";
            if (tabControl1.SelectedTab == tabPage1)
            {
                return a && comboBox1.Text != "";
            }
            if (tabControl1.SelectedTab == tabPage2)
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

            string sqlExpression = "insertNewUser";
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

        private static string addNewTeacher(string pdId, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "insertTeacher";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@pdId",
                Value = pdId
            };

            command.Parameters.Add(a1);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            return table.Rows[0]["ID"].ToString();
        }

        private static void addSpecs(string Id,string subId, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string sqlExpression = "insertTeacherSpecs";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());

            command.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter a1 = new SqlParameter
            {
                ParameterName = "@id",
                Value = Id
            };

            SqlParameter a2 = new SqlParameter
            {
                ParameterName = "@subId",
                Value = subId
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
