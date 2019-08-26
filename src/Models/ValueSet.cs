using System;
using System.Collections.Generic;
using System.Diagnostics;
//using CDC.MMGAT.WebUI.Data;

namespace Cdc.mmg.validator.WebApi.Models
{
    /// <summary>
    /// Represents a PHIN VADS value set
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class ValueSet// : IEntity
    {
        /// <summary>
        /// Gets/sets the ID of this value set
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the VADS code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets/sets the VADS value set name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the VADS value set version number
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets/sets the status of this version of the value set
        /// </summary>
        public string VersionStatusCode { get; set; }

        /// <summary>
        /// Gets/sets the OID for this value set
        /// </summary>
        public string OID { get; set; }

        /// <summary>
        /// Gets/sets the definition of this value set
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Gets/sets the collection of value set concepts associated with this value set
        /// </summary>
        //public List<ValueSetConcept> Concepts { get; set; }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    return this.OID.Equals(((ValueSet)obj).OID, StringComparison.OrdinalIgnoreCase);
        //}

        /// <summary>
        /// Converts a VadsValueSet to a ValueSet
        /// </summary>
        //public static ValueSet NewValueSetFrom(VadsValueSet vadsValueSet)
        //{
        //    List<ValueSetConcept> valueSetConcepts = new List<ValueSetConcept>();
        //    foreach (var vadsValueSetConcept in vadsValueSet.Concepts)
        //    {
        //        var valueSetConcept = new ValueSetConcept()
        //        {
        //            Sequence = vadsValueSetConcept.Sequence,
        //            Code = vadsValueSetConcept.ConceptCode,
        //            CodeSystemName = "",
        //            CodeSystemVersion = "",
        //            HL70396Identifier = vadsValueSetConcept.Hl70396Identifier,
        //            CodeSystemCode = "",
        //            Name = vadsValueSetConcept.CodeSystemConceptName,
        //            PreferredName = vadsValueSetConcept.CdcPreferredDesignation,
        //            CodeSystemOID = vadsValueSetConcept.CodeSystemOid,
        //        };
        //        valueSetConcepts.Add(valueSetConcept);
        //    }

        //    var valueSet = new ValueSet()
        //    {
        //        Id = vadsValueSet.Id,
        //        Code = vadsValueSet.Code,
        //        Name = vadsValueSet.Name,
        //        Definition = "",
        //        OID = vadsValueSet.OID,
        //        VersionStatusCode = vadsValueSet.VersionStatusCode,
        //        Version = vadsValueSet.Version,
        //        Concepts = valueSetConcepts
        //    };

        //    return valueSet;
        //}
    }
}
