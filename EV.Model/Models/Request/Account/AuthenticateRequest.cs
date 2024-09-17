using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Model.Models.Request.Account
{
    public class AuthenticateRequest
    {
       public string UserName { get; set; }
       public string Password { get; set; }
    }
}
