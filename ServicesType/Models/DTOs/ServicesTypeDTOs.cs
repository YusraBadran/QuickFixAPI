using System;

namespace QuickFix.ServicesType.Models.DTOs
{
    public class ServicesTypeDTOs
    {
                public Guid Id {get; set;}
        public string  Name {get; set;}
        public string  NameEn {get; set;}
        public string  Description {get; set;}
        public string  DescriptionEn {get; set;}
        public ServicesTypeState Status {get; set;}
    }
}
