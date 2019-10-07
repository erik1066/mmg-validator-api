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
                    var List = ValidateDataType(FieldValue, segment, Item, ApiUri);
                    ErrorList.AddRange(List);

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

                    var field = segment.Fields(5);

                    if (field.HasRepetitions)
                    {
                        foreach (var repeatedField in field.Repetitions())
                        {
                            var errorsForRelatedElement = ValidateDataType(FieldValue, segment, Item, ApiUri);
                            ErrorList.AddRange(errorsForRelatedElement);
                        }
                    }

                    //Validate codes using API
                }
                catch (Exception ex)
                {
                    ErrorList.Add("Error:TBD: " + FieldValue);
                }


            }




            return ErrorList;


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
