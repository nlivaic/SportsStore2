namespace SportsStore2.Domain.Abstract {
    interface IAuthorize {
        bool Authorize(string username, string password);
    }
}
