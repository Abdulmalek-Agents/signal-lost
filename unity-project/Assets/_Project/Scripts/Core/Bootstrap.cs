using UnityEngine;
using UnityEngine.SceneManagement;

namespace SignalLost.Core
{
    /// <summary>
    /// Entry-point MonoBehaviour. Lives in 00_Bootstrap scene.
    /// Initialises ServiceLocator, loads MainMenu scene additively.
    /// </summary>
    [DefaultExecutionOrder(-10000)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private string firstSceneToLoad = "01_MainMenu";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("[Bootstrap] Initialising services...");

            // Register services from children (AudioManager, SaveSystem, UIController, etc.)
            foreach (var s in GetComponentsInChildren<IService>(true))
            {
                s.Register();
                Debug.Log($"[Bootstrap] Registered {s.GetType().Name}");
            }
        }

        private void Start()
        {
            if (!string.IsNullOrEmpty(firstSceneToLoad))
            {
                SceneManager.LoadScene(firstSceneToLoad, LoadSceneMode.Additive);
                Debug.Log($"[Bootstrap] Loaded {firstSceneToLoad}");
            }
        }
    }

    public interface IService
    {
        void Register();
    }
}
