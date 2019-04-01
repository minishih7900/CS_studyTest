using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Study.DataAccess;
using Study.Models.Models;
using System.Transactions;

namespace Study.Services
{
    public class HomeServices
    {
        public SqlRepository _sqlRepository = new SqlRepository();

        public List<LotNumber> GetNumberServices(string StartDate ,string EndDate)
        {
            return _sqlRepository.GetLotNumber(StartDate, EndDate);
        }
        public List<LotNumber> GetNumberTopServices(string newCount)
        {
            return _sqlRepository.GetLotNumberNewTop(newCount);
        }
        public string GetMaxNoServices()
        {
            return _sqlRepository.GetMaxNo();
        }

        public bool AddNumberServices(LotNumber  data)
        {
            bool success = false;
            List<string> inputNum = new List<string> { data.號碼1, data.號碼2, data.號碼3, data.號碼4, data.號碼5 };
            累計記錄表 countReport =_sqlRepository.GetCountNumber();
            foreach (var item in inputNum)
            {
               
            }
            using (var scope = new TransactionScope())
            {
                success = _sqlRepository.InputLotNumber(data);
                if (success)
                    scope.Complete();
            }
            return success;
        }
    }
}
