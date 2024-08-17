using System.Collections.Generic;
using CityBuilder.DataStorage;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CityBuilder.Interfaces
{
    public interface ISpawner <T, K> where T : DataStorageBase<K> where K : IStorageable
    {
        public void Init(T dataStorage, DiContainer container);
        public UniTask<IEnumerable<K>> Spawn(IEnumerable<int> requestedIds);
    }
}