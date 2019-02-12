using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyCon.ViewModel.Database
{
    public interface IDatabaseWorker
    {
        Dictionary<int, string> Categories { get; }

        bool AddCategory(string name);
        bool RemoveCategory(int index);
        int AddCost(Cost newObj);
        void UpdateRecord(Cost oldRec, Cost newRec);
        void DeleteCost(Cost toRem);
        IEnumerable<Cost> GetRecords();
        IEnumerable<Cost> GetRecordsByDate(DateTime date);
        IEnumerable<Cost> GetRecordsByDateSpan(DateTime bDate, DateTime eDate);
        IEnumerable<Cost> GetRecordsByCategory(int categoryIndex);
        IEnumerable<Cost> GetRecordsByCategory(string category);
        IEnumerable<Cost> GetRecordsByDescription(string desc);
        double GetSumByDate(DateTime date);
        double GetSumByDateSpan(DateTime bDate, DateTime eDate);
        double GetSumByCategory(int categoryIndex);
        double GetSumByCategory(string category);
        double GetSumByDescription(string desc);
        double GetAvgByDate(DateTime date);
        double GetAvgByDateSpan(DateTime bDate, DateTime eDate);
        double GetAvgByCategory(int categoryIndex);
        double GetAvgByCategory(string category);
        double GetAvgByDescription(string desc);
    }
}
