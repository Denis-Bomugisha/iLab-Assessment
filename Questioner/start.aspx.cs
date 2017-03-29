using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Questioner
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            CourseList.Items.Add("Electrical");
            CourseList.Items.Add("Telecom");
            CourseList.Items.Add("Computer");
            CourseList.Items.Add("Biomedical");
            YearList.Items.Add("I");
            YearList.Items.Add("II");
            YearList.Items.Add("III");
            YearList.Items.Add("IV");
            
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            if (studentName.Text == "") ErrorMessage.Text = "*Student Name is Required*";
            else if (RegNumber.Text == "") ErrorMessage.Text = "*Registration Number is Required*";
            else if (studentNumber.Text == "") ErrorMessage.Text = "*Student Number is Required*";
            else
            {
                try
                {

                    Session["name"] = studentName.Text;
                    SqlConnection myConnection = new SqlConnection();
                    myConnection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Questions.mdf;Integrated Security=True;";


                    myConnection.Open();

                    string validateQuery = "SELECT COUNT(*) AS Expr1 FROM Tally WHERE (StdNo =" + "\'" + studentNumber.Text + "\')";
                    SqlCommand validateCmd = new SqlCommand(validateQuery, myConnection);
                    SqlDataReader validateReader;
                    validateReader = validateCmd.ExecuteReader();
                    validateReader.Read();
                    string count = validateReader["Expr1"].ToString();
                    validateReader.Close();
                    int x = Convert.ToInt32(count);
                    if (x == 0)
                    {

                        string query = "INSERT INTO Tally";
                        query += " (Name, Reg, StdNo, Course) VALUES(";
                        query += "\'" + studentName.Text + "\',";
                        query += "\'" + RegNumber.Text + "\',";
                        query += "\'" + studentNumber.Text + "\',";
                        query += "\'" + CourseList.SelectedValue +" "+YearList.SelectedValue+ "\'";
                        query += ")";

                        SqlCommand cmd = new SqlCommand(query, myConnection);
                        SqlDataReader reader;
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        reader.Close();
                        Server.Transfer("Answer.aspx");
                    }
                    else
                    {
                        ErrorMessage.Text = "Student Record Already Exists";

                    }
                }
                catch (Exception Err) 
                {
                    ErrorMessage.Text = "Error accessing Database- Try Again or Contact Administrator";
                    string error = Err.ToString();
                   // MailDefinition errored = new MailDefinition();
                   // errored.Subject = "ISA Error";
                  

                }

             }
        }

        


    }
}