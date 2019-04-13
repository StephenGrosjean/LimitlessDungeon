using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLevel : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameController.instance.StartCoroutine("FadeToBlack");
            LevelCounter.instance.NextLevel();
            Time.timeScale = 0;
            Invoke("ChangeScene", 2);
        }
    }

    private void ChangeScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
