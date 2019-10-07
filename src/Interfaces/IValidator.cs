using Cdc.Mmg.Validator.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HL7.Dotnetcore;
namespace Cdc.mmg.validator.WebApi.Interfaces
{
    public interface IValidator
    {
        List<string> ValidateOBX(List<Segment> segList , List<DataElement> mmgElementList,string ApiUri);
       
        List<string> ValidateNK1(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidateNTE(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidatePID(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidateMSH(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidateSPM(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidateOBR(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
        List<string> ValidatePV1(List<Segment> segList, List<DataElement> mmgElementList, string ApiUri);
    }
}
