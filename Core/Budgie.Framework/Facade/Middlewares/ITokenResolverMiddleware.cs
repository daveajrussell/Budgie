using Budgie.Core;
using System.Threading.Tasks;

namespace Budgie.Framework.Facade.Middlewares
{
    public interface ITokenResolverMiddleware
    {
        Task<User> ResolveAsync();
    }
}
