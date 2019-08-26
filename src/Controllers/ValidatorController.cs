using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using HL7.Dotnetcore;
using Newtonsoft.Json.Linq;
using Cdc.mmg.validator.WebApi.Models;
namespace Cdc.mmg.validator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
      
        
        // POST: api/Validator/mmgId

        [Produces("application/json")]
        [Consumes("text/plain")]
        [HttpPost("{id}")]
        public ActionResult<IEnumerable<string>> Post([FromBody] string HL7)
        {
            string path = @"Content\mmg.json";
            FileStream fileStream;
            if (System.IO.File.Exists(path))
            {
                  fileStream = System.IO.File.OpenRead(path);
                using (StreamReader r = new StreamReader(path))
                {
                    string mmg = r.ReadToEnd();
                    JObject mmg_json = JObject.Parse(mmg);
                    // get to element block
                    var elements = mmg_json["blocks"].Children()["elements"].Children().ToArray();
                    List<string> list = new List<string>();
                    List<DataElement> mmgElementList = new List<DataElement>();
                    foreach (var element in elements)
                    {
                        // list.Add(element.ToString());

                        DataElement DataElement = new DataElement();

                        DataElement.CodeSystem = element["codeSystem"].ToString();
                        DataElement.DataType = element["dataType"].ToString();
                        DataElement.HL7Cardinality = element["hL7Cardinality"].ToString();

                        DataElement.HL7DataType = element["hL7DataType"].ToString();
                        DataElement.HL7Identifier = element["hL7Identifier"].ToString();
                        //   DataElement.HL7LiteralFieldValues = element["HL7LiteralFieldValues"].ToString();

                        DataElement.HL7OBRParent =int.Parse( element["hL7OBRParent"].ToString());
                        DataElement.HL7RepeatingGroupElement = element["hL7RepeatingGroupElement"].ToString();
                        DataElement.HL7SegmentComponentPosition =int.Parse( element["hL7SegmentComponentPosition"].ToString());

                        DataElement.HL7SegmentFieldPosition =int.Parse( element["hL7SegmentFieldPosition"].ToString());
                        DataElement.HL7Usage = element["hL7Usage"].ToString();
                        DataElement.Id = Guid.Parse( element["id"].ToString());

                        /////
                        DataElement.Identifier = element["identifier"].ToString();
                        DataElement.Name = element["name"].ToString();
                      //  DataElement.ValueSet = element["hL7SegmentComponentPosition"].ToString());

                        DataElement.IsUnitOfMeasure = bool.Parse(element["isUnitOfMeasure"].ToString());
                        DataElement.Ordinal = int.Parse(element["ordinal"].ToString());
                       // DataElement.RelatedDataElementId = Guid.Parse(element["relatedDataElementId"].ToString());
                        DataElement.PhinVariableCodeSystem = element["phinVariableCodeSystem"].ToString();
                        DataElement.Repetitions = int.Parse(element["repetitions"].ToString());
                        DataElement.ValueSetName = element["valueSetName"].ToString();

                        //DataElement.HL7SegmentType = int.Parse(element["hL7SegmentFieldPosition"].ToString());
                        DataElement.ValueSetOID = element["valueSetOID"].ToString();
                        DataElement.ValueSetCode =  element["valueSetOID"].ToString();
                        DataElement.HL7DataType = element["hL7DataType"].ToString();
                        DataElement.HL7Usage =  element["hL7Usage"].ToString();
                        mmgElementList.Add(DataElement);




                    }

                    Message message = new Message(HL7);
                    message.ParseMessage();

                    //Some Metadata
                    //string version = message.Version;
                    //string msgControlID = message.MessageControlID;
                    //string messageStructure = message.MessageStructure;
                   // List of Segments
                    List<Segment> segList = message.Segments("OBX");
                    // selecting a specific segment 
                    foreach (var segment in segList)
                    {
                        var FieldValue = segment.Fields(3).Components(1).Value;
                         


                        try
                        {
                            var Item = mmgElementList.Where(x => x.HL7Identifier == FieldValue).ToList();

                        
                        }
                        catch (Exception ex) {


                        }
                       

                    }




                }
            }

            
            
            
            return Ok("Done");
        }

        // PUT: api/Validator/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        
    }
}
