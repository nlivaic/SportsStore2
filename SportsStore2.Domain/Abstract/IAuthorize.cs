namespace SportsStore2.Domain.Abstract {
    public interface IAuthorize {
        bool Authorize(string username, string password);
    }
}
