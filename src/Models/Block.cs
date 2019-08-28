using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cdc.mmg.validator.WebApi.Models
{
    /// <summary>
    /// Represents a named block of data elements within a message mapping guide
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public sealed class Block
    {
        private int _repetitionsForDisplayPurposes = 1;

        /// <summary>
        /// Gets/sets the name of the block. Example: "Demographics"
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the block's description which will appear at the top of the block
        ///  when the block is displayed to end users
        /// </summary>
        public string StartingDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the block's description which will appear at the bottom of the block
        ///  when the block is displayed to end users
        /// </summary>
        public string EndingDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the internal ID of the block
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the order this block appears in the message mapping guide
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Gets/sets the date the block was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets/sets the type of block. 
        /// </summary>
       // public BlockType BlockType { get; set; }

        /// <summary>
        /// Gets/sets whether the block's name should be displayed. Context-dependent.
        /// </summary>
        public bool ShouldDisplayName { get; set; } = true;

        /// <summary>
        /// Gets/sets some basic information about the template from which this block was
        ///  derived.
        /// </summary>
      //  public TemplateInfo TemplateInformation { get; set; }

        /// <summary>
        /// Gets/sets the nationally-notifiable conditions that this block is applicable
        ///  to. The list of conditions is based on the numeric condition code. For example,
        ///  a value of 10660 represents Yellow Fever.
        /// </summary>
        public List<int> Conditions { get; set; } = new List<int>();

        /// <summary>
        /// Gets/sets the list of data elements that are associated with this block
        /// </summary>
        public List<DataElement> Elements { get; set; } = new List<DataElement>();

        /// <summary>
        /// Gets/sets the desired number of times to display this block. For example, if
        ///  this number is "5", then the desired behavior should be to see five instances
        ///  of the elements in this block. This property is intended for display purposes
        ///  only and not for validation or authoring.
        /// </summary>
        //public int RepetitionsForDisplayPurposes
        //{
        //    get
        //    {
        //        if (BlockType != BlockType.Single)
        //        {
        //            return _repetitionsForDisplayPurposes;
        //        }
        //        else
        //        {
        //            return 1;
        //        }
        //    }
        //    set
        //    {
        //        if (BlockType != BlockType.Single)
        //        {
        //            _repetitionsForDisplayPurposes = value;
        //        }
        //        else
        //        {
        //            _repetitionsForDisplayPurposes = 1;
        //        }
        //    }
        //}

        /// <summary>
        /// Adds a data element to the block's collection of data elements
        /// </summary>
        /// <param name="element">The data element to add to the block</param>
        public void Add(DataElement element)
        {
            if (!Elements.Contains(element) && 
                Elements
                    .Where(e => e.hL7OBRParent.HasValue == false || e.hL7OBRParent.Value <= 1)
                    .Where(e => e.identifier.Equals(element.identifier, StringComparison.OrdinalIgnoreCase))
                    .Where(e => e.hL7Identifier.Equals(element.hL7Identifier))
                    .FirstOrDefault() == null)
            {
                // no matching DE found, add it to the right block
                Elements.Add(element);
            }
            else
            {
                throw new InvalidOperationException($"Unable to add element '{element.identifier}' to block '{Name}' - the element already exists! Check to make you're not adding a different element with the same identifier.");
            }
        }
    }
}
