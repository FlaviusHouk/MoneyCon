using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostControl.Model
{
    class Cost
    {
        private readonly double _price;
        private readonly string _desc, _category;
        private readonly DateTime _performedDate;
        private int _id;

        public Cost(DateTime date, double price, string desc, string category)
        {
            _price = price;
            _performedDate = date;
            _desc = desc;
            _category = category;
            _id = -1;
        }

        /// <summary>
        /// Варто використовувати цей конструктор тільки для створення об'єктів під час зчитування з бази
        /// </summary>
        /// <param name="id">Ідентифікатор витрати</param>
        /// <param name="date">Дата здійснення</param>
        /// <param name="price">Ціна</param>
        /// <param name="desc">Опис витрати</param>
        /// <param name="category">Номер категорії</param>
        public Cost(int id, DateTime date, double price, string desc, string category)
        {
            _price = price;
            _performedDate = date;
            _desc = desc;
            _category = category;
            _id = id;
        }

        public double Price
        {
            get
            {
                return _price;
            }
        }

        public string Desc
        {
            get
            {
                return _desc;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
        }

        public DateTime PerformedDate
        {
            get
            {
                return _performedDate;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public bool InBase
        {
            get
            {
                return _id != -1;
            }
        }

        public void InsertCurrentRecord(DataBaseWorker db)
        {
            _id = db.AddCost(this);
        }
    }
}
