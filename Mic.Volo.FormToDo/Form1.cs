using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mic.Volo.FormToDo
{
    public partial class Form1 : Form
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = db_cs1; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand command = new SqlCommand();
        public DataSet ds = new DataSet();
        string currentId = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRecords();

        }
        public void GetRecords()
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("Select * from tbl_tasks", connectionString);
            adapter.Fill(ds, "tbl_tasks");
            dataDataGridView1.DataSource = ds;
            dataDataGridView1.DataMember = "tbl_tasks";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("INSERT INTO tbl_tasks (Task_name) values ('"+textBox1.Text+"')",connection);
            adapter.Fill(ds, "tbl_tasks");
            textBox1.Clear();
            GetRecords();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = dataDataGridView1.CurrentRow.Index;
            currentId = dataDataGridView1[0, i].Value.ToString();
            textBox1.Text = dataDataGridView1[1, i].Value.ToString();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            adapter = new SqlDataAdapter("update tbl_tasks set Task_name ='"+textBox1.Text+"' where Id= "+currentId,connection);
            adapter.Fill(ds, "tbl_tasks");
            textBox1.Clear();
            GetRecords();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataDataGridView1.CurrentRow.Index;
            currentId = dataDataGridView1[0, i].Value.ToString();
            ds = new DataSet();
            adapter = new SqlDataAdapter("delete from tbl_tasks where id="+currentId,connection);
            adapter.Fill(ds, "tbl_tasks");
            textBox1.Clear();
            GetRecords();
        }
    }
}
