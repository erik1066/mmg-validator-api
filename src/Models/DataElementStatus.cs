using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    /// <summary>
    /// Represents the status of a data element
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataElementStatus
    {
        /// <summary>
        /// For internal use only at this time
        /// </summary>
        Development,

        /// <summary>
        /// OMB proposed
        /// </summary>
        Proposed,

        /// <summary>
        /// Final
        /// </summary>
        Final
    }
}