using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;


namespace Questioner 
{
    public partial class Answer : System.Web.UI.Page 
    {
        
        
        int[] Qn =new int[32]; //Array length is last id of Qns in qn table plus 1
        //int Marks;
        protected void Page_Load(object sender, EventArgs e)
        {
            AnswerBox.Visible = false;
            AnswerList.Visible = false;
            Label1.Visible = false;

        }

        protected void ShowQn(string index)
        {
            
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Questions.mdf;Integrated Security=True;";
            myConnection.Open();

            string query = "SELECT [Type],[Question],[AnswerA],[AnswerB],[AnswerC],[AnswerD] FROM [QnTable] WHERE No=" + index + "";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();
            
                Label1.Text = reader["Question"].ToString();
                string t = reader["Type"].ToString();
                if (t == "Objective")
                {
                    Label1.Visible = true;
                    AnswerBox.Visible = false;
                    AnswerList.Visible = true;
                    string AnswerA = reader["AnswerA"].ToString();
                    string AnswerB = reader["AnswerB"].ToString();
                    string AnswerC = reader["AnswerC"].ToString();
                    string AnswerD = reader["AnswerD"].ToString();
                    AnswerList.Items.Add(AnswerA);
                    AnswerList.Items.Add(AnswerB);
                    AnswerList.Items.Add(AnswerC);
                    AnswerList.Items.Add(AnswerD);
                }
                else if (t == "Input")
                {
                    Label1.Visible = true;
                    AnswerList.Visible = false;
                    AnswerBox.Visible = true;
                }
                else
                {
                    Label1.Visible = true;
                    AnswerBox.Visible = false;
                    AnswerList.Visible = true;
                    string AnswerA = reader["AnswerA"].ToString();
                    string AnswerB = reader["AnswerB"].ToString();
                    string AnswerC = reader["AnswerC"].ToString();
                    string AnswerD = reader["AnswerD"].ToString();
                    AnswerList.Items.Add(AnswerA);
                    AnswerList.Items.Add(AnswerB);
                    AnswerList.Items.Add(AnswerC);
                    AnswerList.Items.Add(AnswerD);

                }
            

            reader.Close();
            myConnection.Close();



        }
        protected void ClearAnswer()
        {
            AnswerList.Items.Clear();
            AnswerBox.Text = "";
        }

        protected void AnswerQn(string answer_index) 
        {   
            //Code to show a particular question
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Questions.mdf;Integrated Security=True;User Instance=True";
            myConnection.Open();

            string query = "SELECT [Type],[AnswerFinalObj],[AnsInputUpper], [AnsInputLower] FROM [QnTable] WHERE No=" + answer_index + "";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            reader.Read();

            string choice= reader["Type"].ToString();
            
            
            if (choice == "Objective")
            {
                string answer = AnswerList.SelectedValue;
                if (answer == reader["AnswerFinalObj"].ToString())
                {
                    int index = Convert.ToInt32(answer_index);
                    Qn[index] = 1;
                    int NewMark = Convert.ToInt32(Session["Mark"]) + 1;
                    Session["Mark"] = NewMark.ToString();
                }

            }

            if (choice == "Input")
            {
                int UpperLimit = Convert.ToInt32(reader["AnsInputUpper"].ToString());
                int LowerLimit = Convert.ToInt32(reader["AnsInputLower"].ToString());
                int Answer=Convert.ToInt32(AnswerBox.Text);

                if (Answer <= UpperLimit && Answer >= LowerLimit)
                {
                    int index2 = Convert.ToInt32(answer_index);
                    Qn[index2] = 1;
                    int NewMark2 = Convert.ToInt32(Session["Mark"]) + 1;
                    Session["Mark"] = NewMark2.ToString();
                }
                   
            }
            reader.Close();
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Session["Mark"] = "0";
            Session["id"] = "7"; //First id should be id of first question
            ShowQn("7");
        }
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            string iD_Former = Session["id"].ToString();
            AnswerQn(iD_Former);
            Button1.Visible = false;
            Session["id"] = Convert.ToInt32(Session["id"]) + 1;     
            string iD_Current = Session["id"].ToString();
            ClearAnswer();
            ShowQn(iD_Current);

            int x = Qn.Length;
            x--;
            if ((Convert.ToInt32(Session["id"])) == x) Button2.Visible = false;
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Button2.Visible == false) Button2.Visible = true;
            Session["id"] = Convert.ToInt32(Session["id"]) - 1;
            int intMark = Convert.ToInt32(Session["Mark"]) - 1;
            Session["Mark"] = intMark.ToString();
            string iD = Session["id"].ToString();
            ClearAnswer();
            ShowQn(iD);
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            string iD_Former = Session["id"].ToString();
            AnswerQn(iD_Former);
            string StdName = Session["name"].ToString();
            string Marked = Session["Mark"].ToString();
            int Marked_N = Convert.ToInt32(Marked);
            //for (int i = 1; i < Qn.Length; i++)
            //{
               
            //    Marks += Qn[i];
                
            //}
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Questions.mdf;Integrated Security=True;User Instance=True";
                myConnection.Open();

                string query = "UPDATE Tally SET Mark=" + Marked_N + " WHERE Name=" + "\'" + StdName + "\'";
                SqlCommand cmd = new SqlCommand(query, myConnection);
                //SqlCommand cmd = new SqlCommand(query, myConnection);
                SqlDataReader reader3;
                reader3 = cmd.ExecuteReader();
                reader3.Read();
                reader3.Close();
                myConnection.Close();
                Session.Clear();
                Server.Transfer("FinalPage.aspx");

        }

        

    }
}