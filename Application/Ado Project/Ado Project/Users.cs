using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.PeerToPeer;
using System.Reflection;

namespace Ado_Project
{
    internal class Users 
    {
        public int UserID { get; set; }
        public string UserType { get; set; }
        public string AccountStatus { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }

        clsDBConnection clsDBConnection = new clsDBConnection();
        public void UserNameInitializer(bool AddUser)
        {
            bool Valid = true;
            do
            {
                Console.Write("Enter UserName: ");
                UserName = Console.ReadLine().Trim();
                Valid = ValidUser(UserName, AddUser);

            } while (UserName == null || !Valid);
        }
        public void PasswordInitializer(bool AddUser)
        {
            bool Valid = true;
            do
            {
                Console.Write("Enter Password: ");
                Password = Console.ReadLine().Trim();
                Valid = ValidPass(Password, AddUser);

            } while (UserName == null || !Valid);
        }
        public bool ValidUser(string User,bool NewUser)
        {
            bool ValidUser = true;
            if (User == null || User == "")
            {
                Console.WriteLine("Sorry, Username is Blank.");
                ValidUser = false;
            }
            else
            {
                if (UserName.Length > 30)
                {
                    Console.WriteLine("Sorry, your username must be between 4 and 30 characters long.");
                    ValidUser = false;
                }
                else
                {
                    if (!ValidInput(User))
                    {
                        Console.WriteLine("Sorry, only letters (a-z), numbers (0-9), and periods (.) are allowed.");
                        ValidUser = false;
                    }
                    else
                    {
                        if (!clsDBConnection.AvailUser(User, NewUser, out string msg))
                        {
                            Console.WriteLine(msg);
                            ValidUser = false;
                        }
                    }
                }
            }
            return ValidUser;
        }
        public bool ValidInput(string IpcValue)
        {
            bool ValidInput = true;
            char[] ValidCharacter = "abcdefghijklmnopqrstuvwxyz.0123456789".ToCharArray();
            char[] charArr = IpcValue.ToCharArray();

            foreach (char c in charArr)
            {
                if (!ValidCharacter.Contains(c))
                {
                    ValidInput = false;
                    break;
                }
            }
            return ValidInput;
        }

        public bool ValidPass(string Pass,bool NewUser)
        {
            bool ValidUser = true;
            if (Pass == null || Pass == "")
            {
                Console.WriteLine("Sorry, Password is Blank.");
                ValidUser = false;
            }
            else
            {
                if (Pass.Length > 20)
                {
                    Console.WriteLine("Sorry, your username must be between 4 and 20 characters long.");
                    ValidUser = false;
                }
                else
                {
                    if (!ValidInput(Pass))
                    {
                        Console.WriteLine("Sorry, only letters (a-z), numbers (0-9), and periods (.) are allowed.");
                        ValidUser = false;
                    }
                    else
                    {
                        if (!NewUser && !clsDBConnection.AvailPass(UserName,Pass, out string msg))
                        {
                            Console.WriteLine(msg);
                            ValidUser = false;
                        }
                    }
                }
            }
            return ValidUser;
        }

        public bool Login(out string User, out string msg)
        {
            /* User Login */
            bool ReturnValue = false;
            User = string.Empty;

            UserNameInitializer(false);
            PasswordInitializer(false);
            DataTable dt = clsDBConnection.LoginUser(UserName,Password,out msg);
            if (dt != null)
            {
                UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                UserType = Convert.ToString(dt.Rows[0]["UserType"]);
                AccountStatus = Convert.ToString(dt.Rows[0]["AccountStatus"]);

                if (AccountStatus == "Approved")
                {
                    User = Convert.ToString(dt.Rows[0]["UserName"]);
                    ReturnValue = true;
                }
                else
                {
                    msg = "Sorry, your account is " + AccountStatus;
                }
            }
            return ReturnValue;
            
        }
    }
}
