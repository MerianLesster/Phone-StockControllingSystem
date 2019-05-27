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
    public partial class GampahaBranch : Form
    {
        SqlConnection conn;
        public GampahaBranch()
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StockControllerMenu fm = new StockControllerMenu();
            fm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login fm = new Login();
            fm.Show();
        }

        
        private void GampahaBranch_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_7447_C_DataSet4.GampahaBranch' table. You can move, or remove it, as needed.
            this.gampahaBranchTableAdapter.Fill(this._7447_C_DataSet4.GampahaBranch);
            this.KeyPreview = true;
        }

        private void GampahaBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnAdd.PerformClick();
            }

            if (e.KeyCode == Keys.F2)
            {
                btnUpdate.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                btnDelete.PerformClick();
            }

            if (e.KeyCode == Keys.F4)
            {
                btnSearch.PerformClick();
            }

            if (e.KeyCode == Keys.F5)
            {
                btnTransfer.PerformClick();
            }
        }

        private void ClearFields()
        {
            txt_Price.Text = null;
            txtID.Text = null;
            txtName.Text = null;
            txtStorage.Text = null;
            txtVersion.Text = null;
        }

        private void DisplayData()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from GampahaBranch", conn);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "" || txtName.Text == "" || txtStorage.Text == "" || txtVersion.Text == "" || txt_Price.Text == "" || comboBoxBrand.Text == "")
                {
                    MessageBox.Show("Please Fill all the above Details", "Error while Adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO GampahaBranch VALUES (@ID, @Brand, @Name, @Version, @Storage, @Price)", conn);
                    insertCommand.Parameters.Add(new SqlParameter("ID", txtID.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Brand", comboBoxBrand.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Name", txtName.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Version", txtVersion.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Storage", txtStorage.Text.ToString()));
                    insertCommand.Parameters.Add(new SqlParameter("Price", txt_Price.Text.ToString()));
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Successfully Added", "Add Stock Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    DisplayData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error insering" + ex, "Create User Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand SearchCustomerNameCommand = new SqlCommand("select * from GampahaBranch where Brand = @Brand", conn);
            SearchCustomerNameCommand.Parameters.Add("@Brand", comboBoxBrand.Text.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SearchCustomerNameCommand);
            DataSet ds = new DataSet();
            da.Fill(ds, "GampahaBranch");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "GampahaBranch";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtName.Text == "" || txtStorage.Text == "" || txtVersion.Text == "" || txt_Price.Text == "" || comboBoxBrand.Text == "")
            {
                MessageBox.Show("Please Fill all the above Details", "Error while Updating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand UpdateValues = new SqlCommand("update GampahaBranch set Brand=@Brand, Name=@Name, Version=@Version, Storage=@Storage, Price=@Price where ID=@ID", conn);
                UpdateValues.Parameters.AddWithValue("@ID", txtID.Text);
                UpdateValues.Parameters.AddWithValue("@Brand", comboBoxBrand.Text);
                UpdateValues.Parameters.AddWithValue("@Name", txtName.Text);
                UpdateValues.Parameters.AddWithValue("@Version", txtVersion.Text);
                UpdateValues.Parameters.AddWithValue("@Storage", txtStorage.Text);
                UpdateValues.Parameters.AddWithValue("@Price", txt_Price.Text);
                UpdateValues.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully", "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                DisplayData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand deleteCommand = new SqlCommand("delete from GampahaBranch where ID=@ID", conn);

                deleteCommand.Parameters.Add(new SqlParameter("ID", txtID.Text.ToString().Trim()));
                deleteCommand.ExecuteNonQuery();

                MessageBox.Show("Data Deleted Successfully", "Successfully Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error deleting" + ex, "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (!(txtID.Text.Equals("")))
            {
                try
                {
                    conn.Close();
                    conn.Open();
                    string search = "SELECT * FROM GampahaBranch WHERE ID = '" + txtID.Text + "'";
                    SqlDataAdapter exe = new SqlDataAdapter(search, conn);
                    SqlDataReader data1 = exe.SelectCommand.ExecuteReader();

                    string ID = null;
                    string Brand = null;
                    string Name = null;
                    string Version = null;
                    string Storage = null;
                    string Price = null;

                    while (data1.Read())
                    {
                        ID = data1.GetString(0).ToString();
                        Brand = data1.GetString(1).ToString();
                        Name = data1.GetString(2).ToString();
                        Version = data1.GetString(3).ToString();
                        Storage = data1.GetString(4).ToString();
                        Price = data1.GetString(5).ToString();
                    }
                    data1.Close();
                    conn.Close();

                    if (!(txtID.Text.Equals("")))
                    {
                        conn.Open();
                        string insert = "INSERT INTO DehiwalaBranch VALUES('" + ID + "', '" + Brand + "','" + Name + "', '" + Version + "', '" + Storage + "', '" + Price + "')";
                        SqlDataAdapter func = new SqlDataAdapter(insert, conn);
                        func.SelectCommand.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("You've transfered the data Successfully ", "Successfully Transfered", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        conn.Open();
                        SqlCommand deleteCommand = new SqlCommand("delete from GampahaBranch where ID=@ID", conn);
                        deleteCommand.Parameters.Add(new SqlParameter("ID", txtID.Text.ToString().Trim()));
                        deleteCommand.ExecuteNonQuery();
                        conn.Close();
                    }
                    conn.Close();
                    DisplayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                }
                conn.Close();
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from GampahaBranch", conn);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            comboBoxBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtVersion.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtStorage.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_Price.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void txtID_Click(object sender, EventArgs e)
        {
            SqlCommand selectCommand = new SqlCommand("Select ID from GampahaBranch", conn);

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
                    txtID.Text = "X00" + customeIdInt;
                }
                else if (Cust_Id >= 9 && Cust_Id < 99)
                {
                    customeIdInt = Cust_Id + 1;
                    txtID.Text = "X0" + customeIdInt;
                }
                else if (Cust_Id >= 99)
                {
                    customeIdInt = Cust_Id + 1;
                    txtID.Text = "X" + customeIdInt;
                }
            }
            else
            {
                txtID.Text = "X001";
            }
            reader.Close();
        }
    }
}
