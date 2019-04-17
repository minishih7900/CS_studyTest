using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Study.Models.Models;


namespace Study.DataAccess
{
    public class SqlRepository:BaseRepository
    {
        public bool insertMember (List<member> mm)
        {
            var sql = @"INSERT INTO [dbo].[member]
           ([email]
           ,[password])
             VALUES
           (@email,
           @password)";
            return _dbDapper.NonQuerySQL(sql, mm) > 0;
        }
        public List<LotNumber> GetSelectLotNumber(string selectnum, string StartDate, string EndDate, string StartPeriod, string EndPeriod)
        {
            var sqlTemp = "";
            var sql = @"
select * from [dbo].[開獎記錄表]
where (號碼1=@num or 號碼2=@num or  號碼3=@num or 號碼4=@num or 號碼4=@num) {0}
";
            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                sqlTemp= sqlTemp + "and (開獎日期 between @startdate and @enddate) ";
            }
            if (StartPeriod !="00000000" && EndPeriod != "00000000")
            {
                sqlTemp = sqlTemp + "and 期數 between @startperiod and @endperiod ";
            }
            sql = string.Format(sql, sqlTemp);

            var param = new Dictionary<string, object>();
            param.Add("num", selectnum);
            param.Add("startdate", StartDate);
            param.Add("enddate", EndDate);
            param.Add("startperiod", StartPeriod);
            param.Add("endperiod", EndPeriod);

            return _dbDapper.QueryList<LotNumber>(sql, param);
        }
        
        public List<LotNumber> GetLotNumber(string startDate ,string endDate)
        {
            var sql = @"
SELECT           *
FROM              開獎記錄表
WHERE          (開獎日期 BETWEEN @StartDate AND @EndDate)
";
            return _dbDapper.QueryList<LotNumber>(sql,new { StartDate=startDate,EndDate=endDate});
        }
        public List<LotNumber> GetLotNumberNewTop(string newCount)
        {
            var sql = @"
SELECT   Top " + newCount  + @"*
FROM              開獎記錄表
order by 期數 desc";
            return _dbDapper.QueryList<LotNumber>(sql, null);
        }
        public bool InputLotNumber(LotNumber data)
        {
            var sql = @"
INSERT INTO [dbo].[開獎記錄表]
           ([期數]
           ,[開獎日期]
           ,[號碼1]
           ,[號碼2]
           ,[號碼3]
           ,[號碼4]
           ,[號碼5])
     VALUES
           (@期數
           ,@開獎日期
           ,@號碼1
           ,@號碼2
           ,@號碼3
           ,@號碼4
           ,@號碼5)
";
            return _dbDapper.NonQuerySQL(sql, data) > 0;
        }

        public string GetMaxNo()
        {
            var sql = @"select max(期數) from [dbo].[開獎記錄表]";
            return _dbDapper.ExecuteScalarSQL<string>(sql,null);
        }
        public 累計記錄表 GetCountNumber()
        {
            var sql = @"select top(1) * from [dbo].[累計記錄表] order by 日期 desc";
            return _dbDapper.ExecuteScalarSQL<累計記錄表>(sql, null);
        }
    }
}
