using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Register.Models;
using System.Data;
using Register.Connection;
using System.Data.SqlClient;

namespace Register.Cls
{
    public class clsMaritalStatus
    {
        public List<MaritalStatusM> SelectAllMaritalStatus()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spMaritalStatusSelectAll"))
            {

                List<MaritalStatusM> _MaritalStatus = new List<MaritalStatusM>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _MaritalStatus.Add(new MaritalStatusM
                        {
                            ID = dr["ID"].ToString(),
                            MaritalStatusCode = dr["MaritalStatusCode"].ToString(),
                            MaritalStatus = dr["MaritalStatus"].ToString()
                        });
                    }
                }
                return _MaritalStatus;
            }
        }
    }
}