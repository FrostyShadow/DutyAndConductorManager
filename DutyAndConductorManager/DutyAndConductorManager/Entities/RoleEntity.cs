using System.Collections.Generic;

namespace DutyAndConductorManager.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}