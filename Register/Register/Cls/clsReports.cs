using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Register.Connection;
using Register.Models;

namespace Register.Cls
{
    public class clsReports
    {
        public List<emsReports> SelectAllReportslist()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spSelectAllFromReports"))
            {

                List<emsReports> locleListReports = new List<emsReports>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        locleListReports.Add(new emsReports
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            ReportCategory = dr["ReportCategory"].ToString(),
                            ReportName = dr["ReportName"].ToString(),
                            ReportNumber = int.Parse(dr["ReportNumber"].ToString()),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            ReportDisplayName = dr["ReportDisplayName"].ToString(),
                            ReportFolder = dr["ReportFolder"].ToString(),
                            ServerPath = dr["ServerPath"].ToString()
                        });
                    }
                }
                return locleListReports;
            }
        } 
    }
}