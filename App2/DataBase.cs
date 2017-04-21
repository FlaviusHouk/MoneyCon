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
        #region
        //private static SQLite.SQLiteAsyncConnection newCon;
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
        #endregion

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
        public static async void Open()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            newCon = new SqliteConnection(connectionStr);
            await newCon.OpenAsync();
			string sql = "create table if not exists DB (date varchar(20), price varchar(10), desc varchar(200), tag varchar(30))";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            CheckBase();
            CreateTagTab();
            CreateTemplateTab();
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
        
		private static void AddRec(string date, string price, string desc)
        {
			string sql = queryBuilder.Append("insert into DB (date, price, desc) values (").Append("'").Append(date).Append("', '").Append(price.ToString()).Append("', '").Append(desc).Append("')").ToString();
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            queryBuilder.Clear();
        }
        public static void AddTemp(string price, string desc, string tag)
        {
            string sql = queryBuilder.Append("insert into Templates (price, desc, tag) values (").Append("'").Append(price).Append("', '").Append(desc).Append("', '").Append(tag).Append("')").ToString();
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
            queryBuilder.Clear();
        }
        public static void AddRec(string date, string price, string desc, string tag)
        {
            string sql = queryBuilder.Append("insert into DB (date, price, desc, tag) values (").Append("'").Append(date).Append("', '").Append(price.ToString()).Append("', '").Append(desc).Append("', '").Append(tag).Append("')").ToString();
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
		public static void ReadRec (Action<string, string, string> output)
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
		public static void LookFor(DateTime query, Action<string,string> output)
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
        public static void LookFor(string tag, Action<string, string, string> output)
        {
            output("Тег", "Опис", "Ціна");
            string sql = "select date, price, desc from DB where tag = '" + tag + "'";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                output((string)reader["date"], (string)reader["price"], (string)reader["desc"]);
            }
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
		public static double PerSum(DateTime bDate, DateTime eDate, Action<string, string> output)
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

        private static void CreateTagTab()
        {
            string sql = "CREATE TABLE IF NOT EXISTS Tags(name varchar(30))";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
        }
        public static void AddTag(string tag)
        {
            System.Text.StringBuilder command = new System.Text.StringBuilder();
            command.Append("SELECT 1 FROM Tags WHERE name = '").Append(tag).Append("';");
            SqliteCommand comand = new SqliteCommand(command.ToString(), newCon);
            SqliteDataReader reader = comand.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                command.Clear();
                command.Append("INSERT INTO Tags(name) values ('").Append(tag).Append("')");
                comand.CommandText = command.ToString();
                comand.ExecuteNonQuery();
                return;
            }
            else
            {
                throw new SqliteException("Такий тег уже існує");
            }
            reader.Close();
        }
        public static void ReadTags(Action<string> output)
        {
            string sql = "SELECT * FROM Tags";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool read = reader.Read();
            while (read)
            {
                output((string)reader["name"]);
                read = reader.Read();
            }
            reader.Close();
            command.ExecuteNonQuery();
        }
        public static void DeleteTag(string tag)
        {
            System.Text.StringBuilder command = new System.Text.StringBuilder();
            command.Append("DELETE FROM Tags WHERE name = '").Append(tag).Append("';");
            SqliteCommand com = new SqliteCommand(command.ToString(), newCon);
            com.ExecuteNonQuery();
        }
        private static void CreateTemplateTab()
        {
            string sql = "create table if not exists Templates (price varchar(10), desc varchar(200), tag varchar(30))";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            command.ExecuteNonQuery();
        }
        public static void ReadTemplates(Action<string, string, string> output)
        {
            string sql = "SELECT * FROM Templates";
            SqliteCommand command = new SqliteCommand(sql, newCon);
            SqliteDataReader reader = command.ExecuteReader();
            bool read = reader.Read();
            while (read)
            {
                output((string)reader["price"], (string)reader["desc"], (string)reader["tag"]);
                read = reader.Read();
            }
            reader.Close();
            command.ExecuteNonQuery();
        }
    }
}

