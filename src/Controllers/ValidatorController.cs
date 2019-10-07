using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HL7.Dotnetcore;
using Newtonsoft.Json.Linq;
using Cdc.Mmg.Validator.WebApi.Models;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Cdc.mmg.validator.WebApi.Models;
using Newtonsoft.Json;
using Cdc.mmg.validator.WebApi.Interfaces;

namespace Cdc.Mmg.Validator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        readonly IConfiguration _configuration;
        readonly IValidator _validator;
        public ValidatorController(IConfiguration configuration, IValidator validator)
        {
            _configuration = configuration;
            _validator = validator;
        }


        // POST: api/Validator/mmgId

        [Produces("application/json")]
        [Consumes("text/plain")]
        [HttpPost("{id}")]
        public ActionResult<IEnumerable<string>> Post([FromBody] string HL7)
        {
            string path = @"Content\mmg.json";
            FileStream fileStream;
            List<string> ErrorList = new List<string>();
            List<DataElement> mmgElementList = new List<DataElement>();
            if (System.IO.File.Exists(path))
            {
                fileStream = System.IO.File.OpenRead(path);
                using (StreamReader r = new StreamReader(path))
                {
                    string mmg = r.ReadToEnd();
                    JObject mmg_json = JObject.Parse(mmg);
                    // get to element block
                    var elements = mmg_json["blocks"].Children()["elements"].Children().ToArray();
                    // List<string> list = new List<string>();

                    foreach (var element in elements)
                    {
                        mmgElementList.Add(SetDataElement(element));
                    }
                }
                //Process HL7
                    Message message = new Message(HL7);
                    message.ParseMessage();

                    //Some Metadata
                    //string version = message.Version;
                    //string msgControlID = message.MessageControlID;
                    //string messageStructure = message.MessageStructure;
                   // List of Segments
                     
                   
                    // check to make sure each required data element has a matching segment

                    /* TODO: Also, we must validate the following non-OBX segment types:
                     * NTE
                     * NK1
                     * PID
                     * MSH
                     * SPM
                     * OBR
                     * PV1
                    */
                    ErrorList.AddRange(_validator.ValidateOBX(message.Segments("OBX"), mmgElementList, _configuration["Vocab_Api"]));

                    ErrorList.AddRange(_validator.ValidatePID(message.Segments("PID"), mmgElementList, _configuration["Vocab_Api"]));



                    //ErrorList.AddRange(_validator.ValidateNTE(message.Segments("NTE"), mmgElementList, _configuration["Vocab_Api"]));
                    //ErrorList.AddRange(_validator.ValidateNK1(message.Segments("NK1"), mmgElementList, _configuration["Vocab_Api"]));
                    //ErrorList.AddRange(_validator.ValidateMSH(message.Segments("MSH"), mmgElementList, _configuration["Vocab_Api"]));
                    //ErrorList.AddRange(_validator.ValidateSPM(message.Segments("SPM"), mmgElementList, _configuration["Vocab_Api"]));
                    //ErrorList.AddRange(_validator.ValidateOBR(message.Segments("OBR"), mmgElementList, _configuration["Vocab_Api"]));
                    //ErrorList.AddRange(_validator.ValidatePV1(message.Segments("PV1"), mmgElementList, _configuration["Vocab_Api"]));






            }

            if (ErrorList.Count()>0)
            {

                return Ok(ErrorList);
            }
            else {
                return Ok("HL7 message is valid!");
            }
            
            
            
        }
        
      
        private DataElement SetDataElement(  JToken element)
        {
            DataElement DataElement = new DataElement();

          //  using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(element.ToString())))
         //   {
                //  DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(DataElement));
                //  DataElement = (DataElement)deserializer.ReadObject(ms);
                


         //   }
          
              DataElement = JsonConvert.DeserializeObject<DataElement>(element.ToString());

            return DataElement;
        }

        //private ValueSet GetValueSet(JToken element)
        //{
        //    ValueSet ValueSet = new ValueSet();
        //    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(element.ToString())))
        //    {

        //        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(ValueSet));
        //        ValueSet = (ValueSet)deserializer.ReadObject(ms);


        //    }
        //    return ValueSet;
        //}

       

    }
}
