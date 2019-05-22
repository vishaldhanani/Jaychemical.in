using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Qtm.Lib;
using System.Web;
using WS_C_FUN;
public class LoginController : ApiController
{
    WA_JAYCHEM_DBDataContext objWA = new WA_JAYCHEM_DBDataContext();
    // GET api/<controller>    
    // GET api/<controller>/    
    public string Get(string Username, string Password)
    {
        string Success = "Success";
        string Fail = "Fail";
        Agent obj = Agent.LoginCredentials(Username, Password);
        if (obj != null)
        {
            SessionManager.AddToUserSession(HttpContext.Current, obj.Code, obj.AgentName);
            return Success;
        }
        else
        {
            return Fail;
        }
    }

    // GET api/<controller>
    public void Post(string Username, string Password)
    {
        objWA.SP_WA_LoginCredentials(Username, Password);
    }


    //[ActionName("CheckLogin")]
    // GET api/<controller>/Agent1,Agent1
    [System.Web.Http.AcceptVerbs("GET", "POST")]
    [System.Web.Http.HttpGet]
    public string PostCheckLogin(string Username, string Password)
    {
        //objWA.SP_WA_LoginCredentials(Username, Password);
        string Success = "Success";
        string Fail = "Fail";
        Agent obj = Agent.LoginCredentials(Username, Password);
        if (obj != null)
        {
           
            SessionManager.AddToUserSession(HttpContext.Current, obj.Code, obj.AgentName);
            return Success;
        }
        else
        {
            return Fail;
        }
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}
