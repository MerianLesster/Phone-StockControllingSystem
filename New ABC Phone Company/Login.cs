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

namespace New_ABC_Phone_Company
{
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login()
        {
            try
            {
                DatabaseConnection dbObj = new DatabaseConnection();
                conn = dbObj.getConnection();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Can't Open Connection!! " + ex);
            }
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ck_Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_Showpassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HOME frm = new HOME();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand selectCommand = new SqlCommand(" Select * from Authentication_Level where Username=@Username and Password=@Password", conn);
                selectCommand.Parameters.Add(new SqlParameter("Username", txtUsername.Text.ToString()));
                selectCommand.Parameters.Add(new SqlParameter("Password", txtPassword.Text.ToString()));
                string UserType = null;
                SqlDataReader reader = selectCommand.ExecuteReader();
                bool rowfound = reader.HasRows;
                if (rowfound)
                {
                    while (reader.Read())
                    {
                        UserType = reader[2].ToString().Trim();

                        if (UserType == "Administrator")
                        {
                            MessageBox.Show("Welcome Administrator User! ", "Successfully Logged-in", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AdminMenu frm = new AdminMenu();
                            frm.Show();
                            this.Hide();
                        }
                        else if (UserType == "StockController")
                        {
                            MessageBox.Show("Welcome StockController User! ", "Successfully Logged-in", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StockControllerMenu frm = new StockControllerMenu();
                            frm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("This User has been Deactivated ", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtPassword.Text = null;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Username or Password ", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Text = null;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error login " + ex);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                MessageBox.Show("The Capslock key is ON", "Capslock is ON", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
