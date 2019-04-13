using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject smokeParticles;
    [SerializeField] private float respawnTime;

    private Vector3 backPosition;
    public Vector3 BackPosition { set { backPosition = value; } }

    private bool hasBeenKilled;
    private int startLife;
    // Start is called before the first frame update
    void Start()
    {
        startLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0 && !hasBeenKilled) {
            hasBeenKilled = true;
            Instantiate(smokeParticles, transform.position, Quaternion.identity);
            transform.position = backPosition;
            life = startLife;
            StartCoroutine("WaitForRespawn");
            
        }

        renderer.enabled = !hasBeenKilled;
    }

    public void Hit() {
        life--;
        StartCoroutine("HitColor");
        Debug.Log("Hit : " + life);
    }

    IEnumerator HitColor() {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = Color.white;

    }

    IEnumerator WaitForRespawn() {
        GetComponent<EnemyBehaviour>().CanStartFollow = false;
        yield return new WaitForSeconds(respawnTime);
        GetComponent<EnemyBehaviour>().CanStartFollow = true;
        hasBeenKilled = false;
    }
}
