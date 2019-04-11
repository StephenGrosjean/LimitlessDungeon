using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Image damageUI, damageOverlay;

    [SerializeField] private int life;
    public int Life { set { life = value; } }

    private bool wasHit;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update() {
        if (life <= 0) {
            Debug.Log("PlayerDEAD");
        }

        
    }

    public void Hit() {
        life--;
        if (!wasHit) {
            wasHit = true;
            StartCoroutine("DamageOverlay");
        }
    }

    IEnumerator DamageOverlay() {
        alpha = 1f;
        while (alpha > 0.0f) {
            damageOverlay.color = new Color(1, 1, 1, alpha);
            yield return null;
            alpha -= 0.1f;
        }
        wasHit = false;

    }

}
