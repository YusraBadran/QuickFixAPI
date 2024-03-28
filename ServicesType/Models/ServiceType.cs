using QuickFix.Categories.Models;
using QuickFix.Shared.Module;
using System;
using System.Data.Common;

namespace QuickFix.ServicesType.Models
{
    public class ServiceType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
          public string? Logo { get; set; }
        public TypeStates Status { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}


