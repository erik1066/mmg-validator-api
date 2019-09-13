using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    public class mmgElement
    {
        public mmgElement() { }
        public string businessRules { get; set; }
        public int hL7SegmentComponentPosition { get; set; }
        //public values {get; set;}": [],---------------------
        public Guid guideId { get; set; }
        public string phinVariableCodeSystem { get; set; }
        public string description { get; set; }
        public string hL7SegmentFieldPosition { get; set; }
        public int repetitions { get; set; } 
//public hL7Cardinality {get; set;} "[0..1]",----
public string hL7DataType { get; set; }
        //public defaultValues": [
        //           {
        //             "originalText": "",
        //            "repeatingGroupId": 1,
        //            "label": "",
        //           "value": "20140226"
        //        }
        //     ],
        public string valueSetOID { get; set; }
        public string valueSetCode { get; set; }
        public int internalVersion { get; set; }
        public Guid id { get; set; }
        public string state { get; set; }
        public string hL7MessageContext { get; set; }
        public string guideName { get; set; }
        public bool isUnitOfMeasure { get; set; }
        public string hL7Usage { get; set; } //": "O",
        public string identifier { get; set; }
        public string comments { get; set; }
        public string codeSystem { get; set; }
        public int hL7OBRParent { get; set; }
        public string publishVersion { get; set; }//": "",
        public string dataType { get; set; }//": "Date",
        public string hL7Identifier { get; set; }
        //public hL7LiteralFieldValues {get; set;}": {},
        public string hL7RepeatingGroupElement { get; set; } //": "NO",
        public int priority { get; set; }//": "O",
        public string hL7ImplementationNotes { get; set; }
        public DateTime lastUpdatedDate { get; set; }//": "0001-01-01T00:00:00",

        public string valueSetName { get; set; }
        public string name { get; set; }

        public string hL7SegmentType { get; set; }
        public string valueSetURL { get; set; }
        public string hL7SampleSegment { get; set; }
        public int ordinal { get; set; }

    }
}
