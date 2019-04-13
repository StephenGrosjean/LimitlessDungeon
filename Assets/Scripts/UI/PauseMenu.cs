using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused;
    public bool GamePaused { get { return gamePaused; } }


    //SINGLETON//
    public static PauseMenu instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu.SetActive(gamePaused);
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gamePaused = !gamePaused;
        }

        if (gamePaused) {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }


    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelCounter.instance.ResetCounter();
    }

    public void Menu() {
        SceneManager.LoadScene("MainMenu");
        LevelCounter.instance.DestroyCounter();
    }

    public void Continue() {
        gamePaused = !gamePaused;
    }

    
}
