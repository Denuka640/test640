using FineManagment; 
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FineManagment 
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
                {
                    conn.Open();
                    MessageBox.Show("Connected to the database successfully.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
       
        {
            string username = txtUsername.Text.Trim();
            string password = txtPW.Text.Trim();
            string userType = UserType.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userType))
            {
                MessageBox.Show("Please enter all fields.");
                return;
            }

            string query = "SELECT COUNT(*) FROM user_loginuser WHERE username_userid = @Username AND password = @Password AND usertype = @UserType";

            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@UserType", userType);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Login successful!");

                    
                    if (userType == "Admin")
                    {
                        AdminDashboard adminForm = new AdminDashboard(); 
                        adminForm.Show();
                    }
                    else if (userType == "Officer")
                    {
                        string officerId = username; // Assuming Officer ID is numeric
                        OfficerDashboard officerForm = new OfficerDashboard(officerId);
                        officerForm.Show();
                    }
                    else if (userType == "Citizen")
                    {
                        string nic = username; 
                        CitizenDashboard citizenForm = new CitizenDashboard(/*nic*/);
                        citizenForm.Show();
                    }

                    this.Hide(); 
                }
                else
                {
                    MessageBox.Show("Invalid login.");
                }
            }
        }
    
        

        private void btnRegisterOfficer_Click(object sender, EventArgs e)
        {
            OfficerRegisterForm officerForm = new OfficerRegisterForm(this);
            officerForm.Show();
            this.Hide();
        }

        private void UserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRegisterCitizen_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            CitizenRegisterForm registerForm = new CitizenRegisterForm(this);
            registerForm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

