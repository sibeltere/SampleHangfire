using HangfireProject.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireProject.EntityLayer.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
