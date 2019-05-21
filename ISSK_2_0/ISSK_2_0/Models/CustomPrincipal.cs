using System.Linq;
using System.Security.Principal;
using System.Web.Security;

namespace ISSK_2_0.Models
{
    public class CustomPrincipal : IPrincipal
    {

        #region Identity Properties
        public int ConductorId { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        #endregion

        public bool IsInRole(string role)
        {
            return Roles.Any(role.Contains);
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public IIdentity Identity { get; }
    }
}