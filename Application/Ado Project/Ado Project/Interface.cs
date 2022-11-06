using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado_Project
{
    internal class Interface : CommonSystemMethod
    {
        Users users = new Users();

        public void HomePage()
        {
            int ipiValue = 0;
            ShowLogo("Home Page");
            Console.WriteLine("Select your choice: 1 or 2");
            ShowMsg("1 : Login");
            ShowMsg("2 : Register");
            //SkipLine();
            do
            {
                ipiValue = Conv_Int(ReadLineMsg());

                if (ipiValue != 1 && ipiValue != 2)
                    ShowMsg("Your choice is invalid. Please try again...");

            } while (ipiValue != 1 && ipiValue != 2);
            if (ipiValue == 1)
            {
                LoginProc();
            }

        }
        public void LoginProc()
        {

            ShowLogo("Login Page");
            bool LoginStatus = users.Login(out string username, out string msg);
            if (!LoginStatus)
            {
                Console.WriteLine(msg);
            }
        }
        public void AppStart()
        {
            Console.WriteLine();
        }
    }
    public class CommonSystemMethod 
    {
        public int Conv_Int(string Msg)
        {
            if (int.TryParse(Msg, out int iMsg))
                return iMsg;
            else return -1;
        }
        public string ReadLineMsg()
        {
            return Console.ReadLine();
        }
        public void WriteMsg(string msg)
        {
            Console.Write(msg);
        }
        public void ShowMsg(string msg)
        {
            Console.WriteLine(msg);
        }
        public void WriteMsg(int repeatMsg, string msg)
        {
            for (int i = 0; i < repeatMsg; i++)
            {
                Console.Write(msg);
            }
        }
        public void WriteMsg(int repeatMsg1, string msg1, int skipLine)
        {
            WriteMsg(repeatMsg1, msg1);
            SkipLine(skipLine);
        }
        public void WriteMsg(int repeatMsg1, string msg1, int repeatMsg2, string msg2)
        {
            WriteMsg(repeatMsg1, msg1);
            WriteMsg(repeatMsg2, msg2);
        }
        public void WriteMsg(int repeatMsg1, string msg1, int repeatMsg2, string msg2, int repeatMsg3, string msg3)
        {
            WriteMsg(repeatMsg1, msg1);
            WriteMsg(repeatMsg2, msg2);
            WriteMsg(repeatMsg3, msg3);
        }
        public void SkipLine(int repeat)
        {
            for (int i = 0; i < repeat; i++)
            {
                SkipLine();
            }
        }
        public void SkipLine()
        {
            Console.WriteLine();
        }

        public void ShowLogo(string Page)
        {
            ClearConsole();
            WriteMsg(12, " ", 1, "AdoProject");
            SkipLine();
            WriteMsg(12, " ", 10, "=", 12, " ");
            if (Page == null || Page == "")
                SkipLine();
            if (Page != null && Page != "")
                WriteMsg(1, "{" + Page + "}", 1);
        }
        public void ShowLogo2(string Page)
        {
            ClearConsole();
            WriteMsg(30, " ", 1, "AdoProject");
            SkipLine();
            WriteMsg(30, " ", 10, "=", 20, " ");
            if (Page != null && Page != "")
                WriteMsg(1, "{" + Page + "}", 1);
        }

        public string Conv_String(int iMsg)
        {
            return Convert.ToString(iMsg);
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void HoldScreen()
        {
            Console.ReadKey();
        }

        public string ReturnRepeatMsg(int repeatMsg, string msg)
        {
            string RepeatedMsg = string.Empty;
            for (int i = 0; i < repeatMsg; i++)
            {
                RepeatedMsg += msg;
            }
            return RepeatedMsg;
        }
    }

}

