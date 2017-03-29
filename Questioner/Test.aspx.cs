using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Questioner
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Questions.mdf;Integrated Security=True;User Instance=True";


            myConnection.Open();

            string validateQuery = "SELECT COUNT(*) AS Expr1 FROM Tally WHERE (StdNo ='2100045')";
            SqlCommand validateCmd = new SqlCommand(validateQuery, myConnection);
            SqlDataReader validateReader;
            validateReader = validateCmd.ExecuteReader();
            validateReader.Read();
            string count = validateReader["Expr1"].ToString();
            TextBox1.Text = count;
            validateReader.Close();

        }
    }
}