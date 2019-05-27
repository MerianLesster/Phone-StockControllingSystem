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
    public partial class Administrator : Form
    {
        SqlConnection conn;
        public Administrator()
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

        private void Administrator_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_7447_C_DataSet3.Authentication_Level' table. You can move, or remove it, as needed.
            this.authentication_LevelTableAdapter1.Fill(this._7447_C_DataSet3.Authentication_Level);
            // TODO: This line of code loads data into the '_7447_C_DataSet.Authentication_Level' table. You can move, or remove it, as needed.
            //this.authentication_LevelTableAdapter.Fill(this._7447_C_DataSet.Authentication_Level);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMenu frm = new AdminMenu();
            frm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DisplayData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from Authentication_Level", conn);
            adapt.Fill(dt);
            dataGridView_Search_Ad.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername_Ad.Text == "" || txtPassword_Ad.Text == "")
                {
                    MessageBox.Show("Please Fill all the above requirements", "Error while Adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtReEnterPass.Text != txtPassword_Ad.Text)
                {
                    MessageBox.Show("Please Re-enter the password that you gave above", "Error while Adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Authentication_Level VALUES (@Username, @Password, @UserType)", conn);
                    insertCommand.Parameters.Add(new SqlParameter("Username", txtUsername_Ad.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Password", txtPassword_Ad.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("UserType", comboUsertype.Text.ToString()));
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Successfully Created New User", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    DisplayData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error insering" + ex, "Create User Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtUsername_Ad.Text = null;
            txtPassword_Ad.Text = null;
            txtReEnterPass.Text = null;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            
            if (txtUsername_Ad.Text == "" || txtPassword_Ad.Text == "" )
            {
                MessageBox.Show("Please Fill all the above requirements", "Error while Updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtReEnterPass.Text != txtPassword_Ad.Text)
            {
                MessageBox.Show("Please Re-enter the password that you gave above", "Error while Updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand UpdateValues = new SqlCommand("update Authentication_Level set Password=@Password, UserType=@UserType where Username=@Username", conn);
                UpdateValues.Parameters.AddWithValue("@Username", txtUsername_Ad.Text);
                UpdateValues.Parameters.AddWithValue("@Password", txtPassword_Ad.Text);
                UpdateValues.Parameters.AddWithValue("@UserType", comboUsertype.Text);
                UpdateValues.ExecuteNonQuery();
                MessageBox.Show("User Updated Successfully", "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                DisplayData();
            }
        }

        private void dataGridView_Search_Ad_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtUsername_Ad.Text = dataGridView_Search_Ad.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPassword_Ad.Text = dataGridView_Search_Ad.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboUsertype.Text = dataGridView_Search_Ad.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dataGridView_Search_Ad_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUsername_Ad_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtUsername_Ad_Click(object sender, EventArgs e)
        {
            SqlCommand selectCommand = new SqlCommand ("Select Username from Authentication_Level", conn);

            SqlDataReader reader = selectCommand.ExecuteReader();
            bool rowFound = reader.HasRows;
            string CId = null;
            if (rowFound)
            {
                while (reader.Read())
                {
                    CId = reader[0].ToString();//C003
                }
                string customerIDString = CId.Substring(1);//003

                int Cust_Id = Int32.Parse(customerIDString);//3
                int customeIdInt = 0;
                if (Cust_Id >= 0 && Cust_Id < 9)
                {
                    customeIdInt = Cust_Id + 1;
                    txtUsername_Ad.Text = "S00" + customeIdInt;
                }
                else if (Cust_Id >= 9 && Cust_Id < 99)
                {
                    customeIdInt = Cust_Id + 1;
                    txtUsername_Ad.Text = "S0" + customeIdInt;
                }
                else if (Cust_Id >= 99)
                {
                    customeIdInt = Cust_Id + 1;
                    txtUsername_Ad.Text = "S" + customeIdInt;
                }
            }
            else
            {
                txtUsername_Ad.Text = "S001";
            }
            reader.Close();
        }

        private void linkDeactivate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtUsername_Ad.Text == "")
                {
                    MessageBox.Show("Please fill all the fields", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Close();
                    conn.Open();

                    string deactiveQuery = "UPDATE [7447_C#].[dbo].[Authentication_Level]  SET Usertype = 'DEACTIVATED' WHERE Username ='" + txtUsername_Ad.Text + "'";
                    SqlDataAdapter execute = new SqlDataAdapter(deactiveQuery, conn);
                    execute.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show("User is deactivated", "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    DisplayData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkActivate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtUsername_Ad.Text == "")
                {
                    MessageBox.Show("Please fill all the fields", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    string activeQuery = "select * from [7447_C#].[dbo].[Authentication_Level] where Username ='" + txtUsername_Ad.Text + "'";
                    SqlDataAdapter execute = new SqlDataAdapter(activeQuery, conn);
                    SqlDataReader DataRead = execute.SelectCommand.ExecuteReader();

                    string Username = null;

                    string UserType = null;


                    while (DataRead.Read())
                    {
                        Username = DataRead.GetString(0).ToString();


                    }
                    conn.Close();

                    string UserCode = Username.Substring(1, 2);
                    int UserIdentity = Int32.Parse(UserCode);

                    if (UserIdentity <= 10)
                    {
                        UserType = "Administrator";
                    }
                    else if (UserIdentity > 10)
                    {
                        UserType = "StockController";
                    }
                    conn.Open();
                    string InserQuery = "UPDATE [7447_C#].[dbo].[Authentication_Level] SET UserType = '" + UserType + "' where Username ='" + txtUsername_Ad.Text + "'";
                    SqlDataAdapter function = new SqlDataAdapter(InserQuery, conn);
                    function.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show(" User is activated", "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();

                    DisplayData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
