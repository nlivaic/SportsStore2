using SportsStore2.Domain.Abstract;

namespace SportsStore2.Domain.Concrete {
    public class FormsAuthorization : IAuthorize {
        public bool Authorize(string username, string password) {
            return true;
        }
    }
}