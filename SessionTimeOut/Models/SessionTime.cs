using SessionTimeOut.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionTimeOut.Models
{
    public class SessionTime
    {
        private SessionTime() { }

        public SessionTime(bool inSession)
        {
            InSession = inSession ? inSession : (Current != null);
            SecondsWaitCloseSesssion = Settings.Session_TimeWait;
            TimeOut = InSession ? Settings.Session_TimeOut : 0;
            TimeOut = TimeOut - ((SecondsWaitCloseSesssion * 1000) + 3000);
        }

        public bool InSession { get; set; }
        public int TimeOut { get; set; }
        public int SecondsWaitCloseSesssion { get; set; }

        public static void CreateSession()
        {
            HttpContext.Current.Session["Session"] = new SessionTime(true);
            HttpContext.Current.Session.Timeout = Settings.Session_TimeOut;
        }

        public static SessionTime Current
        {
            get
            {
                SessionTime Current = HttpContext.Current.Session["Session"] as SessionTime;
                if (Current == null)
                {
                    return new SessionTime()
                    {
                        InSession = false,
                        SecondsWaitCloseSesssion = Settings.Session_TimeWait,
                        TimeOut = 0
                    };
                }
                else
                {
                    return new SessionTime()
                    {
                        InSession = Current.InSession,
                        SecondsWaitCloseSesssion = Settings.Session_TimeWait,
                        TimeOut = Settings.Session_TimeOut + 3000
                    };
                }
            }
        }

        public static void CloseSession()
        {
            HttpContext.Current.Session["Session"] = null;
            HttpContext.Current.Session.Abandon();
        }
    }
}