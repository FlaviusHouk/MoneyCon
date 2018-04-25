using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CostControl.Model
{
    class DataBaseWorker
    {
        private const string _connectionString = @"Server=FLAVIUSWINHP\SQLEXPRESS; Database=MoneyCon; User ID=MoneyCon_Internal; Password=12345678";
        private SqlConnection _conn;

        public DataBaseWorker()
        {
            _conn = new SqlConnection(_connectionString);

            _conn.Open();
        }

        public int AddCost(Cost newObj)
        {
            int category = -1;//TODO: Пошук категорії

            SqlCommand com = new SqlCommand("dbo.AddCost", _conn);
            com.CommandType = System.Data.CommandType.StoredProcedure;

            com.Parameters.Add("@PerformedDate", System.Data.SqlDbType.Date);
            com.Parameters["@PerformedDate"].Value = newObj.PerformedDate.ToString("yyyy-MM-dd");

            com.Parameters.Add("@Description", System.Data.SqlDbType.NChar, 512);
            com.Parameters["@Description"].Value = newObj.Desc;

            com.Parameters.Add("@Category", System.Data.SqlDbType.Int);
            com.Parameters["@Category"].Value = category;

            com.Parameters.Add("@Price", System.Data.SqlDbType.Real);
            com.Parameters["@Price"].Value = newObj.Price; 

            com.Parameters.Add("@ID", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
            com.ExecuteNonQuery();

            int id = Convert.ToInt32(com.Parameters["@ID"].Value);

            return id;
        }
    }
}
