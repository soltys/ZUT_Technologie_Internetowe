using Ninject;
using Ninject.Extensions.Logging;
using PSBlog.Repository;
using System;
using System.Linq;
using System.Web.Mvc;
namespace PSBlog.Authentication
{
    public class PSBlogRoleProvider : System.Web.Security.RoleProvider, IDisposable
    {

        [Inject]
        public ILogger _logger { get; set; }

        private IUserRepository _userRepository;

        private bool _disposed;

        public PSBlogRoleProvider()
        {
            _userRepository = DependencyResolver.Current.GetService<IUserRepository>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = _userRepository.GetRolesForUser(username);
            return userRoles.Any(role => role == roleName);            
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                return _userRepository.GetRolesForUser(username).ToArray();
            }
            catch(Exception e)
            {
                return new string[] { };
            }
            
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }


        public void Dispose()
        {
            Dispose(true);

            // Call SupressFinalize in case a subclass implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_userRepository != null)
                    {
                        _userRepository.Dispose();
                        _logger.Info("Repository Disposed " + ToString());
                    }
                }

                _userRepository = null;
                // Indicate that the instance has been disposed.
                _disposed = true;
            }
        }
    }
}