using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Register.Cls;
using Register.Models;
using System.Data;
using Register.Connection;

namespace Register.Cls
{
    public class clsGender
    {
        public List<GenderM> SelectAllGender()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spGenderSelectAll"))
            {

                List<GenderM> _Gender = new List<GenderM>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        _Gender.Add(new GenderM
                        {
                            ID = dr["ID"].ToString(),
                            GenderCode = dr["GenderCode"].ToString(),
                            Gender = dr["Gender"].ToString()
                        });
                    }
                }
                return _Gender;
            }
        }
    }
}