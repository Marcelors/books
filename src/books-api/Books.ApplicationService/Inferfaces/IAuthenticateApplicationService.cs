using Books.ApplicationService.Model;

namespace Books.ApplicationService.Inferfaces
{
    public interface IAuthenticateApplicationService
    {
        UserResultModel Authenticate(AuthenticateModel model);
    }
}
