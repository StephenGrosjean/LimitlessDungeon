using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingOpti : MonoBehaviour
{
    MeshRenderer rd;
    public LayerMask layer;
    Transform player;
    Collider col;
    GameController gc;

    private void Start() {
        rd = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        gc = GameController.instance;
    }

    private void Check() {
        if (Surrounded()) {
            rd.enabled = false;
        }
        else {
            rd.enabled = true;
        }
    }

    bool Surrounded() {
        bool up = gc.cubes[(int)transform.position.x, (int)transform.position.y+1, (int)transform.position.z] != null;
        bool down = gc.cubes[(int)transform.position.x, (int)transform.position.y-1, (int)transform.position.z] != null;

        bool left = gc.cubes[(int)transform.position.x+1, (int)transform.position.y, (int)transform.position.z] != null;
        bool right = gc.cubes[(int)transform.position.x-1, (int)transform.position.y, (int)transform.position.z] != null;

        bool front = gc.cubes[(int)transform.position.x, (int)transform.position.y, (int)transform.position.z+1] != null;
        bool back = gc.cubes[(int)transform.position.x, (int)transform.position.y, (int)transform.position.z-1] != null;

        return (up && down && left && right && front && back);
    }
}
