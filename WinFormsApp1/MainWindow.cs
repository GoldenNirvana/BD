using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class MainWindow : Form
    {
        DataBase database = new DataBase();
        int classId;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = getPdId(textBox1.Text, textBox2.Text, database);
            if (dt.Rows.Count == 1)
            {
                string Id = getId(dt.Rows[0]["PersonalDataId"].ToString(), dt.Rows[0]["role"].ToString(), database);
                if (dt.Rows[0]["role"].ToString() == "0")
                {
                    Form pupil = new PupilWindow(int.Parse(Id), classId);
                    pupil.Show();
                }
                if (dt.Rows[0]["role"].ToString() == "1")
                {
                    Form pupil = new TeacherWindow(this, int.Parse(Id));
                    pupil.Show();
                }
                if (dt.Rows[0]["role"].ToString() == "2")
                {
                    Form pupil = new SecWindow(this);
                    pupil.Show();
                }
            }
            
        }

        private DataTable getPdId(string log, string pwd, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "getPdId";
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter lesson = new SqlParameter
            {
                ParameterName = "@login",
                Value = log
            };

            SqlParameter text = new SqlParameter
            {
                ParameterName = "@passwd",
                Value = pwd
            };
            command.Parameters.Add(lesson);
            command.Parameters.Add(text);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            Console.WriteLine(table.Rows[0]["PersonalDataId"].ToString());
            return table;
        }

        private string getId(string pdId, string role, DataBase database)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string sqlExpression = "";
            if (role == "0")
            {
               sqlExpression = "getUserId";
            }
            if (role == "1")
            {
                sqlExpression = "getTeacherId";
            }
            SqlCommand command = new SqlCommand(sqlExpression, database.GetSqlConnection());
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter lesson = new SqlParameter
            {
                ParameterName = "@pdID",
                Value = pdId
            };
            command.Parameters.Add(lesson);

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (role == "0")
                classId = int.Parse(table.Rows[0]["ClassID"].ToString());
            return table.Rows[0]["ID"].ToString();
        }
    }
}