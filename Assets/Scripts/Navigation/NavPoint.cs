using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    [SerializeField] private LayerMask mask, enemyMask;
    [SerializeField] private Vector3 boxsize;

    private bool isNearWall;
    public bool IsNearWall {  get { return isNearWall; } set { isNearWall = value; } }
    public List<Transform> neighbors = new List<Transform>();
    private Transform player, enemy;
    private bool called, isInside;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        Invoke("FindNeighbors", 1);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, boxsize);
    }

    void FindNeighbors() {
        float range = 1;
        if (isNearWall) {
            range = 1;
        }
        else {
            range = 1.5f;
        }

        Collider[] nb = Physics.OverlapSphere(transform.position, range, mask);
        foreach(Collider col in nb) {
            if (col.transform != this.transform) {
                neighbors.Add(col.transform);
            }
        }
        AddPoint();
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Enemy") {
            if (enemy.gameObject.GetComponent<EnemyBehaviour>().CurrentNode.pos == new Vector2(transform.position.x, transform.position.z)) {
                enemy.gameObject.GetComponent<EnemyBehaviour>().NextNode();
            }
        }
    }

    void AddPoint() {
        GameObject.FindGameObjectWithTag("Generator").GetComponent<NavMeshGeneration>().AddPoint(transform, neighbors);
    }

}
