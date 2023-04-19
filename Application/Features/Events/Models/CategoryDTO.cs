using System;
using System.Collections.Generic;
using CQRS.Application.Common.Mapping;
using Domain.Entities;

namespace  CQRS.Application.Features.Event.Models
{
    public class CategoryDTO:IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }

     
    }
}
