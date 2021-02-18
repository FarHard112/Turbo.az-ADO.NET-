using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turbo.az
{
    class ClssInfoAdapter
    {
        SqlUtils sqlUtils = SqlUtils.GetInstance();
        public DataTable GetBrands()
        {
           
            string query = @"   select -1 ID ,'Butun markalar' NAME
             union 
              select id,name from BRANDS  ";
            return sqlUtils.GetDataWithAdapter(query);
        }

        public DataTable GetModels(string brand_ID)
        {
            string query = $"SELECT ID,NAME FROM MODELS WHERE BRAND_ID = {brand_ID}";
            return sqlUtils.GetDataWithAdapter(query);
        }
        public DataTable GetGeneralInfoForID(string type_id)
        {

            string query = $"SELECT ID,NAME FROM GENERAL_INFO WHERE TYPE_ID={type_id}";
            return sqlUtils.GetDataWithAdapter(query);

        }
        public DataTable GetImage(string adsId)
        {

            string query = $"SELECT [ID],[CAR_IMAGE],[ADS_ID]  FROM [dbo].[ADS_IMAGES] WHERE ADS_ID ={adsId}";
            return sqlUtils.GetDataWithAdapter(query);
        }




    }
}
