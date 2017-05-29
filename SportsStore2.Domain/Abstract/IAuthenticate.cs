namespace SportsStore2.Domain.Abstract {
    interface IAuthenticate {
        bool Authenticate(string username, string password);
    }
}
