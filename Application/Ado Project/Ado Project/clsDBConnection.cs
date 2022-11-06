using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Interop;

namespace Ado_Project
{
    internal class clsDBConnection
    {
        static string ConStr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        SqlConnection con = new SqlConnection(ConStr);

        public DataTable LoginUser(string ipcUserName, string ipcPass,out string msg)
        {
            DataTable dt = new DataTable();
            msg = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("spValidateLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", ipcUserName);
                cmd.Parameters.AddWithValue("@Password", ipcPass);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                msg = "Error : Problem Connecting";
                return dt;
            }
        }
        public string AddUserRecord(Users users)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", users.UserName);
                cmd.Parameters.AddWithValue("@Pass", users.Password);
                cmd.Parameters.AddWithValue("@Phone", users.ContactNo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Data save Successfully");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message.ToString());
            }
        }
        public bool AvailUser(string ipcUserName, bool NewUser, out string msg)
        {
            int iUserID = -1;
            msg = string.Empty;
            bool ReturnValue = false;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT [dbo].[fnAvailUser](@UserName)", con);
                cmd.Parameters.AddWithValue("@UserName", ipcUserName);
                con.Open();
                iUserID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return AvailMsg(iUserID,"username", NewUser, out msg);

            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = "Error : Problem Connecting";
                return ReturnValue;
            }
        }
        public bool AvailMsg(int UserID,string MsgType,bool NewUser,out string msg)
        {
            msg =string.Empty;
            bool ReturnValue = false;
            if (NewUser)
            {
                switch (UserID)
                {
                    case 0:
                        ReturnValue = true;
                        break;
                    default:
                        msg = "Sorry, " + MsgType + " is already taken.";
                        ReturnValue = false;
                        break;
                }
            }
            else
            {
                switch (UserID)
                {
                    case 0:
                    case -1:
                        msg = "Sorry, " + MsgType + " is not available.";
                        ReturnValue = false;
                        break;
                    default:
                        ReturnValue = true;
                        break;
                }
            }
            return ReturnValue;

        }

        public bool AvailPass(string ipcUserName, string ipcPass, out string msg)
        {
            int iUserID = -1;
            bool ReturnValue = false;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT [dbo].[fnGetUserID](@UserName,@Pass)", con);
                cmd.Parameters.AddWithValue("@UserName", ipcUserName);
                cmd.Parameters.AddWithValue("@Pass", ipcPass);
                con.Open();
                iUserID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return PassAvailMsg(iUserID, out msg);

            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = "Error : Problem Connecting";
                return ReturnValue;
            }
        }
        public bool PassAvailMsg(int UserID,out string msg)
        {
            msg =string.Empty;
            bool ReturnValue = false;
            switch (UserID)
            {
                case 0:
                case -1:
                    msg = "Sorry, Password is invalid.";
                    ReturnValue = false;
                    break;
                default:
                    ReturnValue = true;
                    break;
            }
            return ReturnValue;

        }
    }
}
