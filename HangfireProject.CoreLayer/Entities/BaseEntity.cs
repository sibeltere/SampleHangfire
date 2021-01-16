using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.CoreLayer.Entities
{
    public class BaseEntity:IEntity
    {
        public int Id { get; set; }
    }
}
