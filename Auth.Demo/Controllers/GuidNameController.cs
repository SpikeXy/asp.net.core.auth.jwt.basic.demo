﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GuidNameController : ControllerBase
    {
        private readonly ICustomAuthenticationManager customAuthenticationManager;
        public GuidNameController(ICustomAuthenticationManager customAuthenticationManager)
        {
            this.customAuthenticationManager = customAuthenticationManager;
        }
       

        // GET: api/Name
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = customAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            if (token == null) 
                return Unauthorized();
            
            return Ok(token);
        }

    }
}
