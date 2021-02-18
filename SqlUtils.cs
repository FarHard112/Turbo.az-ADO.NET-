using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turbo.az
{
   class SqlUtils
    {
        private static SqlUtils sqlUtil { get; set; }


        public string conString { get; set; }
        private SqlUtils()
        {
           
            conString = ConfigurationManager.ConnectionStrings["MainConString"].ConnectionString;
        }
        public static SqlUtils GetInstance()
        {
            if(sqlUtil == null)
            {
                sqlUtil = new SqlUtils();              
            }
            return sqlUtil;
        }

        public DataTable GetDataWithAdapter(string _query)
        {
            SqlConnection sqlConnection = new SqlConnection(conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_query,sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
        
    }
}
