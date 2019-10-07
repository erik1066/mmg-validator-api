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
    }
}
