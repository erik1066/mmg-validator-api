using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    /// <summary>
    /// Represents the data type of a data element
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataType
    {
        /// <summary>
        /// Coded data type
        /// </summary>
        Coded,

        /// <summary>
        /// Date data type
        /// </summary>
        Date,

        /// <summary>
        /// Date/time data type
        /// </summary>
        DateTime,

        /// <summary>
        /// Numeric data
        /// </summary>
        Numeric,

        /// <summary>
        /// Textual data
        /// </summary>
        Text,

        /// <summary>
        /// No specified data type
        /// </summary>
        None,

        /// <summary>
        /// Textual data
        /// </summary>
        LongText,

        /// <summary>
        /// Textual data
        /// </summary>
        FormattedText,

        /// <summary>
        /// Integer
        /// </summary>
        Integer,

        /// <summary>
        /// Image/document
        /// </summary>
        ImageOrDocumentAttachment,
    }
}