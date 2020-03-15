using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Register.Cls.User;
using Register.Connection;
using Register.Models;

namespace Register.Cls
{
    public class clsMember
    {
        SqlParameter[] p;
        public List<MemberM> SelectAllActiveMemberlist()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spMemberSelectActiveAll"))
            {

                List<MemberM> MemberList = new List<MemberM>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MemberList.Add(new MemberM
                        {
                            ID = dr["ID"].ToString(),
                            MemberID = dr["MemberID"].ToString(),
                            NIC = dr["NIC"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            NameWithInitials = dr["NameWithInitials"].ToString(),
                            CallingName = dr["CallingName"].ToString(),
                            DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString()),
                            MaritalStatusCode = dr["MaritalStatusCode"].ToString(),
                            GenderCode = dr["GenderCode"].ToString(),
                            ReligionCode = dr["ReligionCode"].ToString(),
                            Address = dr["Address"].ToString(),
                            City = dr["City"].ToString(),
                            Country = dr["Country"].ToString(),
                            MobileNo = int.Parse(dr["MobileNo"].ToString()),
                            HomeTel = int.Parse(dr["HomeTel"].ToString()),
                            Email = dr["Email"].ToString(),
                            GCEOL = bool.Parse(dr["GCEOL"].ToString()),
                            GCEAL = bool.Parse(dr["GCEAL"].ToString()),
                            OtherQualification = dr["OtherQualification"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            ActiveMember = bool.Parse(dr["ActiveMember"].ToString()),
                            Comments = dr["Comments"].ToString()
                        });
                    }
                }
                return MemberList;
            }
        }

        public void CreateMember(string MemberID,string NIC,string FullName,string NameWithInitials,string CallingName,DateTime DateOfBirth,
         string MaritalStatusCode, string GenderCode,string ReligionCode, string Address, string City,string Country,int MobileNo,
         int HomeTel,string Email,bool GCEOL,bool GCEAL, string OtherQualification,bool ActiveMember, string Comments)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;

            p = new SqlParameter[21];

            p[0] = new SqlParameter("@MemberID", SqlDbType.VarChar, 50);
            p[0].Value = MemberID;
            p[1] = new SqlParameter("@NIC", SqlDbType.VarChar, 100);
            p[1].Value = NIC;
            p[2] = new SqlParameter("@FullName", SqlDbType.VarChar, 50);
            p[2].Value = FullName;
            p[3] = new SqlParameter("@NameWithInitials", SqlDbType.VarChar, 255);
            p[3].Value = NameWithInitials;
            p[4] = new SqlParameter("@CallingName", SqlDbType.VarChar, 50);
            p[4].Value = CallingName;
            p[5] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime, 100);
            p[5].Value = DateOfBirth;
            p[6] = new SqlParameter("@MaritalStatusCode", SqlDbType.VarChar, 50);
            p[6].Value = MaritalStatusCode;
            p[7] = new SqlParameter("@GenderCode", SqlDbType.VarChar, 50);
            p[7].Value = GenderCode;
            p[8] = new SqlParameter("@ReligionCode", SqlDbType.VarChar);
            p[8].Value = ReligionCode;
            p[9] = new SqlParameter("@Address", SqlDbType.VarChar, 50);
            p[9].Value = Address;
            p[10] = new SqlParameter("@City", SqlDbType.VarChar, 50);
            p[10].Value = City;
            p[11] = new SqlParameter("@Country", SqlDbType.VarChar, 50);
            p[11].Value = Country;
            p[12] = new SqlParameter("@MobileNo", SqlDbType.Int);
            p[12].Value = MobileNo;
            p[13] = new SqlParameter("@HomeTel", SqlDbType.Int);
            p[13].Value = HomeTel;
            p[14] = new SqlParameter("@Email", SqlDbType.VarChar);
            p[14].Value = Email;
            p[15] = new SqlParameter("@GCEOL", SqlDbType.Bit);
            p[15].Value = GCEOL;
            p[16] = new SqlParameter("@GCEAL", SqlDbType.Bit);
            p[16].Value = GCEAL;
            p[17] = new SqlParameter("@OtherQualification", SqlDbType.VarChar);
            p[17].Value = OtherQualification;
            p[18] = new SqlParameter("@TrnUser", SqlDbType.VarChar);
            p[18].Value = idendity.User.Username;
            p[19] = new SqlParameter("@ActiveMember", SqlDbType.Bit);
            p[19].Value = ActiveMember;
            p[20] = new SqlParameter("@Comments", SqlDbType.VarChar);
            p[20].Value = Comments;

            try
            {
                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spMemberCreate", p);


                //return p[12].Value.ToString();

            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
        }

        public List<MemberM> SelectAllMemberlistByMemberID(string MemberID)
        {
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@MemberID", SqlDbType.VarChar, 50);
            p[0].Value = MemberID;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spMemberSelectByMemberID",p))
            {

                List<MemberM> MemberList = new List<MemberM>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        MemberList.Add(new MemberM
                        {
                            ID = dr["ID"].ToString(),
                            MemberID = dr["MemberID"].ToString(),
                            NIC = dr["NIC"].ToString(),
                            FullName = dr["FullName"].ToString(),
                            NameWithInitials = dr["NameWithInitials"].ToString(),
                            CallingName = dr["CallingName"].ToString(),
                            DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString()),
                            MaritalStatusCode = dr["MaritalStatusCode"].ToString(),
                            GenderCode = dr["GenderCode"].ToString(),
                            ReligionCode = dr["ReligionCode"].ToString(),
                            Address = dr["Address"].ToString(),
                            City = dr["City"].ToString(),
                            Country = dr["Country"].ToString(),
                            MobileNo = int.Parse(dr["MobileNo"].ToString()),
                            HomeTel = int.Parse(dr["HomeTel"].ToString()),
                            Email = dr["Email"].ToString(),
                            GCEOL = bool.Parse(dr["GCEOL"].ToString()),
                            GCEAL = bool.Parse(dr["GCEAL"].ToString()),
                            OtherQualification = dr["OtherQualification"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            ActiveMember = bool.Parse(dr["ActiveMember"].ToString()),
                            Comments = dr["Comments"].ToString()
                        });
                    }
                }
                return MemberList;
            }
        }

    }
}