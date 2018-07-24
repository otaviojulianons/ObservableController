using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("/")]
    public class StatusController : Controller
    {

        [HttpGet]
        public Dictionary<string,string> CheckStatus()
        {
            return new Dictionary<string, string>() { { "status","OK" } };
        }


    }
}
