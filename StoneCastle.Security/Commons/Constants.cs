using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneCastle.Security.Commons
{
    public static class Constants
    {
        public static string ENTITY_FRAMEWORK_CONNECTION_STRING = "SCDbContext";
        public static string COOKIE_PATH = "";
        public static string CONFIGURATION_AUDIENCE_ID = ConfigurationManager.AppSettings["as:AudienceId"];
        public static string CONFIGURATION_AUDIENCE_SECRET = ConfigurationManager.AppSettings["as:AudienceSecret"];
        public static string CONFIGURATION_ISSUER = ConfigurationManager.AppSettings["as:issuer"];
        public static string CONFIGURATION_TOKEN_ENDPOINT = ConfigurationManager.AppSettings["as:TokenEndPoint"];
        public static int ACCESSTOKEN_EXPIRE_TIMESPAN_MINUTES = 0;
        public static int TOKEN_LIFESPAN_MINUTES = 60;
        public static int PASSWORD_MIN_LENGHT = 8;
    }
}
