using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CostControl.Helpers;

namespace CostControl.Model
{
    public class DataBaseWorker
    {
        private const string _connectionString = @"Server=MOZGOKLUY-PC; Database=MoneyCon; User ID=MoneyCon_Internal; Password=12345678";
        private SqlConnection _conn;
        private Dictionary<int, string> _categories;

        public List<string> Categories { get { return _categories.Values.ToList(); } }

        public DataBaseWorker()
        {
            _conn = new SqlConnection(_connectionString);

            _conn.Open();
            ReadCategories();
        }

        private void ReadCategories()
        {
            _categories = new Dictionary<int, string>();

            SqlCommand com = new SqlCommand("SELECT * FROM [dbo].Categories", _conn);
            var reader = com.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader.GetValue(0));
                string name = reader.GetValue(1).ToString();
                _categories.Add(id, name);
            }
            reader.Close();
        }

        public int AddCost(Cost newObj)
        {
            SqlCommand com = new SqlCommand("dbo.AddCost", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@PerformedDate", System.Data.SqlDbType.Date);
            com.Parameters["@PerformedDate"].Value = newObj.PerformedDate.ToString("yyyy-MM-dd");

            com.Parameters.Add("@Description", System.Data.SqlDbType.NChar, 512);
            com.Parameters["@Description"].Value = newObj.Desc;

            com.Parameters.Add("@Category", System.Data.SqlDbType.Int);

            int categoryNum = _categories.Values.ToList().IndexOf(newObj.Category);
            if (categoryNum > 0)
                com.Parameters["@Category"].Value = _categories.Keys.ElementAt(categoryNum);
            else
                com.Parameters["@Category"].Value = -1;

            com.Parameters.Add("@Price", System.Data.SqlDbType.Real);
            com.Parameters["@Price"].Value = newObj.Price;

            com.Parameters.Add("@ID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
            com.ExecuteNonQuery();

            int id = Convert.ToInt32(com.Parameters["@ID"].Value);

            return id;
        }

        public void UpdateRecord(Cost oldRec, Cost newRec)
        {
            if (oldRec.InBase)
            {
                SqlCommand com = new SqlCommand("dbo.UpdateCost", _conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.Add("@PerformedDate", System.Data.SqlDbType.Date);
                com.Parameters["@PerformedDate"].Value = newRec.PerformedDate.ToString("yyyy-MM-dd");

                com.Parameters.Add("@Description", System.Data.SqlDbType.NChar, 512);
                com.Parameters["@Description"].Value = newRec.Desc;

                com.Parameters.Add("@Category", System.Data.SqlDbType.Int);

                int categoryNum = _categories.Values.ToList().IndexOf(newRec.Category);
                if (categoryNum > 0)
                    com.Parameters["@Category"].Value = _categories.Keys.ElementAt(categoryNum);
                else
                    com.Parameters["@Category"].Value = -1;

                com.Parameters.Add("@Price", System.Data.SqlDbType.Real);
                com.Parameters["@Price"].Value = newRec.Price;

                com.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                com.Parameters["@ID"].Value = oldRec.Id;

                com.ExecuteNonQuery();

            }
        }

        public void DeleteCost(Cost toRem)
        {
            if (toRem.InBase)
            {
                SqlCommand com = new SqlCommand("dbo.DeleteCost", _conn);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                com.Parameters["@ID"].Value = toRem.Id;

                com.ExecuteNonQuery();
            }
        }

        public IEnumerable<Cost> GetRecordsByDate(DateTime date)
        {
            SqlCommand com = new SqlCommand("dbo.GetRecordsByDate", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@Date", System.Data.SqlDbType.Date);
            com.Parameters["@Date"].Value = date.ToString("yyyy-MM-dd");

            var reader = com.ExecuteReader();
            List<Cost> toRet = new List<Cost>();

            while (reader.Read())
            {
                string desc = reader.GetString(3);
                int cat = reader.GetInt32(4);
                double price = Convert.ToDouble(reader.GetValue(2));
                int id = reader.GetInt32(0);

                Cost read = new Cost(id, date, price, desc, _categories[cat]);
                toRet.Add(read);
            }
            reader.Close();

            return toRet;
        }

        public IEnumerable<Cost> GetRecordsByDateSpan(DateTime bDate, DateTime eDate)
        {
            SqlCommand com = new SqlCommand("dbo.GetRecordsByDateSpan", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@bDate", System.Data.SqlDbType.Date);
            com.Parameters["@bDate"].Value = bDate.ToString("yyyy-MM-dd");

            com.Parameters.Add("@eDate", System.Data.SqlDbType.Date);
            com.Parameters["@eDate"].Value = eDate.ToString("yyyy-MM-dd");

            var reader = com.ExecuteReader();
            List<Cost> toRet = new List<Cost>();

            while (reader.Read())
            {
                string desc = reader.GetString(3);
                int cat = reader.GetInt32(4);
                double price = Convert.ToDouble(reader.GetValue(2));
                int id = reader.GetInt32(0);
                var s = reader.GetValue(1).ToString();
                DateTime date;
                DateTime.TryParse(s, out date);

                Cost read = new Cost(id, date, price, desc, _categories[cat]);
                toRet.Add(read);
            }
            reader.Close();
            return toRet;
        }

        public IEnumerable<Cost> GetRecordsByCategory(int categoryIndex)
        {
            return GetRecordsByCategory(_categories[categoryIndex]);
        }

        public IEnumerable<Cost> GetRecordsByCategory(string category)
        {
            SqlCommand com = new SqlCommand("dbo.GetRecordsByCategory", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@Category", System.Data.SqlDbType.NChar);
            com.Parameters["@Category"].Value = category;

            var reader = com.ExecuteReader();
            List<Cost> toRet = new List<Cost>();

            while (reader.Read())
            {
                string desc = reader.GetString(3);
                double price = Convert.ToDouble(reader.GetValue(2));
                int id = reader.GetInt32(0);
                var s = reader.GetValue(1).ToString();
                DateTime date;
                DateTime.TryParse(s, out date);
                Cost read = new Cost(id, date, price, desc, category);
                toRet.Add(read);
            }
            reader.Close();
            return toRet;
        }

        public IEnumerable<Cost> GetRecordsByDescription(string desc)
        {
            SqlCommand com = new SqlCommand("dbo.GetRecordsByDescription", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@Desc", System.Data.SqlDbType.NChar);
            com.Parameters["@Desc"].Value = desc;

            var reader = com.ExecuteReader();
            List<Cost> toRet = new List<Cost>();

            while (reader.Read())
            {
                string description = reader.GetString(3);
                double price = Convert.ToDouble(reader.GetValue(2));
                int cat = reader.GetInt32(4);
                int id = reader.GetInt32(0);
                var s = reader.GetValue(1).ToString();
                DateTime date;
                DateTime.TryParse(s, out date);
                Cost read = new Cost(id, date, price, desc, _categories[cat]);
                toRet.Add(read);
            }
            reader.Close();
            return toRet;
        }
    }
}
