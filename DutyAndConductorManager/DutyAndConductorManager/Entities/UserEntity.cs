using System;
using System.Collections.Generic;

namespace DutyAndConductorManager.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastActiveDateTime { get; set; }
        public bool IsActive { get; set; }
        public string ActivationCode { get; set; }

        public UserDataEntity UserData { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
        public ICollection<NotificationEntity> Notifications { get; set; }
        public ICollection<NotificationRecipientEntity> NotificationRecipients { get; set; }
        public ICollection<BrigadeUserEntity> BrigadeUsers { get; set; }
    }

    public class UserDataEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Pesel { get; set; }
        public bool IsTrained { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarPath { get; set; }
        public string City { get; set; }
        
        public UserEntity User { get; set; }
    }
}