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
        [HttpGet]
        public string Index()
        {
            return $"Server is UP!! Sonaar Service version 0.0.2";
        }
    }
}

