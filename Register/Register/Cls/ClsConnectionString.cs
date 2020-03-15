using System.Configuration;


/// <summary>
/// Summary description for ClsConnectionstring
/// </summary>
/// 


namespace Register.Connection
{
    public class clsConnectionString
    {
        public clsConnectionString()
        {

        }
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        }
    }
}