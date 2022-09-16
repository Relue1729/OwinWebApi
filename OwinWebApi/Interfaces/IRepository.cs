using System.Threading.Tasks;

namespace OwinWebApi.Interfaces
{
    public interface IRepository
    {
        Task IncrementAtIndexAsync(int index);
        Task<int?> GetValue(int index);
    }
}
