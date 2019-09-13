using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    /// <summary>
    /// Represents a "code" and "code name" pair from a public health vocabulary
    /// </summary>
   // [DebuggerDisplay("{Code} : {Name}")]
    public class ValueSetConcept
    {
       
        /// <summary>
        /// Gets/sets the concept code
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Gets/sets the concept name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets the label for this concept
        /// </summary>
        public string label
        {
            get
            {
                return code + " - " + preferredName;
            }
        }

        /// <summary>
        /// Gets/sets the preferred name of this concept, if any
        /// </summary>
        public string preferredName { get; set; }

        /// <summary>
        /// Gets/sets the code system OID
        /// </summary>
        public string codeSystemOID { get; set; }

        /// <summary>
        /// Gets/sets the HL70396 identifier for this concept, which is used to provide OBX-5.3 values for coded elements
        /// </summary>
        public string hl70396Identifier { get; set; }

        /// <summary>
        /// Gets/sets the code system version
        /// </summary>
        public string codeSystemVersion { get; set; }

        /// <summary>
        /// Gets/sets the code system code
        /// </summary>
        public string codeSystemCode { get; set; }

        /// <summary>
        /// Gets/sets the code system name
        /// </summary>
        public string codeSystemName { get; set; }

        /// <summary>
        /// Gets/sets the sequence that this concept should appear when being displayed in a list
        /// </summary>
        public int sequence { get; set; }
    }

}
