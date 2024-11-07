using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDiary.Services
{ 
    public static class AuthenticationService
    {
        private static string sessionToken;

        public static void SaveSessionToken(string token)
        {
            sessionToken = token;
        }

        public static string GetSessionToken()
        {
            return sessionToken;
        }

        public static bool IsAuthenticated => !string.IsNullOrEmpty(sessionToken);
    }

}
