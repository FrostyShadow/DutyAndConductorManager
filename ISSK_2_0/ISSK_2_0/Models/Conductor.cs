﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISSK_2_0.Models
{
    public class Conductor
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public bool IsActive { get; set; }
        public string ActivationCode { get; set; }
        public virtual ConductorData ConductorData { get; set; }
    }

    public class ConductorData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Pesel { get; set; }
        public bool IsTrained { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public int ConductorId { get; set; }
        public virtual Conductor Conductor { get; set; }
    }
}