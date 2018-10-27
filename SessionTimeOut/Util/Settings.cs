using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionTimeOut.Util
{
    public class Settings
    {
        public static int Session_TimeOut
        {
            get
            {
                string SessionTimeOut = System.Configuration.ConfigurationManager.AppSettings["Session.TimeOut"]; // Se ingresan en minutos
                return Convert.ToInt32(SessionTimeOut) * 60 * 1000; // Se retornan en milisegundos
            }
        }

        public static int Session_TimeWait
        {
            get
            {
                string SessionTimeWait = System.Configuration.ConfigurationManager.AppSettings["Session.TimeWait"];
                return Convert.ToInt32(SessionTimeWait);
            }
        }
    }
}