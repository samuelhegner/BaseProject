using System.Threading.Tasks;

namespace Backend.Managers
{
    public interface IBackendManager
    {
        public Task SignIn();
        public Task SignOut();
    }
}