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
    public partial class RegisterForm : Form
    {
        SqlConnection con = new SqlConnection(@Program.ConnectionString);
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0)    // check if txtUsername is empty
            {
                MessageBox.Show("Username cannot be empty");
                return;
            }
            string p1 = txtPassword.Text.Trim();
            string p2 = txtRetypePassword.Text.Trim();
            if (p1.Equals(p2) == false)     
            {
                MessageBox.Show("Password and Retype Password do not match");
                return;
            }
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string name = txtName.Text.Trim();
            string contact = txtContact.Text.Trim();
            string address = txtAddress.Text.Trim();

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[User] where Username='" + username + "'";
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();
            bool user_exist = true;
            if (reader.Read() == false) 
            {
                user_exist = false;
            }
            else
            {
                MessageBox.Show("Username " + username + " already taken.");
            }
            reader.Close();        
           
            if (user_exist == false)
            {
                SqlCommand add_cmd = con.CreateCommand();
                add_cmd.CommandType = CommandType.Text;
                add_cmd.CommandText = "insert into [dbo].[User] values('" + username + "','" + password + "','" + name + "','" +
                    contact + "','" + address + "')";
                add_cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Registered");
                con.Close();
                this.Close();
            }
            con.Close();
        }
        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    }

