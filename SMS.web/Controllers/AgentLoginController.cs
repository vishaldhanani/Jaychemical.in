using Qtm.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using WS_C_FUN;
public class AgentLoginController : ApiController
{
    // GET api/<controller>
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<controller>
    public void Post([FromBody]string value)
    {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }

    [ActionName("CheckLogin")]
    public string CheckLogin(string Username, string Password)
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
}
