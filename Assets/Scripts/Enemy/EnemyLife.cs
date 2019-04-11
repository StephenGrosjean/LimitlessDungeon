using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life = 100;
    [SerializeField] private Renderer renderer;

    private Vector3 backPosition;
    public Vector3 BackPosition { set { backPosition = value; } }

    private bool hasBeenKilled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0 && !hasBeenKilled) {
            hasBeenKilled = true;
            transform.position = backPosition;
            life = 100;
        }

        if (hasBeenKilled) {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);
        }
        else {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1);
        }
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
        yield return new WaitForSeconds(2);
        hasBeenKilled = false;
    }
}
