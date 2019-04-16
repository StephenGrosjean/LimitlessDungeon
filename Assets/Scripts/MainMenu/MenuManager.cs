using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject credits;

    private void Start() {
        StartCoroutine("FadeToWhite");
    }

    public void StartGame() {
        StartCoroutine("FadeToBlack", true);
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
    }

    public void ToggleCredits() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        credits.SetActive(!credits.activeInHierarchy);
    }

    public void Exit() {
        StartCoroutine("FadeToBlack");
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        Application.Quit();
    }

    IEnumerator FadeToBlack(bool loadLevels = false) {
        float alpha = 0.0f;
        while (alpha <= 1.1f) {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
            alpha += 0.1f;
        }

        if (loadLevels) {
            LoadLevels();
        }
    }

    IEnumerator FadeToWhite() {
        float alpha = 1.0f;
        while (alpha >= -0.1f) {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
            alpha -= 0.1f;
        }
    }

    void LoadLevels() {
        SceneManager.LoadScene("Levels");
    }
}
