using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Image damageUI, damageOverlay;
    [SerializeField] private GameObject deathUI;

    [SerializeField] private int life;
    public int Life { set { life = value; } }

    private bool wasHit;
    private float alpha;
    private float fullLife;
    private bool dead;
    public bool Dead { get { return dead; } }

    void Start()
    {
        fullLife = life;
    }

    void Update() {
        if (life <= 0 && !dead) {
            dead = true;
            SoundManager.instance.PlaySound(SoundManager.sound.Player_Die);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            deathUI.SetActive(true);
        }

        
        
    }

    public void Hit() {
        life--;
        SoundManager.instance.PlaySound(SoundManager.sound.Player_Hit);
        damageUI.fillAmount = life / fullLife;
        if (!wasHit) {
            wasHit = true;
            StartCoroutine("DamageOverlay");
        }
    }

    IEnumerator DamageOverlay() {
        alpha = 1f;
        while (alpha > -0.1f) {
            damageOverlay.color = new Color(1, 1, 1, alpha);
            yield return null;
            alpha -= 0.1f;
        }
        wasHit = false;

    }

}
