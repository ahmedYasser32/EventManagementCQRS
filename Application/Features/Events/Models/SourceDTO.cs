using CQRS.Application.Common.Mapping;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Events.Models

{
    public class SourceDTO :IMapFrom<Source>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 


    }
}
