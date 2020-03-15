using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Register.Cls.User;
using Register.Connection;
using Register.Models;

namespace Register
{
    public class clsU_User
    {
        #region Fields

        private string strquery = "";
        private SqlParameter[] p;
        private List<emsRecentActivityU> lstRecentActivity;
        private SqlDataReader reader;
        #endregion


        public static void WriteUserActivity(RecentActivity recentActivity)
        {

            SqlParameter[] p = new SqlParameter[7];

            try
            {
                p[0] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[0].Value = recentActivity.TrnUser;
                p[1] = new SqlParameter("@LogType", SqlDbType.VarChar, 50);
                p[1].Value = recentActivity.LogType;
                p[2] = new SqlParameter("@TransactionType", SqlDbType.VarChar, 50);
                p[2].Value = recentActivity.TransactionType;
                p[3] = new SqlParameter("@RecordType", SqlDbType.VarChar, 50);
                p[3].Value = recentActivity.RecordType;
                p[4] = new SqlParameter("@Description", SqlDbType.VarChar, 150);
                p[4].Value = recentActivity.Description;
                p[5] = new SqlParameter("@ReferenceNo", SqlDbType.VarChar, 50);
                p[5].Value = recentActivity.ReferenceNo;
                p[6] = new SqlParameter("@Remarks", SqlDbType.VarChar, 150);
                p[6].Value = recentActivity.Remarks;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spInsertUserActivity", p);

            }
            catch (Exception ex)
            {

                throw;
            }


        }


        /// <summary>
        /// Get All user activity details where the logged username 
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public static List<emsRecentActivityU> GetUserActivity(string Username)
        {
            SqlParameter[] p = new SqlParameter[1];

            List<emsRecentActivityU> lstRecentActivity = new List<emsRecentActivityU>();

            if (string.IsNullOrEmpty(Username)) return null;

            try
            {
                p[0] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[0].Value = Username;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spGetAllDetailsRecentActivity", p))
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstRecentActivity.Add(new emsRecentActivityU { ID = long.Parse(dr["ID"].ToString()), TrnUser = dr["TrnUser"].ToString(), TransactionType = short.Parse(dr["TransactionType"].ToString()), RecordType = short.Parse(dr["RecordType"].ToString()), LogType = short.Parse(dr["LogType"].ToString()), Description = dr["Description"].ToString(), ReferenceNo = dr["ReferenceNo"].ToString(), Remarks = dr["Remarks"].ToString() });
                        }
                    }

                    return lstRecentActivity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


   
        public UserU GetUserByUsername(string Username)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = Username;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spSelectUserFromUsername", p))
                {
                    UserU user = new UserU();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.Username = dr["Username"].ToString();
                            user.Password = dr["Password"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.Tel = dr["Tel"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());
                            user.NIC = dr["NIC"].ToString();
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetUserRolesByUsername(string Username)
        {
            p = new SqlParameter[1];
            List<string> lstRoles = new List<string>();

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = Username;
                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spSelectRolesFromUsername", p))
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lstRoles.Add(dr["RoleName"].ToString());
                        }

                    }
                    string[] arrRoles = lstRoles.ToArray();
                    return arrRoles;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<UserU> GetUserActiveAll()
        {
            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spUserSelectActiveAll"))
            {
                List<UserU> userList = new List<UserU>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userList.Add(new UserU
                        {
                            Active = bool.Parse(dr["Active"].ToString()),
                            Email = dr["Email"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            Id = int.Parse(dr["Id"].ToString()),
                            LastName = dr["LastName"].ToString(),
                            Password = dr["Password"].ToString(),
                            Tel = dr["Tel"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                            Username = dr["Username"].ToString(),
                            NIC = dr["NIC"].ToString()
                    });
                    }
                }
                return userList;
            }
        }

        public string createUser(UserU emsUser)
        {
            
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[10];

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = emsUser.Username;
                p[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                p[1].Value = emsUser.Password;
                p[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                p[2].Value = emsUser.FirstName;
                p[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                p[3].Value = emsUser.LastName;
                p[4] = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                p[4].Value = emsUser.Email;
                p[5] = new SqlParameter("@Tel", SqlDbType.VarChar, 50);
                p[5].Value = emsUser.Tel;
                p[6] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[6].Value = "Anuradha";
                p[7] = new SqlParameter("@Active", SqlDbType.Bit, 0);
                p[7].Value = emsUser.Active;                
                p[8] = new SqlParameter("@NIC", SqlDbType.VarChar, 50);
                p[8].Value = emsUser.NIC;               
                p[9] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[9].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                    "spUserCrate", p);               

                return p[9].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string editUser(int ID, UserU emsUser)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[9];

            try
            {
                p[0] = new SqlParameter("@ID", SqlDbType.Int, 0);
                p[0].Value = ID;
                p[1] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                p[1].Value = emsUser.FirstName;
                p[2] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                p[2].Value = emsUser.LastName;
                p[3] = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                p[3].Value = emsUser.Email;
                p[4] = new SqlParameter("@Tel", SqlDbType.VarChar, 50);
                p[4].Value = emsUser.Tel;
                p[5] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[5].Value = idendity.User.Username;
                p[6] = new SqlParameter("@Active", SqlDbType.Bit, 0);
                p[6].Value = emsUser.Active;
                p[7] = new SqlParameter("@NIC", SqlDbType.VarChar, 50);
                p[7].Value = emsUser.NIC;
                p[8] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                    "spUserEdit", p);
                return p[8].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string changeUserPassword(int ID, UserU emsUser)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
                p[0].Value = emsUser.Password;
                p[1] = new SqlParameter("@ID", SqlDbType.Int, 0);
                p[1].Value = ID;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                    "spUserChangePassword", p);
                return p[8].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public List<emsUserRoleTUview> getUserRoleByUserID(int UserID)
        {
            p=new SqlParameter[1];

            p[0]= new SqlParameter("@UserID",SqlDbType.Int,0);
            p[0].Value = UserID;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                CommandType.StoredProcedure, "spUserSelectRolesByUserId", p))
            {
                List<emsUserRoleTUview> userRoleList = new List<emsUserRoleTUview>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userRoleList.Add(new emsUserRoleTUview
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            RoleID = dr["RoleID"].ToString(),
                            RoleName = dr["RoleName"].ToString(),
                            Username = dr["Username"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString(),
                        });
                    }
                }
                return userRoleList;
            }
        } 

