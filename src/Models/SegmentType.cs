using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cdc.mmg.validator.WebApi.Models
{
    /// <summary>
    /// Enum for HL7 v2.5.1 segment types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SegmentType
    {
        /// <summary>
        /// Abstract
        /// </summary>
        ABS,

        /// <summary>
        /// Accident
        /// </summary>
        ACC,

        /// <summary>
        /// Addendum
        /// </summary>
        ADD,

        /// <summary>
        /// Professional affiliation
        /// </summary>
        AFF,

        /// <summary>
        /// Appointment Information - General Resource
        /// </summary>
        AIG,

        /// <summary>
        /// Appointment Information - Location Resource
        /// </summary>
        AIL,

        /// <summary>
        /// Appointment Information - Personnel Resource
        /// </summary>
        AIP,

        /// <summary>
        /// Appointment information
        /// </summary>
        AIS,

        /// <summary>
        /// Patient allergy information
        /// </summary>
        AL1,

        /// <summary>
        /// Appointment Preferences
        /// </summary>
        APR,

        /// <summary>
        /// Appointment request
        /// </summary>
        ARQ,

        /// <summary>
        /// Authorization Information
        /// </summary>
        AUT,

        /// <summary>
        /// Batch Header
        /// </summary>
        BHS,

        /// <summary>
        /// Blood code
        /// </summary>
        BLC,

        /// <summary>
        /// Billing
        /// </summary>
        BLG,

        /// <summary>
        /// Blood product order
        /// </summary>
        BPO,

        /// <summary>
        /// Blood product dispense status
        /// </summary>
        BPX,

        /// <summary>
        /// Batch trailer
        /// </summary>
        BTS,

        /// <summary>
        /// Blood Product Transfusion/Disposition
        /// </summary>
        BTX,

        /// <summary>
        /// Charge Description Master
        /// </summary>
        CDM,

        /// <summary>
        /// Certificate Detail
        /// </summary>
        CER,

        /// <summary>
        /// Clinical Study Master
        /// </summary>
        CM0,

        /// <summary>
        /// Clinical Study Phase Master
        /// </summary>
        CM1,

        /// <summary>
        /// Clinical Study Schedule Master
        /// </summary>
        CM2,

        /// <summary>
        /// Clear notification
        /// </summary>
        CNS,

        /// <summary>
        /// Consent segment
        /// </summary>
        CON,

        /// <summary>
        /// Clinical study phase
        /// </summary>
        CSP,

        /// <summary>
        /// Clinical study registration
        /// </summary>
        CSR,

        /// <summary>
        /// Clinuial study data schedule segment
        /// </summary>
        CSS,

        /// <summary>
        /// Contact data
        /// </summary>
        CTD,

        /// <summary>
        /// Clinical trial identification
        /// </summary>
        CTI,

        /// <summary>
        /// Disability
        /// </summary>
        DB1,

        /// <summary>
        /// Diagnosis
        /// </summary>
        DG1,

        /// <summary>
        /// Diagnosis Related Group
        /// </summary>
        DRG,

        /// <summary>
        /// Continuation pointer
        /// </summary>
        DSC,

        /// <summary>
        /// Display data
        /// </summary>
        DSP,

        /// <summary>
        /// Equipment Command
        /// </summary>
        ECD,

        /// <summary>
        /// Equipment Command response
        /// </summary>
        ECR,

        /// <summary>
        /// Educational Detail
        /// </summary>
        EDU,

        /// <summary>
        /// Embedded Query Language
        /// </summary>
        EQL,

        /// <summary>
        /// Equipment/log Service
        /// </summary>
        EQP,

        /// <summary>
        /// Equipment Detail
        /// </summary>
        EQU,

        /// <summary>
        /// Event replay query
        /// </summary>
        ERQ,

        /// <summary>
        /// Error
        /// </summary>
        ERR,

        /// <summary>
        /// Event Type
        /// </summary>
        EVN,

        /// <summary>
        /// Facility
        /// </summary>
        FAC,

        /// <summary>
        /// File header
        /// </summary>
        FHS,

        /// <summary>
        /// Financial Transaction
        /// </summary>
        FT1,

        /// <summary>
        /// File trailer
        /// </summary>
        FTS,

        /// <summary>
        /// Goal detail
        /// </summary>
        GOL,

        /// <summary>
        /// Grouping/Reimbursement - Visit
        /// </summary>
        GP1,

        /// <summary>
        /// Grouping/Reimbursement - Procedure Line Item
        /// </summary>
        GP2,

        /// <summary>
        /// Guarantor
        /// </summary>
        GT1,

        /// <summary>
        /// 
        /// </summary>
        Hxx,

        /// <summary>
        /// Patient Adverse Reaction Information
        /// </summary>
        IAM,

        /// <summary>
        /// Inventory Item Master
        /// </summary>
        IIM,

        /// <summary>
        /// Insurance
        /// </summary>
        IN1,

        /// <summary>
        /// Insurance Additional Information
        /// </summary>
        IN2,

        /// <summary>
        /// Insurance Additional Information, Certification
        /// </summary>
        IN3,

        /// <summary>
        /// Inventory detail
        /// </summary>
        INV,

        /// <summary>
        /// Imaging Procedure Control Segment
        /// </summary>
        IPC,

        /// <summary>
        /// Interaction Status Detail
        /// </summary>
        ISD,

        /// <summary>
        /// Language Detail
        /// </summary>
        LAN,

        /// <summary>
        /// Location Charge Code
        /// </summary>
        LCC,

        /// <summary>
        /// Location Characteristic
        /// </summary>
        LCH,

        /// <summary>
        /// Location department
        /// </summary>
        LDP,

        /// <summary>
        /// Location identification
        /// </summary>
        LOC,

        /// <summary>
        /// Location relationship
        /// </summary>
        LRL,

        /// <summary>
        /// Master File Acknowledgment
        /// </summary>
        MFA,

        /// <summary>
        /// Master File Entry
        /// </summary>
        MFE,

        /// <summary>
        /// Master File Identification
        /// </summary>
        MFI,

        /// <summary>
        /// Merge Patient Information
        /// </summary>
        MRG,

        /// <summary>
        /// Message Acknowledgment
        /// </summary>
        MSA,

        /// <summary>
        /// Message Header
        /// </summary>
        MSH,

        /// <summary>
        /// System Clock
        /// </summary>
        NCK,

        /// <summary>
        /// Notification Detail
        /// </summary>
        NDS,

        /// <summary>
        /// Next of Kin / Associated Pa
        /// </summary>
        NK1,

        /// <summary>
        /// Bed Status Update
        /// </summary>
        NPU,

        /// <summary>
        /// Application Status Change
        /// </summary>
        NSC,

        /// <summary>
        /// Application control level statistics
        /// </summary>
        NST,

        /// <summary>
        /// Notes and Comments
        /// </summary>
        NTE,

        /// <summary>
        /// Observation Request
        /// </summary>
        OBR,

        /// <summary>
        /// Observation/Result
        /// </summary>
        OBX,

        /// <summary>
        /// Dietary Orders, Supplements, and Preferences
        /// </summary>
        ODS,

        /// <summary>
        /// Diet Tray Instructions
        /// </summary>
        ODT,

        /// <summary>
        /// General Segment
        /// </summary>
        OM1,

        /// <summary>
        /// Numeric Observation
        /// </summary>
        OM2,

        /// <summary>
        /// Categorical Service/Test/Observation
        /// </summary>
        OM3,

        /// <summary>
        /// Observations that Require Specimens
        /// </summary>
        OM4,

        /// <summary>
        /// Observation Batteries (Sets)
        /// </summary>
        OM5,

        /// <summary>
        /// Observations that are Calculated from Other Observations
        /// </summary>
        OM6,

        /// <summary>
        /// Additional Basic Attributes
        /// </summary>
        OM7,

        /// <summary>
        /// Common Order
        /// </summary>
        ORC,

        /// <summary>
        /// Practitioner Organization Unit
        /// </summary>
        ORG,

        /// <summary>
        /// Override Segment
        /// </summary>
        OVR,

        /// <summary>
        /// Possible Causal Relationship
        /// </summary>
        PCR,

        /// <summary>
        /// Patient Additional Demographic
        /// </summary>
        PD1,

        /// <summary>
        /// Patient Death and Autopsy
        /// </summary>
        PDA,

        /// <summary>
        /// Product Detail Country
        /// </summary>
        PDC,

        /// <summary>
        /// Product Experience Observation
        /// </summary>
        PEO,

        /// <summary>
        /// Product Experience Sender
        /// </summary>
        PES,

        /// <summary>
        /// Patient Identification
        /// </summary>
        PID,

        /// <summary>
        /// Procedures
        /// </summary>
        PR1,

        /// <summary>
        /// Practitioner Detail
        /// </summary>
        PRA,

        /// <summary>
        /// Problem Details
        /// </summary>
        PRB,

        /// <summary>
        /// Pricing
        /// </summary>
        PRC,

        /// <summary>
        /// Provider data
        /// </summary>
        PRD,

        /// <summary>
        /// Product summary header
        /// </summary>
        PSH,

        /// <summary>
        /// Pathway
        /// </summary>
        PTH,

        /// <summary>
        /// Patient visit
        /// </summary>
        PV1,

        /// <summary>
        /// Patient Visit - Additional Information
        /// </summary>
        PV2,

        /// <summary>
        /// Query Acknowledgment
        /// </summary>
        QAK,

        /// <summary>
        /// Query Identification
        /// </summary>
        QID,

        /// <summary>
        /// Query Parameter Definition
        /// </summary>
        QPD,

        /// <summary>
        /// Original-Style Query Definition
        /// </summary>
        QRD,

        /// <summary>
        /// Original style query filter
        /// </summary>
        QRF,

        /// <summary>
        /// Query Response Instance
        /// </summary>
        QRI,

        /// <summary>
        /// Response Control Parameter
        /// </summary>
        RCP,

        /// <summary>
        /// Table Row Definition
        /// </summary>
        RDF,

        /// <summary>
        /// Table Row Data
        /// </summary>
        RDT,

        /// <summary>
        /// Referral Information
        /// </summary>
        RF1,

        /// <summary>
        /// Resource Group
        /// </summary>
        RGS,

        /// <summary>
        /// Risk Management Incident
        /// </summary>
        RMI,

        /// <summary>
        /// Role
        /// </summary>
        ROL,

        /// <summary>
        /// Requisition Detail-1
        /// </summary>
        RQ1,

        /// <summary>
        /// Requisition Detail
        /// </summary>
        RQD,

        /// <summary>
        /// Pharmacy/Treatment Administration
        /// </summary>
        RXA,

        /// <summary>
        /// Pharmacy/Treatment Component Order
        /// </summary>
        RXC,

        /// <summary>
        /// Pharmacy/Treatment Dispense
        /// </summary>
        RXD,

        /// <summary>
        /// Pharmacy/Treatment Encoded Order
        /// </summary>
        RXE,

        /// <summary>
        /// Pharmacy/Treatment Encoded Give
        /// </summary>
        RXG,

        /// <summary>
        /// Pharmacy/Treatment Order
        /// </summary>
        RXO,

        /// <summary>
        /// Pharmacy/Treatment Route
        /// </summary>
        RXR,

        /// <summary>
        /// Specimen Container detail
        /// </summary>
        SAC,

        /// <summary>
        /// Scheduling Activity Information
        /// </summary>
        SCH,

        /// <summary>
        /// Software Segment
        /// </summary>
        SFT,

        /// <summary>
        /// Substance Identifier
        /// </summary>
        SID,

        /// <summary>
        /// Specimen
        /// </summary>
        SPM,

        /// <summary>
        /// Stored Procedure Request Definition
        /// </summary>
        SPR,

        /// <summary>
        /// Staff Identification
        /// </summary>
        STF,

        /// <summary>
        /// Test Code Configuration
        /// </summary>
        TCC,

        /// <summary>
        /// Test Code Detail
        /// </summary>
        TCD,

        /// <summary>
        /// Timing/Quantity
        /// </summary>
        TQ1,

        /// <summary>
        /// Timing/Quantity Relationship
        /// </summary>
        TQ2,

        /// <summary>
        /// Transcription Document Header
        /// </summary>
        TXA,

        /// <summary>
        /// UB82
        /// </summary>
        UB1,

        /// <summary>
        /// UB92 Data
        /// </summary>
        UB2,

        /// <summary>
        /// Results/update definition
        /// </summary>
        URD,

        /// <summary>
        /// Unsolicited Selection
        /// </summary>
        URS,

        /// <summary>
        /// Variance
        /// </summary>
        VAR,

        /// <summary>
        /// Virtual Table Query Request
        /// </summary>
        VTQ,

        /// <summary>
        /// (proposed example only)
        /// </summary>
        ZL7,

        /// <summary>
        /// 
        /// </summary>
        Zxx
    }

}
