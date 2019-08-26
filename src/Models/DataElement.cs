using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
//using CDC.MMGAT.WebUI.Data;

namespace Cdc.mmg.validator.WebApi.Models
{
    /// <summary>
    /// Represents a single data element within a message mapping guide
    /// </summary>
    [DebuggerDisplay("{Identifier} : {Name}")]
    public sealed class DataElement //: IEntity, IVersionedEntity
    {
        private string _HL7Usage = string.Empty;

        /// <summary>
        /// Gets/sets the PHIN data element identifier. Examples: DEM197, NOT115
        /// </summary>
        [RegularExpression(@"^([\-\.a-zA-Z0-9:,'\(\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "Data element {0} field contains invalid characters")]
        public string Identifier { get; set; } = string.Empty;//

        /// <summary>
        /// Gets/sets the internal ID of the data element.
        /// </summary>
        public Guid Id { get; set; }//

        
        /// <summary>
        /// Gets/sets the short name for the data element, which is passed in the message.
        ///  Examples: "Local Subject ID", "Birth Date", "Country of Exposure"
        /// </summary>
        [Required]
        [RegularExpression(@"^([\-\.a-zA-Z0-9:,'’®\(\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "Data element {0} field contains invalid characters")]
        public string Name { get; set; } = string.Empty;//

      
        /// <summary>
        /// Gets/sets the data type for the variable response expected by the program area. Data
        ///  types are Coded, Numeric, Date or Date/time, and Text.
        /// </summary>
       // public DataType DataType { get; set; } = DataType.Text;//
        public string DataType { get; set; } //

        /// <summary>
        /// Gets/sets the PHIN VADS value set this data element has been assigned. 
        ///  This property should remain null for all data elements other than those
        ///  of type 'Coded'.
        /// </summary>
        public ValueSet ValueSet { get; set; }//

        

        /// <summary>
        /// Gets/sets whether this data element represents a unit of measure, such as 
        ///  age in days, weight in kilograms, height in inches, etc.
        /// </summary>
        public bool IsUnitOfMeasure { get; set; } = false;//

        /// <summary>
        /// Gets/sets the order this element appears in a block
        /// </summary>
        public int Ordinal { get; set; }//

         
        /// <summary>
        /// Gets/sets the Id of another data element that this data element is related to.
        ///  The intended use of this is for situations where two data elements might end
        ///  up in the same part of an HL7 2.5.1 message (or other messaging format if
        ///  HL7 2.5.1 is not used any longer). An example is one data element that fills
        ///  in OBX-5, while a separate data element fills in OBX-6 for the same segment.
        ///  The software has to know that these two data elements are related in order for
        ///  test message generation and sample segment generation to correctly place the 
        ///  together.
        /// </summary>
        public Guid? RelatedDataElementId { get; set; } = null;//

        /// <summary>
        /// Gets/sets the code system from which the PHIN data element identifier is drawn.
        ///  Example values: PHINQUESTION, LOINC, CDCPHINVS, SNOMED-CT
        /// </summary>
        // [RegularExpression(@"^([a-zA-Z0-9\-]+)$")]
        [RegularExpression(@"^([\-\.a-zA-Z0-9:,'\(\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "Data element {0} field contains invalid characters")]
        public string PhinVariableCodeSystem { get; set; } = string.Empty;//

        /// <summary>
        /// Gets/sets the Data Element Identifier to be sent in HL7 message. Examples: null, 
        ///  "PID-11.4", "11368-8", "OBX-6"
        /// </summary>
        [Required]
        [RegularExpression(@"^([\-\.a-zA-Z0-9:,_'\(\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "Data element {0} field contains invalid characters")]
        public string HL7Identifier { get; set; } = string.Empty;///

        /// <summary>
        /// Gets/sets the code system from which the data element identifier is drawn (e.g. 
        ///  PHINQUESTION, LOINC, CDCPHINVS, SNOMED-CT).  This reference is sent in the message 
        ///  for those observations that map as CE (Coded Element) or CWE (Coded With Exceptions) 
        ///  datatypes in the message. Example values: LN, N/A, PHINQUESTION
        /// </summary>
        [RegularExpression(@"^([\-\.a-zA-Z0-9:,'\(\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "Data element {0} field contains invalid characters")]
        public string CodeSystem { get; set; } = string.Empty;//

         

        /// <summary>
        /// Gets/sets the number of times this element is allowed to appear in a message. If set to 
        ///  1, then no repeating is allowed and only a single value is permitted for this data
        ///  element. Zero and negative values are disallowed. Set to Int32.MaxValue to allow for
        ///  unlimited repetitions.
        /// </summary>
        public int Repetitions { get; set; } = 1;//

        /// <summary>
        /// Gets/sets the name of the VADS value set. For example, "Yes No Unknown (YNU)". This
        ///  should be set to string.Empty if the data element does not have a 'coded' data type.
        /// </summary>
        [DefaultValue("")]
        public string ValueSetName { get; set; } = string.Empty;//

         
        /// <summary>
        /// Gets/sets the key-value pair of literal values. The 'key' is an integer and refers to the
        ///  field position in the segment to which the literal belongs. The value is a string and is
        ///  simply the literal value. This is used, for example, in Varicella (see data element 67453-1)
        ///  where there's a conformance statement like this: "CONFORMANCE STATEMENT: OBX-6 SHALL 
        ///  contain the literal value 'd^day^UCUM'"
        /// </summary>
        public IDictionary<int, string> HL7LiteralFieldValues { get; set; } = new Dictionary<int, string>();//

        /// <summary>
        /// Gets/sets the OBR segment that this 
        /// </summary>
        public int? HL7OBRParent { get; set; } = 1;//

        /// <summary>
        /// Gets/sets the type of HL7 segment to which this data element belongs
        /// </summary>
       // public HL7.SegmentType? HL7SegmentType { get; set; }

        /// <summary>
        /// Gets/sets the ordinal of the field within an HL7 segment where this data element's
        ///  data can be located. For most elements this value is automatically determined
        ///  by the software and can't be overriden. In some cases, however, the user may need
        ///  more control over where a value appears in a given segment. If for instance the
        ///  data element needs to be placed into OBX-8, then this value should read 8 and the
        ///  related data element ID should point to the ID of the data element whose values
        ///  should go into OBX-5.
        /// </summary>
        public int HL7SegmentFieldPosition { get; set; } = -1;//

        /// <summary>
        /// Gets/sets the ordinal of the component within an HL7 field where this data element's
        ///  data can be located.
        /// </summary>
        public int? HL7SegmentComponentPosition { get; set; } = -1;//

         

        /// <summary>
        /// Gets/sets the OID associated with the VADS value set. should be set to string.Empty
        ///  if the data element does not have a 'coded' data type.
        /// </summary>
        public string ValueSetOID { get; set; } = string.Empty;//

        /// <summary>
        /// Gets/sets the ID of the VADS value set. For example, "PHVS_YesNoUnknown_CDC". This
        ///  should be set to string.Empty if the data element does not have a 'Coded' data type.
        /// </summary>
        public string ValueSetCode { get; set; } = string.Empty;//

         
        /// <summary>
        /// Gets/sets the HL7 data type used by PHIN to express the variable. Examples of 
        ///  data types expected are CWE, SN, TS, DT, ST, TX, XPN, XON, or XAD, depending 
        ///  on the type of data being passed. The specific HL7 datatype allowed in the field 
        ///  is consistent with the HL7 2.5.1 Standard for this message.
        /// </summary>
        public string HL7DataType { get; set; } = string.Empty;//

        /// <summary>
        /// Indicates if the field is required, optional, or conditional in a segment. The 
        ///  only values that appear in the Message Mapping are: R – Required.  Must always 
        ///  be populated. RE - Required but may be Empty.  This variable indicates that the 
        ///  message receiver must be prepared to process the variable, but it may be absent 
        ///  from a particular message instance. O – Optional. May optionally be populated.     
        /// </summary>
        public string HL7Usage //
        {
            get 
            {
                return _HL7Usage;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Equals("R") || value.Equals("RE") || value.Equals("O") || value.Equals("C(R/RE)") || value.Equals("R if the NTE segment is used") || value.Equals("R if the SPM segment is used"))
                {
                    _HL7Usage = value;
                }
                else 
                {
                    throw new ArgumentOutOfRangeException(nameof(HL7Usage));
                }
            }
        }

        /// <summary>
        /// Gets/sets the HL7 cardinality. This identifies the minimum and maximum number of 
        ///  repetitions for a particular field. When a field repeats, the values are sent in 
        ///  the same field with the instances separated by the tilde (~). Examples:  [1..1] 
        ///  means that the field is required and will not repeat. [0..1] means that the field 
        ///  is optional and will not repeat if it is present.  [0..*] means the field is
        ///  optional and may repeat an unlimited number of times. Cardinality is a the field
        ///  level and does not indicate whether the data element is part of a repeating group.
        ///  Examples: [0..1], [1..1], [0..*], [2..2]
        /// </summary>
        public string HL7Cardinality { get; set; } = string.Empty;//

        
        /// <summary>
        /// Gets/sets whether this data element is part of a repeating group. The PRIMARY/PARENT
        ///  observation is marked to serve as the anchor for the repeating group, and must be present
        ///  for the group to process correctly. The CHILD notation denotes that the variable is a 
        ///  child observation in the repeating group and must have the same OBX-4 Observation Sub-id 
        ///  value as the PRIMARY/PARENT observation to be considered in the same group.  YES indicates 
        ///  that this variable is considered to be part of a repeating group, but the PARENT/CHILD 
        ///  relationship does not apply to the elements in the repeating group.  NO indicates that this 
        ///  variable is not considered part of a repeating group and will not be processed as such.
        /// </summary>
        public string HL7RepeatingGroupElement { get; set; } = string.Empty;//

    }
}
