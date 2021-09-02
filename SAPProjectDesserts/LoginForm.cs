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

namespace SAPProjectDesserts
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(@Program.ConnectionString);

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            
            if (username.Length == 0) {
                MessageBox.Show("Username cannot be empty");
                return;
            }
            
            if (password.Length == 0)
            {
                MessageBox.Show("Password cannot be empty");
                return;
            }
            
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[User] where Username='" + username + "' and Password='" + password + "'";
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == false) // means no result => cannot find matching username and password
            {
                MessageBox.Show("Username or Password is incorrect");
                con.Close();
                return;
            }
            else
            {
                Program.ID = (int) reader["ID_User"];

                MessageBox.Show("Login successfully.");
                con.Close();
                this.Hide();
                MyMainForm f = new MyMainForm();
                f.ShowDialog();
                this.Show();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();   
            RegisterForm f = new RegisterForm();    
            f.ShowDialog();
            txtUsername.Text = "";
            //////txtPassword.Text = "";
            this.Show();   
        }
    }
}
