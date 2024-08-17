using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CityBuilder.SceneLoaders
{
    public class SceneLoader : MonoBehaviour
    {
        // todo made it in singleton dont destroy on load

        public async void LoadGameScene()
        {
            await SceneManager.LoadSceneAsync("GameScene");
        } 
    }
}
