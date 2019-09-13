using System;
using Xunit;
using Cdc.Mmg.Validator.WebApi.Controllers;

namespace ValidatorValidator
{
    public class Converters
    {
        public Converters()
        {
        }
        
        // MSH|^~\&|SendAppName^2.16.840.1.114222.TBD^ISO|Sending-Facility^2.16.840.1.114222.TBD^ISO|PHINCDS^2.16.840.1.114222.4.3.2.10^ISO|PHIN^2.16.840.1.114222^ISO|20140630120030.1234-0500||ORU^R01^ORU_R01|MESSAGE CONTROL ID|D|2.5.1|||||||||NOTF_ORU_v3.0^PHINProfileID^2.16.840.1.114222.4.10.3^ISO ~Generic_MMG_V2.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO ~TB_MMG_V3.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO
        [Fact]
        public void HL7_JSON()
        {
            try
            {
                // ARRANGE
                // var controller = new ConverterController();
                // string HL7 = "MSH|^~\\&|SendAppName^2.16.840.1.114222.TBD^ISO|Sending-Facility^2.16.840.1.114222.TBD^ISO|PHINCDS^2.16.840.1.114222.4.3.2.10^ISO|PHIN^2.16.840.1.114222^ISO|20140630120030.1234-0500||ORU^R01^ORU_R01|MESSAGE CONTROL ID|D|2.5.1|||||||||NOTF_ORU_v3.0^PHINProfileID^2.16.840.1.114222.4.10.3^ISO~Generic_MMG_V2.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO~TB_MMG_V3.0^PHINMsgMapID^2.16.840.1.114222.4.10.4^ISO";
                
                // ACT
                // controller.Post();
            }
            catch
            {

            }
        }
    }
}
