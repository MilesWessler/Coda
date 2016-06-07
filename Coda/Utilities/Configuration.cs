using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace Coda.Utilities
{
    public class Configuration
    {
        private static readonly string AccessToken;
        private static readonly Dictionary<string, string> Config;

        //private static ISchedulerFactory schedFact;

        //// get a scheduler
        //private static IScheduler sched;

        static Configuration()
        {

            // Get a reference to the Config
            Config = ConfigManager.Instance.GetProperties();

            // Use OAuthTokenCredential to request an access token from PayPal
            AccessToken = new OAuthTokenCredential(Config).GetAccessToken();


            //var Config = GetConfig();
            //ClientId = Config["clientId"];
            //ClientSecret = Config["clientSecret"];
        }

        //public static void StartScheduler()
        //{
        //    schedFact = new StdSchedulerFactory();
        //    sched = schedFact.GetScheduler();
        //    sched.Start();
        //}

        //public static void ScheduleJob(IJobDetail job, ITrigger trigger)
        //{
        //    sched.ScheduleJob(job, trigger);
        //}

        //public static void DeleteJob(JobKey jobKey)
        //{
        //    sched.DeleteJob(jobKey);
        //}

        //// getting properties from the web.Config
        //public static Dictionary<string, string> GetConfig()
        //{
        //    return PayPal.Manager.ConfigManager.Instance.GetProperties();
        //}

        //private static string GetAccessToken()
        //{
        //    // getting accesstocken from paypal                
        //    string AccessToken = new OAuthTokenCredential
        //(ClientId, ClientSecret, GetConfig()).GetAccessToken();

        //    return AccessToken;
        //}

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(AccessToken) { Config = Config };
            return apiContext;
        }
    }
}