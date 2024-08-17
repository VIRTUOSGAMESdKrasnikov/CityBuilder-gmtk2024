using Cysharp.Threading.Tasks;

namespace CityBuilder.Interfaces
{
    public interface ISpawnable
    {
        public UniTask<bool> Spawn(int id);
    }
}