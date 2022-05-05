using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using Tdms.Api;
using Tdms.Log;

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

        [Route("api"), HttpPost]
        public object UserMethod([FromBody] JsonObject jsonobject)
        {
            try
            {
                jsonobjectO = jsonobject;
                if (jsonobjectO.mode == null || jsonobjectO.mode == "")
                {
                    response = "Invalid 'mode' value. Should be 'createuser', 'updateuser' etc.";
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, response);
                };
                string mode = jsonobjectO.mode.ToLower();
                mode = $"{mode.Substring(0, 1).ToUpper()}{mode.Substring(1, mode.Length - 1)}";

                Type thisType = this.GetType();
                MethodInfo method = thisType.GetMethod(mode);
                if (!thisType.GetMethods().Contains(method))
                {
                    response = $"'mode' '{mode}' not found";
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, response);
                };

                object res = method.Invoke(this, null);
                return res;
            }
            catch (Exception ex)
            {
                response = ex.Message + "\n" + ex.StackTrace;
                Logger.Info($"Error: {response}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, response);
            };
        }

        /**
         * CREATE NEW TDMS USER
         */
        public object Createuser()
        {
            try
            {
                jUser juser = jsonobjectO.User;
                if (juser == null)
                {
                    response = "Request must contain 'user' key";
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, response);
                };
                string[] fields = { "firstname", "lastname" };
                foreach (string field in fields)
                {
                    if (juser.GetType().GetProperty(field).GetValue(juser, null) == null)
                    {
                        response = $"User property '{field}' not found";
                        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, response);
                    }
                };

                string login = this.Translit($"{juser.lastname}{juser.firstname}");
                string templogin = login;

                //TDMSQuery qq = ThisApplication.CreateQuery();
                //qq.AddCondition(TDMSQueryConditionType.tdmQueryConditionObjectDef, "USER_DEF");
                //qO_ClaimRegistry.AddCondition(TDMSQueryConditionType.tdmQueryConditionAttribute, "= Null or =''", "A_Bool_Started");
                //Logger.Info(qq.Users.Count.ToString());

                TDMSUser user;
                for (int i = 1; i <= 10; i++)
                {
                    user = ThisApplication.Users.GetUserByLogin(templogin);
                    if (ThisApplication.Users.GetUserByLogin(templogin) != null)
                    {
                        templogin = login + i;
                    };
                };

                user = ThisApplication.Users.Create();
                user.Login = templogin;
                user.ChangePassword(juser.password);
                user.FirstName = juser.firstname;
                user.LastName = juser.lastname;
                user.MiddleName = juser.patronymic;
                user.Description = $"{juser.lastname} {juser.firstname}";
                user.Phone = juser.phone;
                user.Mail = juser.email;
                ThisApplication.SaveChanges();
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, user.SysName);
            }
            catch (Exception ex)
            {
                response = ex.Message + "\n" + ex.StackTrace;
                Logger.Info($"Error: {response}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, response);
            }
        }

        /**
         * UPDATE TDMS USERS'S ATTRIBUTES
         */
        public object Updateuser()
        {
            try
            {
                jUser juser = jsonobjectO.User;
                string sysname = juser.sysname;
                TDMSUser user = ThisApplication.Users[sysname];
                if (user != null)
                {
                    user.Login = this.Translit($"{juser.lastname}{juser.firstname}");
                    user.ChangePassword(juser.password);
                    user.FirstName = juser.firstname;
                    user.LastName = juser.lastname;
                    user.MiddleName = juser.patronymic;
                    user.Description = $"{juser.lastname} {juser.firstname}";
                    user.Phone = juser.phone;
                    user.Mail = juser.email;
                    ThisApplication.SaveChanges();

                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, "ok");
                }
                else
                {
                    response = $"TDMS User sysname = '{juser.sysname}' not found";
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, response);
                }
            }
            catch (Exception ex)
            {
                response = ex.Message + "\n" + ex.StackTrace;
                Logger.Info($"Error: {response}");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest, response);
            }
        }
        /**
         * AUXILIARY FUNCTIONS
         */
        public string Translit(string str)
        {
            str = str.Replace("б", "b");
            str = str.Replace("Б", "B");

            str = str.Replace("в", "v");
            str = str.Replace("В", "V");

            str = str.Replace("г", "h");
            str = str.Replace("Г", "H");

            str = str.Replace("ґ", "g");
            str = str.Replace("Ґ", "G");

            str = str.Replace("д", "d");
            str = str.Replace("Д", "D");

            str = str.Replace("є", "ye");
            str = str.Replace("Э", "Ye");

            str = str.Replace("ж", "zh");
            str = str.Replace("Ж", "Zh");

            str = str.Replace("з", "z");
            str = str.Replace("З", "Z");

            str = str.Replace("и", "y");
            str = str.Replace("И", "Y");

            str = str.Replace("ї", "yi");
            str = str.Replace("Ї", "YI");

            str = str.Replace("й", "j");
            str = str.Replace("Й", "J");

            str = str.Replace("к", "k");
            str = str.Replace("К", "K");

            str = str.Replace("л", "l");
            str = str.Replace("Л", "L");

            str = str.Replace("м", "m");
            str = str.Replace("М", "M");

            str = str.Replace("н", "n");
            str = str.Replace("Н", "N");

            str = str.Replace("п", "p");
            str = str.Replace("П", "P");

            str = str.Replace("р", "r");
            str = str.Replace("Р", "R");

            str = str.Replace("с", "s");
            str = str.Replace("С", "S");

            str = str.Replace("ч", "ch");
            str = str.Replace("Ч", "CH");

            str = str.Replace("ш", "sh");
            str = str.Replace("Щ", "SHH");

            str = str.Replace("ю", "yu");
            str = str.Replace("Ю", "YU");

            str = str.Replace("Я", "YA");
            str = str.Replace("я", "ya");

            str = str.Replace('ь', '"');
            str = str.Replace("Ь", "");

            str = str.Replace('т', 't');
            str = str.Replace("Т", "T");

            str = str.Replace('ц', 'c');
            str = str.Replace("Ц", "C");

            str = str.Replace('о', 'o');
            str = str.Replace("О", "O");

            str = str.Replace('е', 'e');
            str = str.Replace("Е", "E");

            str = str.Replace('а', 'a');
            str = str.Replace("А", "A");

            str = str.Replace('ф', 'f');
            str = str.Replace("Ф", "F");

            str = str.Replace('і', 'i');
            str = str.Replace("І", "I");

            str = str.Replace('У', 'U');
            str = str.Replace("у", "u");

            str = str.Replace('х', 'x');
            str = str.Replace("Х", "X");

            return str;
        }
    }
}
