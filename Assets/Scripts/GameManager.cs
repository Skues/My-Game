using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void PlayerCaught(){
        LevelStats.Instance.PlayerSpotted();   
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
    public void RestartLevel(){
        // Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
