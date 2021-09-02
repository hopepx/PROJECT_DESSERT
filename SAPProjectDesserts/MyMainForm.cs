using SAPProjectDesserts.Properties;
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
    public partial class MyMainForm : Form
    {

        SqlConnection con = new SqlConnection(@Program.ConnectionString);
        public MyMainForm()
        {

            InitializeComponent();
            InitializeProfile();
        }

        private void InitializeProfile()
        {
            con.Open();

            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [dbo].[User] WHERE [ID_User] = @id";
                command.Parameters.AddWithValue("@id", Program.ID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["USername"].ToString();
                        textBox3.Text = reader["Name"].ToString();
                        txtContact.Text = reader["Contact"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                    }
                }
                con.Close();
            }
        }

        private void MyMainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'databaseDataSet7.User' table. You can move, or remove it, as needed.
            this.userTableAdapter1.Fill(this.databaseDataSet7.User);
            // TODO: This line of code loads data into the 'databaseDataSet7.Order' table. You can move, or remove it, as needed.

            // TODO: This line of code loads data into the 'databaseDataSet4.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.databaseDataSet4.User);
            // TODO: This line of code loads data into the 'databaseDataSet6.Order' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'databaseDataSet5.Order' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'databaseDataSet4.Order' table. You can move, or remove it, as needed.
            displayData();

            pictureBox5.Image = null;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            displayData();
        }
        private void displayData()
        {

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[Order]";
            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView.DataSource = dt;

            con.Close();

        }

        private void displayData1()
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[User]";
            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Update();
            dataGridView1.Refresh();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            string dessert = cbDesserts1.Text.Trim();
            string cost = txtCost1.Text.Trim();
            string quantity = nudQuantity1.Value.ToString();
            string total = txtTotal1.Text.Trim();

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Order] values( '" + dessert + "', '" + cost + "', '" + quantity + "','" + total + "' )";
            cmd.ExecuteNonQuery();
            con.Close();

            displayData();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count < 1)
            {
                MessageBox.Show("Nothing to delete as it is empty");
                return;
            }


            int index = dataGridView.SelectedRows[0].Index;



            var key = dataGridView.SelectedRows[0].Cells[0].Value;
            int id_customer = (int)key;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from [dbo].[Order] where ID_Customer=" + id_customer;
            cmd.ExecuteNonQuery();
            con.Close();

            displayData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (dataGridView.SelectedRows.Count < 1)
            {

                return;
            }


            int id_customer = (int)dataGridView.SelectedRows[0].Cells[0].Value;
            string dessert = cbDesserts1.Text.Trim();
            string cost = txtCost1.Text.Trim();
            string quantity = nudQuantity1.Value.ToString();
            string total = txtTotal1.Text.Trim();

            con.Open();

            string query = "update [dbo].[Order] set Desserts=@c, Cost=@n, Quantity=@m, Total=@t where ID_Customer=" + id_customer;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@c", dessert);
            cmd.Parameters.AddWithValue("@n", cost);
            cmd.Parameters.AddWithValue("@m", quantity);
            cmd.Parameters.AddWithValue("@t", total);
            cmd.ExecuteNonQuery();

            con.Close();
            displayData();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm f = new LoginForm();
            f.ShowDialog();
            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cbDesserts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDesserts.SelectedIndex == 0)
                pictureBox5.Image = Resources.download;
            else if (cbDesserts.SelectedIndex == 1)
                pictureBox5.Image = Resources.download__1_;
            else if (cbDesserts.SelectedIndex == 2)
                pictureBox5.Image = Resources._784cf27879da893d719794c181839a5d;
            else if (cbDesserts.SelectedIndex == 3)
                pictureBox5.Image = Resources._10292219_309651812534456_1086475442894971596_n_20190614170331;

            int cost = 0;

            switch (cbDesserts.SelectedIndex)
            {
                case 0:
                    cost = 3;
                    break;
                case 1:
                    cost = 4;
                    break;
                case 2:
                    cost = 5;
                    break;
                case 3:
                    cost = 2;
                    break;
            }

            txtCost.Text = "$" + Convert.ToString(cost);
        }

        private void tpProfile_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private int extractNumericalValue(string a)
        {
            string b = string.Empty;
            int val = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }
            if (b.Length > 0)
                val = int.Parse(b);
            return val;

        }

        private void displayOrderData()
        {
            con.Open();


            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [dbo].[Order]";
            cmd.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbDesserts.Text = "";
            nudQuantity.Value = 1;
            txtTotal.Text = "";
            txtCost.Text = "";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(nudQuantity.Text);
            int cost = Convert.ToInt32(extractNumericalValue(txtCost.Text));
            string Desserts = cbDesserts.SelectedItem.ToString();
            txtTotal.Text = "$" + Convert.ToString(cost * quantity);



            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [dbo].[Order] values ('" + Desserts + "', '" + "$" + cost + "', '" + quantity + "', '" + "$" + cost * quantity + "')";

            MessageBox.Show("Order successfully placed!");

            cmd.ExecuteNonQuery();
            con.Close();
            displayOrderData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cost = 0;
            int quantity = Convert.ToInt32(nudQuantity1.Value);

            switch (cbDesserts1.SelectedIndex)
            {
                case 0:
                    cost = 3;
                    break;
                case 1:
                    cost = 4;
                    break;
                case 2:
                    cost = 5;
                    break;
                case 3:
                    cost = 2;
                    break;
            }

            txtCost1.Text = "$" + Convert.ToString(cost);
            txtTotal1.Text = "$" + Convert.ToString(cost * quantity);
        }

        private void tpOrder_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count < 1) return;

            var cells = dataGridView.SelectedRows[0].Cells;

            cbDesserts1.Text = cells[1].Value.ToString();
            txtCost1.Text = cells[2].Value.ToString();
            nudQuantity1.Value = Convert.ToDecimal(cells[3].Value.ToString().Replace("$", ""));
            txtTotal1.Text = cells[4].Value.ToString();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtAddress1.Text = "";
            txtContact1.Text = "";
            txtName1.Text = "";
            txtPassword1.Text = "";
            txtUsername1.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MyMainForm f = new MyMainForm();
            f.ShowDialog();
            this.Show();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count < 1)
            {

                return;
            }


            int id_user = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            string username = txtUsername1.Text.Trim();
            string password = txtPassword1.Text.Trim();
            string name = txtName1.Text.Trim();
            string contact = txtContact1.Text.Trim();
            string address = txtAddress1.Text.Trim();

            con.Open();

            string query = "update [dbo].[User] set Username=@U, Password=@P, Name=@N, Contact=@C, Address=@A where ID_User=" + id_user;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@U", username);
            cmd.Parameters.AddWithValue("@P", password);
            cmd.Parameters.AddWithValue("@N", name);
            cmd.Parameters.AddWithValue("@C", contact);
            cmd.Parameters.AddWithValue("@A", address);
            cmd.ExecuteNonQuery();

            con.Close();
            displayData1();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername1.Text.Trim();
            string password = txtPassword1.Text.Trim();
            string name = txtName1.Text.Trim();
            string contact = txtContact1.Text.Trim();
            string address = txtAddress1.Text.Trim();

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [dbo].[User] values( '" + username + "', + '" + password + "', '" + name + "', '" + contact + "','" + address + "' )";
            cmd.ExecuteNonQuery();
            con.Close();

            displayData1();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Nothing to delete as it is empty");
                return;
            }


            int index = dataGridView1.SelectedRows[0].Index;



            var key = dataGridView1.SelectedRows[0].Cells[0].Value;
            int id_user = (int)key;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from [dbo].[User] where ID_User=" + id_user;
            cmd.ExecuteNonQuery();
            con.Close();

            displayData1();
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            cbDesserts1.Text = "";
            nudQuantity1.Value = 1;
            txtTotal1.Text = "";
            txtCost1.Text = "";
        }

        private void nudQuantity1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtRetypePassword.Text)
            {
                MessageBox.Show("Password is not the same.");
                return;
            }
            else if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Password is empty");
                return;
            }

            con.Open();

            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "UPDATE [dbo].[USer] SET Password=@password WHERE [ID_User]=@id";
                command.Parameters.AddWithValue("@password", txtPassword.Text);
                command.Parameters.AddWithValue("@id", Program.ID);

                bool isUpdated = (int)command.ExecuteNonQuery() >= 1;

                if (isUpdated == false)
                {
                    MessageBox.Show("Update Failed. ");
                }
                else
                {
                    MessageBox.Show("Password Changed. New Password is " + txtPassword.Text);
                }
            }
            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                return;
            }
            var cells = dataGridView1.SelectedRows[0].Cells;

            txtUsername1.Text = (string)dataGridView1.SelectedRows[0].Cells[1].Value;
            txtPassword1.Text = (string)dataGridView1.SelectedRows[0].Cells[2].Value;
            txtName1.Text = (string)dataGridView1.SelectedRows[0].Cells[3].Value;
            txtContact1.Text = (string)dataGridView1.SelectedRows[0].Cells[4].Value;
            txtAddress1.Text = (string)dataGridView1.SelectedRows[0].Cells[5].Value;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            displayData1();
        }

        private void txtContact1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContact1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}


