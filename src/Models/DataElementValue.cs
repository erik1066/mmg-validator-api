using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cdc.Mmg.Validator.WebApi.Models
{
    /// <summary>
    /// Represents a value that can be associated with a data element. This class
    ///  is used for cases when the user wishes to fill in a data entry form for
    ///  the message mapping guide, such as when they want to create test messages.
    /// </summary>
    [DebuggerDisplay("{Value} : {Label}")]
    public sealed class DataElementValue
    {
        /// <summary>
        /// Gets/sets the data element value's repeating group ID. Only applicable
        ///  when the element appears in a repeating block.
        /// </summary>
        public int RepeatingGroupId { get; set; } = 1;

        /// <summary>
        /// Gets/sets the data element value
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the data element's label, which describes the value
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the data element's 'other' value
        /// </summary>
        public string OriginalText { get; set; } = string.Empty;
    }
}
