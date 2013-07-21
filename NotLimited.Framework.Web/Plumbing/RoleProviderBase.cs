using System.Web.Security;

namespace NotLimited.Framework.Web.Plumbing
{
    public class RoleProviderBase : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            return null;
        }

        public override void CreateRole(string roleName)
        {
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return false;
        }

        public override bool RoleExists(string roleName)
        {
            return false;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return null;
        }

        public override string[] GetAllRoles()
        {
            return null;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return null;
        }

        public override string ApplicationName
        {
            get { return null; }
            set { }
        }
    }
}