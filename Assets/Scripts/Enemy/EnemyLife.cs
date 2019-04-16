using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private Renderer rd;
    [SerializeField] private GameObject smokeParticles;
    [SerializeField] private float respawnTime;

    private Vector3 backPosition;
    public Vector3 BackPosition { set { backPosition = value; } }

    private bool hasBeenKilled;
    private int startLife;

    void Start()
    {
        startLife = life;
    }

    void Update()
    {
        if(life <= 0 && !hasBeenKilled) {
            SoundManager.instance.PlaySound(SoundManager.sound.Enemy_Die);
            hasBeenKilled = true;
            Instantiate(smokeParticles, transform.position, Quaternion.identity);
            transform.position = backPosition;
            life = startLife;
            StartCoroutine("WaitForRespawn");
            
        }

        rd.enabled = !hasBeenKilled;
    }

    public void Hit() {
        life--;
        StartCoroutine("HitColor");
    }

    IEnumerator HitColor() {
        rd.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rd.material.color = Color.white;
    }

    IEnumerator WaitForRespawn() {
        GetComponent<EnemyBehaviour>().CanStartFollow = false;
        yield return new WaitForSeconds(respawnTime);
        GetComponent<EnemyBehaviour>().CanStartFollow = true;
        hasBeenKilled = false;
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
    }
}
