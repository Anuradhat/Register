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
    public class clsReligion
    {
        public List<ReligionM> SelectAllReligion()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spReligionSelectAll"))
            {

                List<ReligionM> _Religion = new List<ReligionM>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _Religion.Add(new ReligionM
                        {
                            ID = dr["ID"].ToString(),
                            ReligionCode = dr["ReligionCode"].ToString(),
                            ReligionName = dr["ReligionName"].ToString()
                        });
                    }
                }
                return _Religion;
            }
        }
    }
}