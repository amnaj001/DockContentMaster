using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Npgsql;
namespace DockContentMaster
{
    public class CommonUse
    {
        private readonly string strConn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        private readonly string strConnPG = ConfigurationManager.ConnectionStrings["ConnPG"].ConnectionString;
        readonly SqlConnection Conn = new SqlConnection();
        readonly NpgsqlConnection ConnPG = new NpgsqlConnection();
        //Public ConndbKR As New SqlConnection(strconnReport);
        SqlCommand cmd;
        NpgsqlCommand pgcmd;
        public SqlConnection Connect()
        {
           if(Conn.State ==  ConnectionState.Closed )
           {
               Conn.ConnectionString = strConn;
               Conn.Open();
           }
            return Conn;
        }

        public NpgsqlConnection PgConnect()
        {
            if (ConnPG.State == ConnectionState.Closed)
            {
                ConnPG.ConnectionString = strConnPG;
                ConnPG.Open();
            }
            return ConnPG;
        }

        public DataTable ExecuteReader(string strsql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.ConnectionString = strConn;
                    Conn.Open();
                }
                cmd = new SqlCommand();
                cmd.CommandText = strsql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Conn;
                dt.Load(cmd.ExecuteReader());
            }
            catch
            {

            }
            Conn.Close();
            return dt;
        }

        public DataTable ExecuteReaderOdoo(string strsql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (ConnPG.State == ConnectionState.Closed)
                {
                    ConnPG.ConnectionString = strConnPG;
                    ConnPG.Open();
                }
                pgcmd = new NpgsqlCommand
                {
                    CommandText = strsql,
                    CommandType = CommandType.Text,
                    Connection = ConnPG
                };
                dt.Load(pgcmd.ExecuteReader());
            }
            catch
            {

            }
            ConnPG.Close();
            return dt;
        }

        public static string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }
    }
    //public Thaibaht()
    //{
    //}
    public class ThaibahtMgr
    {
        public ThaibahtMgr()
        {
        }
        private char cha1;
        private string ProcessValue;
        public string Process(string numberVar1)
        {
            string[] NumberWord;
            string[] NumberWord2;
            string Num3 = "";
            cha1 = '.';
            NumberWord = numberVar1.Split(cha1);
            cha1 = ',';
            NumberWord2 = NumberWord[0].Split(cha1);
            for (int i = 0; i <= NumberWord2.Length - 1; i++)
            {
                Num3 = Num3 + NumberWord2[i];
            }
            ProcessValue = SplitWord(Num3);
            if (NumberWord.Length > 1)
            {
                if (int.Parse(NumberWord[1]) > 0)
                {
                    ProcessValue = ProcessValue + "บาท" + SplitWord(NumberWord[1]) + "สตางค์";
                }
                else
                {
                    ProcessValue = ProcessValue + "บาทถ้วน";
                }
            }
            else
            {
                ProcessValue = ProcessValue + "บาทถ้วน";
            }
            return ProcessValue;
        }
        public string SplitWord(string numberVar)
        {
            int i = numberVar.Length;
            int k = 0;
            int n = i;
            int m = i;
            int b = 6;
            //char value2;
            char[] value1;
            string CurrencyWord = "";
            value1 = numberVar.ToCharArray();
            for (int a = 0; a <= i; a = a + 7)
            {
                if (n <= a + 7 && n > 0)
                {
                    b = n - 1;
                    if (i > 7)
                    {
                        k = 1;
                    }
                }
                else
                {
                    b = 6;
                }
                if (n > 0)
                {
                    for (int j = 0; j <= b; j++)
                    {
                        n--;
                        k++;
                        CurrencyWord = GetWord(value1[n].ToString(), k) + CurrencyWord;
                    }
                }
            }
            return CurrencyWord;
        }
        public string GetWord(string str1, int Num1)
        {
            string value1 = GetCurrency(Num1);
            switch (str1)
            {
                case "1":
                    if (Num1 == 1)
                    {
                        value1 = value1 + "เอ็ด";
                    }
                    else if (Num1 > 2)
                    {
                        value1 = "หนึ่ง" + value1;
                    }
                    break;
                case "2":
                    if (Num1 == 2)
                    {
                        value1 = "ยี่" + value1;
                    }
                    else
                    {
                        value1 = "สอง" + value1;
                    }
                    break;
                case "3":
                    value1 = "สาม" + value1;
                    break;
                case "4":
                    value1 = "สี่" + value1;
                    break;
                case "5":
                    value1 = "ห้า" + value1;
                    break;
                case "6":
                    value1 = "หก" + value1;
                    break;
                case "7":
                    value1 = "เจ็ด" + value1;
                    break;
                case "8":
                    value1 = "แปด" + value1;
                    break;
                case "9":
                    value1 = "เก้า" + value1;
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
        public string GetCurrency(int Num2)
        {
            string value1;
            switch (Num2)
            {
                case 1:
                    value1 = "";
                    break;
                case 2:
                    value1 = "สิบ";
                    break;
                case 3:
                    value1 = "ร้อย";
                    break;
                case 4:
                    value1 = "พัน";
                    break;
                case 5:
                    value1 = "หมื่น";
                    break;
                case 6:
                    value1 = "แสน";
                    break;
                case 7:
                    value1 = "ล้าน";
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
    }
}
