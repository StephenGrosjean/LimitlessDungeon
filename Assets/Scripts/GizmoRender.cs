using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRender : MonoBehaviour
{
    [SerializeField] private bool sphere, cube;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (sphere) {
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
        if (cube) {
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}
