/* Цей файл — частина MoneyCon.

   Moneycon - вільна програма: ви можете повторно її розповсюджувати та/або
   змінювати її на умовах Стандартної суспільної ліцензії GNU в тому вигляді,
   в якому вона була опублікована Фондом вільного програмного забезпечення;
   або третьої версії ліцензії, або (зігдно з вашим вибором) будь-якої наступної
   версії.

   Moneycon розповсюджується з надією, що вона буде корисною,
   але БЕЗ БУДЬ-ЯКИХ ГАРАНТІЙ; навіть без неявної гарантії ТОВАРНОГО ВИГЛЯДУ
   або ПРИДАТНОСТІ ДЛЯ КОНКРЕТНИХ ЦІЛЕЙ. Детальніше див. в Стандартній
   суспільній ліцензії GNU.

   Ви повинні були отримати копію Стандартної суспільної ліцензії GNU
   разом з цією програмою. Якщо це не так, див.
   <http://www.gnu.org/licenses/>.*/

using System;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace App2
{
    public static class DataBase
    {
        //область бази даних
        private static SqliteConnection newCon;
        static string documentsPath = Android.OS.Environment.DataDirectory.AbsolutePath;
        static string path = string.Concat(documentsPath, "/data/MoneyCon.MoneyCon/localbase.db");
		//static string path = "localbase.db";
        private static string connectionStr = "Data Source=" + path + "; Version=3;";
        private static string secureCon = connectionStr;
        private static System.Text.StringBuilder queryBuilder = new System.Text.StringBuilder();
		private static string Key;
		//private static byte[] pass;
		//private static byte[] IV;

        //область делегатів
        public delegate void outputMeth1(string str1, string str2);
        public delegate void outputMeth2(string str1, string str4, string str5);

        //область допоміжних полів
        private static DateTime fewTimesCont;
        private static bool passworded;
		private static System.Globalization.CultureInfo country = new System.Globalization.CultureInfo("uk-UA");

        //Область властивостей
        public static bool IsPassworded
        {
            get
            {
                return passworded;
            }
            set
            {
                passworded = value;
            }
        }
		public static byte[] GetPass()
		{
			System.Security.Cryptography.SHA256 temp = SHA256.Create ();
            if (Key != null || Key != String.Empty)
            {
                temp.ComputeHash(Conventer.GetNums(Key.ToCharArray()));
            }
            else
            {
                throw new NullReferenceException("Пароль не встановло або не зчитано");
            }
			return temp.Hash;
		}
		public static byte[] GetIV()
		{
			System.Security.Cryptography.MD5 temp = MD5.Create ();
			temp.ComputeHash (Conventer.GetNums (Key.ToCharArray ()));
			return temp.Hash;
		}
		public static void SetKey(string passw)
		{
			Key = passw;
            passw = Conventer.BytesToString(GetPass());
            string sql = "insert into DBFirst (first) values ('" + passw + "')";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
        }
        public static bool CheckPass(string tryPass)
        {
            string sql = "select * from DBFirst";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool readerRead = reader.Read();
            readerRead = reader.Read();
            string truePass = reader["first"].ToString();
            SHA256 passChecker = SHA256.Create();
            if (truePass == Conventer.BytesToString(passChecker.ComputeHash(Conventer.GetNums(tryPass.ToCharArray()))))
            {
                Key = tryPass;
                return true;
            }
            else
            {
                return false;
            }
        }


        //Область методів
        private static bool CheckBase()
        {
            string sql = "select * from DB";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool readerRead = reader.Read();
            if ((reader["date"]).ToString().Length > 10)
            {
                IsPassworded = true;
                return true;
            }
            IsPassworded = false;
            return false;
        }
        public static string GetStartPath()
        {
            return Path.GetDirectoryName(path);
        }
        public static void Open()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            newCon = new SqliteConnection(connectionStr);
            newCon.Open();
			string sql = "create table if not exists DB (date varchar(20), price varchar(10), desc varchar(200))";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            CheckBase();
                
        }

       
        public static int FirstCheck()
        {
            string sql = "select * from DBFirst";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();
            int FirstStart = Int32.Parse(reader["first"].ToString());
            reader.Close();
            command.ExecuteNonQuery();
            return FirstStart;
        }

        public static void FirstStart()
        {
            string sqlF = "create table if not exists DBFirst (first varchar(300))";
            SqliteCommand command = new SqliteCommand(sqlF, newCon);
            command.ExecuteNonQuery();
            sqlF = "insert into DBFirst (first) values ('1')";
            command = new SqliteCommand(sqlF, newCon);
            command.ExecuteNonQuery();
        }
        
		public static void AddRec(string date, string price, string desc)
        {
			string sql = queryBuilder.Append("insert into DB (date, price, desc) values (").Append("'").Append(date).Append("', '").Append(price.ToString()).Append("', '").Append(desc).Append("')").ToString();
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            queryBuilder.Clear();
        }
        private static int CalcElem()
        {
            string sql = "select * from DB";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool readerRead = reader.Read();
            int i = 0;
            while (readerRead)
            {
                i++;
                readerRead = reader.Read();
            }
            reader.Close();
            command.ExecuteNonQuery();
            return i;
        }
		public static void ReadRec (outputMeth2 output)
        {
            output("Дата ", "Ціна ", "Опис");
            string sql = "select * from DB";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            int numElem = CalcElem();
            reader.Read();
            for(int i = 0; i<numElem; i++)
            {
				if (CheckBase()) 
				{
					try 
					{
						DateTime encCh = DateTime.ParseExact ((string)reader ["date"], "dd/MM/yyyy", country);
					} 
					catch (FormatException e) 
					{
						Record dec = new Record ();
						dec.Init ((string)reader ["date"], (string)reader ["price"], (string)reader ["desc"]);
						dec.DecryptAll ();
						output (dec.Date, dec.Price, dec.Desc);
                        reader.Read();
					}
				} 
				else 
				{
					output ((string)reader ["date"], (string)reader ["price"], (string)reader ["desc"]);
                    reader.Read();
				}
            }
            reader.Close();
            command.ExecuteNonQuery();
        }
		public static void LookFor(DateTime query, outputMeth1 output)
        {
            if (fewTimesCont == query)
            {
                output("Опис ", "Ціна");
            }
			string sql;
			if(!CheckBase())
				sql = queryBuilder.AppendFormat("select price, desc from DB where date = '{0}'",query.ToShortDateString()).ToString();
			else
			{
				Record rec = new Record ();
				rec.Init (query.ToShortDateString (), "-1", "-1");
				rec.EncryptAll ();
				sql = queryBuilder.AppendFormat("select price, desc from DB where date = '{0}'",rec.Date).ToString(); 
			}
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool readerRead = reader.Read();
            while (readerRead)
            {
				if (!CheckBase()) 
				{
					output ((string)reader ["desc"] + "   ", (string)reader ["price"] + " ");
					readerRead = reader.Read ();
				}
				else 
				{
					Record temp = new Record ();
					temp.Init ("-1", (string)reader ["price"], (string)reader ["desc"]);
					temp.DecryptAll ();
					output (temp.Desc, temp.Price);
					readerRead = reader.Read ();
				}
            }
            reader.Close();
            command.ExecuteNonQuery();
            queryBuilder.Clear();
        }
        public static void LookFor(string desc)
        {
            string sql = "select price, desc from DB where desc = '" + desc + "'";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("\nЦіна: " + reader["price"] + "\nОпис: " + reader["desc"]);
            reader.Close();
            command.ExecuteNonQuery();
        }
		public static double DaySum(DateTime query)
        {
            double sum = 0, temp;
            string sql;
            if (!CheckBase())
                sql = queryBuilder.AppendFormat("select price from DB where date = '{0}'", query.ToShortDateString()).ToString();
            else
            {
                Record rec = new Record();
                rec.Init(query.ToShortDateString(), "-1", "-1");
                rec.EncryptAll();
                sql = queryBuilder.AppendFormat("select price from DB where date = '{0}'", rec.Date).ToString();
            }
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool canRead = reader.Read();
            while (canRead)
            {
				if (!CheckBase()) 
				{
					temp = double.Parse (reader ["price"].ToString ());
					sum += temp;
				}
				else 
				{
					Record t = new Record ();
					t.Init ("-1", (string)reader ["price"], "-1");
					t.DecryptAll ();
					sum += Double.Parse (t.Price);
				}
                canRead = reader.Read();
            }
            reader.Close();
            command.ExecuteNonQuery();
            queryBuilder.Clear();
            return sum;
        }
		public static double MonthSum(DateTime query)
        {
			double sum = 0;
			DateTime bDate = new DateTime (query.Year, query.Month, 1);
			DateTime eDate = bDate;
			eDate.AddMonths (1);
			while (bDate != eDate) 
			{
				sum = DaySum (bDate);
				bDate.AddDays (1);
			}
            return sum;
        }
		public static double PerSum(DateTime bDate, DateTime eDate, outputMeth1 output)
        {
            double sum = 0;
            fewTimesCont = bDate;
            eDate = eDate.AddDays(1);
            while (bDate != eDate)
            {
				LookFor(bDate,output);
                sum += DaySum(bDate);
                bDate = bDate.AddDays(1);
            }
            return sum;
        }
        public static void Delete(string date, string price , string desc)
        {
            string sql = queryBuilder.AppendFormat("delete from DB where date = '{0}' and price = '{1}' and desc = '{2}'", date, price, desc).ToString();
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            queryBuilder.Clear();
        }
        public static void Close()
        {
            secureCon = connectionStr;
            newCon.Close();
        }

        public static void EncryptAll()
		{
			ReadRec (EncRec);
        }
		static void EncRec(string str1, string str2, string str3)
		{
			if (str1 == "Дата ") 
			{
				return;
			}
			Record temp = new Record ();
			temp.Init (str1, str2, str3);
			temp.EncryptAll ();
			DateTime ttime;
			try
			{
				ttime = DateTime.ParseExact (str1, "dd/MM/yyyy", country);
			}
			catch(FormatException e) 
			{
				return;
			}
			Delete (ttime.ToShortDateString(), str2, str3);
			AddRec (temp.Date, temp.Price, temp.Desc);
		}
		public static void DecryptAll()
		{
			ReadRec (DecRec);
		}
		static void DecRec(string str1, string str2, string str3)
		{
			if (str1 == "Дата ") 
			{
				return;
			}
			Record temp = new Record ();
			temp.Init (str1, str2, str3);
            Record toDel = new Record();
            toDel.Init(temp.Date, temp.Price, temp.Desc);
            toDel.EncryptAll();
            Delete(toDel.Date, toDel.Price, toDel.Desc);
            AddRec(temp.Date, temp.Price, temp.Desc);
            return;
		}
    }
}

