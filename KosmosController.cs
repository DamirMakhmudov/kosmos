using Microsoft.AspNetCore.Mvc;
using Tdms.Api;
using Tdms.Log;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Timers;
using System.Net.Http;
using System.Collections.Generic;

namespace kosmos
{
    public class KosmosController : ControllerBase
    {
        //[TdmsAuthorize] // for avoid authorization
        public TDMSApplication ThisApplication;
        public ILogger Logger { get; set; }
        public TDMSObject thisobject;
        public JsonObject jsonobjectO;
        string response = "";

        public KosmosController(TDMSApplication application)
        {
            ThisApplication = application;
            Logger = Tdms.Log.LogManager.GetLogger("Kosmos");
        }

        public string CreateUserd(string text)
        {
            Logger.Info(text);
            Logger.Info(jsonobjectO.mode);
            return "true";
        }
        /* User create */
        [Route("api/user"), HttpPost]
        public object UserMethod([FromBody] JsonObject jsonobject)
        {
            try
            {
                jsonobjectO = jsonobject;
                string TheCommandString = "CreateUserd";

                Type thisType = this.GetType();
                MethodInfo theMethod = thisType.GetMethod(TheCommandString);
                object[] ar = new object[1];
                ar[0] = "Damir";
                theMethod.Invoke(this, ar);

                //string methodName = "CreateUserd";
                //MethodInfo mi = this.GetType().GetMethod(methodName);
                //mi.Invoke(this, null);

                switch (jsonobject.mode)
                {
                    case "create":
                        //TDMSUser user = ThisApplication.Users.Create();
                        //jUser juser = jsonobject.User;
                        //if (juser.login == null || juser.login == "")
                        //{
                        //    response = "Invalid 'login' property";
                        //    return response;
                        //};
                        //user.Login = juser.login;
                        //user.ChangePassword(juser.password);
                        //user.FirstName = juser.firstname;
                        //user.LastName = juser.lastname;
                        //user.MiddleName = juser.patronymic;
                        //user.Description = $"{juser.lastname} {juser.firstname}";
                        //ThisApplication.SaveChanges();
                        //response = user.SysName ;
                        break;
                    case "update":
                        response = "";
                        break;
                    case "delete":
                        response = "";
                        break;

                    default:
                        response = "Invalid 'mode' value. Should be 'create', 'update', 'delete' etc.";
                        break;
                };
                ThisApplication.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                response = ex.Message + "\n" + ex.StackTrace;
                Logger.Info($"Flow 2. GPPgetClaimRegistry: finished with: {response}");
                return response;
            }
        }
        public string CreateUser()
        {
            Logger.Info("ddd");
            return "true";
        }
    }
}
