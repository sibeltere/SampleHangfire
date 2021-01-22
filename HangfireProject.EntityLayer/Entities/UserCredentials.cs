using HangfireProject.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HangfireProject.EntityLayer.Entities
{
    public class UserCredentials:BaseEntity
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
