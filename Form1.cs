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
using System.Globalization;

namespace sql_datagridview
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        string connectionString = "";
        string sqlCMD = "select* from ";
        private BindingSource bindingSource = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = bindingSource;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                connectionString = textBoxConnectionString.Text;

                if (string.IsNullOrEmpty(connectionString))
                {
                    toolStripStatusLabel1.Text = "Invalid Connection String!";
                }
                else
                {
                    string tableName = textBoxTableName.Text;

                    if (string.IsNullOrEmpty(tableName))
                    {
                        toolStripStatusLabel1.Text = "Invalid Table Name!";
                    }
                    else
                    {
                        dataAdapter = new SqlDataAdapter((sqlCMD + tableName), connectionString);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                        DataTable table = new DataTable
                        {
                            Locale = CultureInfo.InvariantCulture
                        };
                        dataAdapter.Fill(table);
                        bindingSource.DataSource = table;
                        dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                        toolStripStatusLabel1.Text = "None";
                    }
                }
            }
            catch (SqlException exc)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                                "connectionString variable with a connection string that is " +
                                "valid for your system."
                                );
                toolStripStatusLabel1.Text = "SQL Exception!";
                System.Console.WriteLine("Exception: " + exc.Message);
            }
        }
    }
}
