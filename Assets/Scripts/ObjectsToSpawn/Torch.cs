using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool hideGizmo;
    private Vector3 pos;
    private bool canRotate = true;

    private void OnDrawGizmos() {
        if (hideGizmo) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos, Vector3.one);
    }

    private void Start() {
        Invoke("StopRotation", 1);
    }

    public void StartRot(Quaternion rotation, Vector3 position) {
        pos = position;
    }

    private void Update() {
        if (canRotate) {
            transform.LookAt(pos);
        }
    }

    void StopRotation() {
        canRotate = false;
    }
}
