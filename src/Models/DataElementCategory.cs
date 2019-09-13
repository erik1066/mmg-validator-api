using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    /// <summary>
    /// Broad categories to which data elements can be grouped into
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataElementCategory
    {
        /// <summary>
        /// Demographics
        /// </summary>
        Demographics,

        /// <summary>
        /// Clinical, e.g. signs and symptoms, complications, past medical history, vital signs
        /// </summary>
        Clinical,

        /// <summary>
        /// Laboratory
        /// </summary>
        Laboratory,

        /// <summary>
        ///  Vaccine
        /// </summary>
        Vaccine,

        /// <summary>
        /// Treatment
        /// </summary>
        Treatment,

        /// <summary>
        /// Epidemiology
        /// </summary>
        Epidemiology
    }
}
