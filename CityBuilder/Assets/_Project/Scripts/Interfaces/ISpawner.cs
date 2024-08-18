using System.Collections.Generic;
using CityBuilder.DataStorage;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CityBuilder.Interfaces
{
    public interface ISpawner <T, K, P> where T : DataStorageBase<K> where K : IStorageable where P : ISpawnable
    {
        public void Init(T dataStorage, DiContainer container);
        public UniTask<IEnumerable<P>> Spawn(IEnumerable<int> requestedIds);
    }
}