using UnityEngine;
using UnityEngine.SceneManagement;

namespace SignalLost.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private string hubScene = "02_Hub_PlayerShip";
        [SerializeField] private GameObject pauseMenuRoot;

        public void NewGame() => SceneManager.LoadScene(hubScene);
        public void Quit() => Application.Quit();

        public void TogglePause()
        {
            bool paused = !pauseMenuRoot.activeSelf;
            pauseMenuRoot.SetActive(paused);
            Time.timeScale = paused ? 0f : 1f;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused;
        }
    }
}