        public UserU GetUserById(int ID)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@ID", SqlDbType.VarChar, 0);
                p[0].Value = ID;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),CommandType.StoredProcedure, "spUserSelectActiveByID", p))
                {

                    UserU user = new UserU();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.Username = dr["Username"].ToString();
                            user.Password = dr["Password"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.Tel = dr["Tel"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());
                            user.NIC = dr["NIC"].ToString();
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserU GetUserByUserName(string UserName)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = UserName;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUserSelectActiveByUserName", p))
                {

                    UserU user = new UserU();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.Username = dr["Username"].ToString();
                            user.Password = dr["Password"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.Tel = dr["Tel"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());
                            user.NIC = dr["NIC"].ToString();                            
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<emsUserRoleU> getRolesNotAssignedForUser( string username)
        {
            p = new SqlParameter[1];

            p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 0);
            p[0].Value = username;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(),
                 CommandType.StoredProcedure, "spRolesSelectAllRolesNotAssignedForUser", p))
            {

                List<emsUserRoleU> userRoles = new List<emsUserRoleU>();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userRoles.Add(new emsUserRoleU
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            RoleID = dr["RoleID"].ToString(),
                            RoleName = dr["RoleName"].ToString(),
                            TrnDate = DateTime.Parse(dr["TrnDate"].ToString()),
                            TrnUser = dr["TrnUser"].ToString()
                        });
                    }
                }
                return userRoles;
            }
        }

        public string addUserRoleToUser(string RoleID,string username)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@RoleID", SqlDbType.VarChar, 50);
                p[0].Value = RoleID;
                p[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[1].Value = username;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                    "spUserAddRole", p);
                return p[3].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string deleteUserRoleToUser(string RoleID, string username)
        {
            var idendity = (HttpContext.Current.User as clsPrincipal).Identity as clsIdentity;
            p = new SqlParameter[4];

            try
            {
                p[0] = new SqlParameter("@RoleID", SqlDbType.VarChar, 50);
                p[0].Value = RoleID;
                p[1] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[1].Value = username;
                p[2] = new SqlParameter("@TrnUser", SqlDbType.VarChar, 50);
                p[2].Value = idendity.User.Username;
                p[3] = new SqlParameter("@msg", SqlDbType.VarChar, 50);
                p[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure,
                    "spUserDeleteRole", p);
                return p[3].Value.ToString();
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public ViewModalUserDetails GetUserDetailsByUserName(string UserName)
        {
            p = new SqlParameter[1];

            try
            {
                p[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
                p[0].Value = UserName;

                using (SqlDataReader dr = SqlHelper.ExecuteReader(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spUserSelectActiveByUserName", p))
                {

                    ViewModalUserDetails user = new ViewModalUserDetails();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.Id = int.Parse(dr["ID"].ToString());
                            user.Username = dr["Username"].ToString();
                            user.FirstName = dr["FirstName"].ToString();
                            user.LastName = dr["LastName"].ToString();
                            user.Email = dr["Email"].ToString();
                            user.Tel = dr["Tel"].ToString();
                            user.TrnDate = DateTime.Parse(dr["TrnDate"].ToString());
                            user.TrnUser = dr["TrnUser"].ToString();
                            user.Active = bool.Parse(dr["Active"].ToString());                            
                            user.NIC = dr["NIC"].ToString();                            
                        }
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UplinerValidation(string MemberID, string RefID)
        {
            p = new SqlParameter[3];

            p[0] = new SqlParameter("@MemberID", SqlDbType.VarChar, 50);
            p[0].Value = MemberID;
            p[1] = new SqlParameter("@RefID", SqlDbType.VarChar, 50);
            p[1].Value = RefID;
            p[2] = new SqlParameter("@msg", SqlDbType.Bit);
            p[2].Direction = ParameterDirection.Output;

            try
            {
                SqlHelper.ExecuteNonQuery(clsConnectionString.getConnectionString(), CommandType.StoredProcedure, "spValidateUpliner", p);
                bool msg = bool.Parse(p[2].Value.ToString());

                return msg;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}