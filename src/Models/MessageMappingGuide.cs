using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
//using CDC.MMGAT.WebUI.Data;
using Newtonsoft.Json;

namespace Cdc.mmg.validator.WebApi.Models
{
    /// <summary>
    /// A class representing a message mapping guide.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public sealed class MessageMappingGuide //: IEntity, IVersionedEntity
    {
        #region Properties
        /// <summary>
        /// Gets/sets the ID of the message mapping guide. Only used internally by
        ///  the software.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the guide's name. Example: "Generic Version 2"
        /// </summary>
        [Required]
        [StringLength(64)]
        [RegularExpression("^([\\-\\.a-zA-Z0-9:,\\(\\)/!=' ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "The MMG name contains invalid characters")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the guide's description
        /// </summary>
        [Required]
        [StringLength(4096)]
        [RegularExpression("^([\\-\\.a-zA-Z0-9:,\\(\\)/!\\?='\r\n ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "The MMG description contains invalid characters")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets whether this MMG is active or inactive
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets/sets the user ID of the person who created this MMG
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the user ID of the person who currently owns this MMG
        /// </summary>
        public string OwnedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the full name of the person who currently owns this MMG
        /// </summary>
        public string OwnedByFullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the internal version of the entity.
        /// </summary>
        public int InternalVersion { get; set; } = 0;
        
        /// <summary>
        /// Gets/sets the date this entity was last updated
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets/sets the date this entity was last created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets/sets the public-facing version number of this entity.
        ///  Leave this string empty for entities that are not published.
        /// </summary>
        /// <remarks>
        /// Should follow the major.minor versioning scheme, e.g. "1.2"
        /// </remarks>
        // [RegularExpression("^[0-9\\.]*$")]
        [StringLength(10)]
        [RegularExpression("^([\\-\\.a-zA-Z0-9:,\\(\\)/ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "The published version number contains invalid characters")]
        public string PublishVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the MSH-21 profile identifier string for this MMG
        /// </summary>
        [StringLength(180)]
        [RegularExpression("^([\\-\\.a-zA-Z0-9:,\\(\\)/!\\?='\\^~_ ÇüéâäàåçêëèïîíìÄÅÉæÆôöòûùÖÜáíóúñÑÀÁÂÃÈÊËÌÍÎÏÐÒÓÔÕØÙÚÛÝßãðõøýþÿ]+)$", ErrorMessage = "The MMG profile identifier contains invalid characters")]
        public string ProfileIdentifier { get; set; } = string.Empty;

        /// <summary>
        /// Gets/sets the public-facing date that this entity was published.
        ///  Leave this null for entities that are not published.
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Gets/sets the nationally-notifiable conditions that this guide is applicable
        ///  to. The list of conditions is based on the numeric condition code. For example,
        ///  a value of 10660 represents Yellow Fever.
        /// </summary>
        public List<int> Conditions { get; set; } = new List<int>();

        /// <summary>
        /// Gets/sets the list of templates that were used to build this message mapping guide.
        ///  Note that the templates are referred to by their Guids.
        /// </summary>
        public List<Guid> Templates { get; set; } = new List<Guid>();

        /// <summary>
        /// Gets/sets the guide's template status.
        /// </summary>
       // public TemplateStatus TemplateStatus { get; set; } = TemplateStatus.Disallowed;

        /// <summary>
        /// Gets/sets the state of this message mapping guide
        /// </summary>
       // public GuideStatus State { get; set; } = GuideStatus.Development;

        /// <summary>
        /// Gets the collection of data elements that are used in this guide
        /// </summary>
        [JsonIgnore]
        public IEnumerable<DataElement> Elements 
        {
            get 
            {
                var elements = new List<DataElement>();
                foreach(var block in Blocks)
                {
                    elements.AddRange(block.Elements);
                }
                return elements;
            }
        }

        /// <summary>
        /// Gets the collection of value sets that are used in this guide
        /// </summary>
        [JsonIgnore]
        public IEnumerable<ValueSet> ValueSets
        {
            get 
            {
                var valueSets = new List<ValueSet>();
                foreach(var block in Blocks)
                {
                    foreach (var element in block.Elements)
                    {
                        if (element.ValueSet != null && !valueSets.Contains(element.ValueSet))
                        {
                            valueSets.Add(element.ValueSet);
                        }
                    }
                }
                return valueSets;
            }
        }

        /// <summary>
        /// Gets/sets the collection of blocks that are used in this guide
        /// </summary>
        public List<Block> Blocks { get; set; } = new List<Block>();
        #endregion // Properties
        
        /// <summary>
        /// Adds a data element to the guide's collection of data elements
        /// </summary>
        /// <param name="block">The block where the data element should be added</param>
        /// <param name="element">The data element to add to the guide</param>
        public void Add(Block block, DataElement element)
        {
            if (Elements
                .Where(e => e.Identifier.Equals(element.Identifier, StringComparison.OrdinalIgnoreCase))
                .Where(e => e.HL7Identifier.Equals(element.HL7Identifier))
                .FirstOrDefault() == null)
            {
                // no matching DE found, add it to the right block
                block.Add(element);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add element '{element.Identifier}' to this guide; element already exists!");
            }
        }

        /// <summary>
        /// Adds a block to the guide's collection of blocks
        /// </summary>
        /// <param name="block">The block to be added</param>
        public void Add(Block block)
        {
            if (!Blocks.Contains(block) && Blocks.FirstOrDefault(b => b.Name.Equals(block.Name, StringComparison.OrdinalIgnoreCase)) == null)
            {
                // no matching block exists so add it
                Blocks.Add(block);
            }
            else
            {
                throw new InvalidOperationException($"Cannot add block '{block.Name}' to this guide; block already exists!");
            }
        }

        /// <summary>
        /// Gets the block to which this data element belongs. Returns null if the element does not belong to this guide.
        /// </summary>
        /// <param name="element">The element whose block needs to be determined</param>
        /// <returns>The block that the element belongs to</returns>
        public Block GetBlock(DataElement element)
        {
            return Blocks.FirstOrDefault(b => b.Elements.FirstOrDefault(e => e.Id.Equals(element.Id)) != null);
        }
    }
}
