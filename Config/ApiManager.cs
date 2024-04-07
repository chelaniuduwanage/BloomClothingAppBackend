using System.Data.SqlClient;
using static Trendy_BackEnd.Config.EnumCollection;

namespace Trendy_BackEnd.Config
{
    public class ApiManager
    {
        private readonly string Environment = BloomEnvironment.Development.ToString();

        private ApiManager() { }
        private static ApiManager instance = null;
        internal static ApiManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiManager();
                }
                return instance;
            }
        }

        internal SqlConnectionStringBuilder GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            if (Environment == BloomEnvironment.Development.ToString())
            {
                builder.DataSource = "SQL5106.site4now.net";
                builder.InitialCatalog = "db_aa6ddf_chelani2000";
                builder.UserID = "db_aa6ddf_chelani2000_admin";
                builder.Password = "Chela200053*";
            }
            else if (Environment == BloomEnvironment.UAT.ToString())
            {
                builder.DataSource = "";
                builder.InitialCatalog = "";
                builder.UserID = "";
                builder.Password = "";
            }
            else if (Environment == BloomEnvironment.Live.ToString())
            {
                builder.DataSource = "";
                builder.InitialCatalog = "";
                builder.UserID = "";
                builder.Password = "";
            }
            return builder;
        }

        internal string GetEnvironment()
        { return Environment; }
    }
}
