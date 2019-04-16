using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused;
    public bool GamePaused { get { return gamePaused; } }

    private PlayerLife playerLife;

    //SINGLETON//
    public static PauseMenu instance;

    private void Awake() {
        instance = this;
    }
    //END SINGLETON//

    private void Start() {
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    void Update()
    {
        if (!playerLife.Dead) {
            pauseMenu.SetActive(gamePaused);
            if (Input.GetKeyDown(KeyCode.Escape)) {
                gamePaused = !gamePaused;
            }


            if (gamePaused) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }


    public void Restart() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        LevelCounter.instance.ResetCounter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        LevelCounter.instance.DestroyCounter();
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        gamePaused = !gamePaused;
    }

    
}
