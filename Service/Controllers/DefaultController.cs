using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Sonaar.Controllers;
using Sonaar.Domain.DataContexts;
using Sonaar.Interface;

namespace Sonaar.Service.APi.Controllers
{
    [ApiController]
    [Route("/")]
	public class DefaultController : ControllerBase
    {
        public string Index()
        {
            return $"It Works on {Dns.GetHostName()} ! Sonaar Service version 0.0.1";
        }
    }
}

