using ServiceLocator.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ServiceLocator
{
    public class ServiceLocatorInitializer : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad = "Scenes/MainScene";

        void Start()
        {
            ServiceLocator.Initiailze();
            FighterLocator fighterLocator = new FighterLocator();
            fighterLocator.Initialize();
            ServiceLocator.Current.Register(fighterLocator);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}