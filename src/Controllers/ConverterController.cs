using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HL7.Dotnetcore;
using Newtonsoft.Json.Linq;
using Cdc.mmg.validator.WebApi.Models;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace Cdc.mmg.validator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        [Produces("application/json")]
        [Consumes("text/plain")]
        [HttpPost("{id}")]
        public ActionResult<IEnumerable<string>> Post([FromBody] string HL7)
        {
            Message message = new Message(HL7);
            message.ParseMessage();

            return Ok();
        }
    }
}
