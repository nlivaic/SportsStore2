using SportsStore2.Domain.Abstract;
using System.Web.Security;

namespace SportsStore2.Domain.Concrete {
    public class FormsAuthorization : IAuthorize {
        public bool Authorize(string username, string password) {
            if (FormsAuthentication.Authenticate(username, password)) {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }
    }
}