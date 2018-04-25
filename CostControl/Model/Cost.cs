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

        public void InsertCurrentRecord(DataBaseWorker db)
        {
            _id = db.AddCost(this);
        }
    }
}
