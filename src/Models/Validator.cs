using Cdc.Mmg.Validator.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HL7.Dotnetcore;
using Cdc.mmg.validator.WebApi.Interfaces;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using AutoMapper.Configuration;

namespace Cdc.mmg.validator.WebApi.Models
{
    public class Validator : IValidator
    {
         
        public List<string> ValidateOBX(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri) {

            List<string> ErrorList = new List<string>();
            var FieldValue = string.Empty;

            foreach (var element in mmgElementList
                        .Where(e => e.hL7SegmentType.HasValue && e.hL7SegmentType.Value == SegmentType.OBX) // todo: Refactor the two AND statements
                        .Where(e => e.priority == Priority.Required))
            {
                // check to see if a corresponding segment exists
                var elementId = element.hL7Identifier;
                var segment = segList.Where(s => s.Fields(3).Components(1).Value.Equals(elementId)).FirstOrDefault();
                if (segment == null)
                {
                    ErrorList.Add($"Error:A required element ({elementId}) wasn't found in the HL7 message");
                }
            }

            // selecting a specific  HL7 segment
            foreach (var segment in segList)
            {
                try
                {
                    FieldValue = segment.Fields(3).Components(1).Value; //   Api call
                }
                catch (Exception ex)
                {
                    ErrorList.Add("Error:MMG section does not exist for HL7 Identifier: " + FieldValue);
                }

                try
                {
                    var Item = mmgElementList.Where(x => x.hL7Identifier == FieldValue).FirstOrDefault();
                    if (Item == null)
                    {
                        ErrorList.Add("test: " + FieldValue);
                        continue;
                    }

                    // validate data type 
                    
                  
                        var errorsForRelatedElement1 = ValidateDataType(FieldValue, segment, Item, ApiUri);
                        ErrorList.AddRange(errorsForRelatedElement1);
                  
                    // validate related data elements (such as OBX-6)

                    foreach (var relatedElement in mmgElementList.Where(e => e.relatedDataElementId == Item.id))
                    {
                        var relatedElementPosition = relatedElement.hL7SegmentFieldPosition;

                        if (relatedElementPosition <= 5 || relatedElementPosition > 25)
                        {
                            // this should never happen for OBX, so add an error to the error list
                            continue;
                        }

                        var relatedData = segment.Fields(relatedElementPosition).Components(1).Value; // TODO: Test to see what happens if you go out of bounds

                        if (relatedElement.priority == Priority.Required && string.IsNullOrWhiteSpace(relatedData))
                        {
                            // add an error for missing required data
                        }
                        else
                        {
                            var errorsForRelatedElement = ValidateDataType($"OBX-{relatedElementPosition} value for {FieldValue}", segment, relatedElement, ApiUri);
                            ErrorList.AddRange(errorsForRelatedElement);
                        }
                    }

                    /* This logic can be used to implement checking for repeating data elements, but the other code in this file may need
                     some refactoring to get it integrated */

                   

                    //Validate codes using API
                }
                catch (Exception ex)
                {
                    ErrorList.Add("Error:TBD: " + FieldValue);
                }


            }




            return ErrorList;


        }
        public List<string> ValidateNK1(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            //NK1|1||MTH^Mother^HL70063|^^^22^71301^USA^^^22079||||||||||S^Single^HL70002||19940113||||||||||||2186-5^Not Hispanic or Latino^CDCREC|||||||2054-5^Black or African American^CDCREC
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidateNTE(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            //NTE|1|L|Response to therapy is indicated by a > or =4-fold decrease in titer between pre and post treatment samples. However, a significant decrease in RPR titers may not occur for months to years following treatment, some patients may show persistent, low-level titers (e.g. serofast) despite adequate therapy.
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidatePID(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            //PID|1||CONSYPH_TC01^^^SendAppName&2.16.840.1.114222.nnnn&ISO||~^^^^^^S||20130801|F||2054-5^Black or African American^CDCREC|^^^22^71301^^^^22079|||||||||||2186-5^Not Hispanic or Latino^CDCREC
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidateMSH(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            //MSH|^~\&|SendAppName^2.16.840.1.114222.nnnn^ISO|Sending-Facility^2.16.840.1.114222.nnnn^ISO|PHINCDS^2.16.840.1.114222.4.3.2.10^ISO|PHIN^2.16.840.1.114222^ISO|20141225120030.1234-0500||ORU^R01^ORU_R01|CONSYPH_V1_0_TM_TC01|T|2.5.1|||||||||NOTF_ORU_v3.0^PHINProfileID^2.16.840.1.114222.4.10.3^ISO~Generic_MMG_V2.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO~CongenitalSyphilis_MMG_V1.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidateSPM(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidateOBR(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            //OBR|1||CONSYPH_TC01^SendAppName^2.16.840.1.114222.nnnn^ISO|68991-9^Epidemiologic Information^LN|||20130825170100|||||||||||||||20130825170100|||F||||||10316^Syphilis, Congenital^NND
            List<string> List = new List<string>();
            return List;

        }
        public List<string> ValidatePV1(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri)
        {
            List<string> List = new List<string>();
            return List;

        }

        private List<string> ValidateDataType(string FieldValue, Segment segment, DataElement Item, string ApiUri)
        {
            List<string> ErrorList = new List<string>();
            string DataType = segment.Fields(2).Value.ToString();
            if (DataType != Item.hL7DataType)
            {
                ErrorList.Add("Error: Invalid Data Type. HL7 Identifier: " + FieldValue);
            }

            switch (DataType)
            {
                case "NM":  //Validate numeric values
                    int numeric;
                    if (!int.TryParse(segment.Fields(5).Value, out numeric))
                    {
                        ErrorList.Add("Error: Invalid numeric value. HL7 Identifier: " + FieldValue);
                    }
                    break;
                case "ST":  //Validate 

                    var field = segment.Fields(5);

                    if (field.HasRepetitions)
                    {
                        foreach (var repeatedField in field.Repetitions())
                        {
                            if (string.IsNullOrWhiteSpace(repeatedField.Value))
                            {
                                ErrorList.Add("Error: Invalid string value. HL7 Identifier: " + repeatedField.Value);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(FieldValue))
                        {
                            ErrorList.Add("Error: Invalid string value. HL7 Identifier: " + FieldValue);
                        }

                    }
                 
                    break;
                case "CWE":  //Validate codes using API
                    var ValueSetCode = Item.valueSetCode;
                    var ConceptCode = segment.Fields(5).Components(1).Value;
                    var RelatedIndicator = segment.Fields(4).Components(1).Value;


                    if (string.IsNullOrEmpty(RelatedIndicator))
                    {

                        if (string.IsNullOrEmpty(ValueSetCode))
                        {
                            // this is probably an error with the MMG itself and not the HL7 message, potentially
                            ErrorList.Add("Error: Value set code is required: HL7 Identifier: " + FieldValue);
                        }
                        else
                        {

                            //Call 
                            try
                            {
                                using (var _client = new HttpClient())
                                {
                                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");

                                    var uri = ApiUri + ConceptCode + "&valuesetcode=" + ValueSetCode;
                                    var response1 = _client.GetAsync(uri);

                                    response1.Wait();
                                    var result1 = response1.Result;
                                    if (!result1.IsSuccessStatusCode)
                                    {
                                        ErrorList.Add("Error: Value set Not found. HL7 Identifier: " + FieldValue + "/ConceptCode:" + ConceptCode + "/ValueSetCode:" + ValueSetCode);
                                    }
                                    else
                                    {
                                        string Json = result1.Content.ReadAsStringAsync().Result;
                                        var Content = JArray.Parse(Json);
                                        //OBX-5.2 and OBX-5.3 values (the “Yes” and “HL70136” values from the HL7 message) against the “cdcPreferredDesignation” and “hl70396Identifier” properties in the API’s Json response.
                                        //OBX|38|CWE|75204-8^Prenatal Visit Indicator^LN||Y^Yes^HL70136||||||F

                                        var cdcPreferredDesignation = Content[0]["cdcPreferredDesignation"].ToString();
                                        var hl70396Identifier = Content[0]["hl70396Identifier"].ToString();

                                        var OBX_5_2 = segment.Fields(5).Components(2).Value;
                                        var OBX_5_3 = segment.Fields(5).Components(3).Value;

                                        if (OBX_5_2.ToLower() != cdcPreferredDesignation.ToLower())
                                        {
                                            ErrorList.Add("Error: Invalid cdcPreferredDesignation . HL7 Identifier: " + FieldValue + "/ConceptCode:" + ConceptCode + "/ValueSetCode:" + ValueSetCode);
                                        }
                                        if (OBX_5_3.ToLower() != hl70396Identifier.ToLower())
                                        {
                                            ErrorList.Add("Error:Invalid  hl70396Identifier. HL7 Identifier: " + FieldValue + "/ConceptCode:" + ConceptCode + "/ValueSetCode:" + ValueSetCode);
                                        }

                                    }

                                }
                            }
                            catch (Exception ex)
                            {

                                ErrorList.Add("Error: Value set Api call. HL7 Identifier: " + FieldValue + "/ConceptCode:" + ConceptCode + "/ValueSetCode:" + ValueSetCode);
                            }
                        }
                    }
                    else
                    {
                        //handle related
                        var RelatedValue = segment.Fields(5).Components(1).Value;

                        var ConceptItem = Item.valueSet.concepts.Where(x => x.code == RelatedValue).Single();

                        if (ConceptItem.name.ToLower() != segment.Fields(5).Components(2).Value.ToLower())
                        {

                            ErrorList.Add("Error: Invalid value set name. Concept code: " + RelatedValue);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Other");
                    break;
            }

            // Check for:
            // - An OBX-based element in a SINGLE element block shall only appear once in the HL& message. Check to make sure only 1 segment exists for these elements.
            // - An OBX-based element that is REQUIRED shall have a corresponding OBX segment in the HL7 message

            return ErrorList;
        }
    }
}
