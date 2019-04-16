using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    private Transform player;

    private bool canEnable = true;
    public bool CanEnable { get { return canEnable; } set { canEnable = value; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnable) {
            if (Vector2.Distance(player.position, transform.position) < 5) {
                foreach (GameObject g in objectsToEnable) {
                    if (!g.activeInHierarchy) {
                        g.SetActive(true);
                    }
                }
            }
            else {
                foreach (GameObject g in objectsToEnable) {
                    if (g.activeInHierarchy) {
                        g.SetActive(false);
                    }
                }
            }
        }
        else {
            foreach (GameObject g in objectsToEnable) {
                if (g.activeInHierarchy) {
                    g.SetActive(false);
                }
            }
        }
    }

    
}
