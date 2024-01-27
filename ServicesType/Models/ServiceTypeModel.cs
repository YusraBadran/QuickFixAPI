using System;
using System.Data.Common;

namespace QuickFix.ServicesType.Models
{
    public class ServiceType
    {
        public Guid Id {get; set;}
        public string  Name {get; set;}
        public string  NameEn {get; set;}
        public string  Description {get; set;}
        public string  DescriptionEn {get; set;}
        public ServicesTypeState Status {get; set;}
    }
}


