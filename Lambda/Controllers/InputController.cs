﻿using Lambda.Data;
using LambdaLogic; 
using Microsoft.AspNetCore.Mvc;

namespace Lambda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InputController : ControllerBase
    {
        [HttpGet(Name = "Lambda Input")]
        public IActionResult LambdaInput(string input)
        {
            var result = Logic.processInput(input);
            var resultString = result.ToString();
            return Ok(new InputData { lambdaInputString = resultString });
        }
    }
}