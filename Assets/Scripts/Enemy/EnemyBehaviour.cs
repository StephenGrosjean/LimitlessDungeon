using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public states currentState = states.followPath;
    public enum states { followPath, followPlayer };
    private states lastState;
    [SerializeField] private Animator animator;
    [SerializeField] private float nearDistance;
    [SerializeField] private float speed;
    private NavMeshGeneration.Node currentNode;
    public NavMeshGeneration.Node CurrentNode { get { return currentNode; } }

    private Transform player;
    private Rigidbody rb;
    private PathFinder pathFinder;
    private List<NavMeshGeneration.Node> path = new List<NavMeshGeneration.Node>();
    private Vector3 targetPos;
    private bool canStartFollow;
    private bool seePlayer;

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if (path.Count > 0) {
            foreach (NavMeshGeneration.Node node in path) {
                if (node.cameFrom != null) {
                    Gizmos.DrawLine(new Vector3(node.pos.x, 7, node.pos.y), new Vector3(node.cameFrom.pos.x, 7, node.cameFrom.pos.y));
                }
            }
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(targetPos, Vector3.one * 0.6f);
    }

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pathFinder = GetComponent<PathFinder>();
        Invoke("Init", 4);
    }

    void Init() {
        canStartFollow = true;
        InvokeRepeating("FindPath", 4, 10);
    }

    void FindPath() {
        //StartCoroutine(pathFinder.FindPath());
        path = pathFinder.FindPath();
        NextNode();
    }

    private void FixedUpdate() {
        lastState = currentState;


        if (canStartFollow) {
            if (path.Count > 0) {
                if (targetPos.x != 0 && targetPos.z != 0 && currentState == states.followPath) {
                    transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
            }
            else {
                FindPath();
            }

            float distance = Vector3.Distance(transform.position, player.position);
            RaycastHit hit;
            Vector3 rayDirection = player.position - transform.position;
           
            if (Physics.Raycast(transform.position, rayDirection, out hit)) {
                if (hit.transform.tag == "Player") {
                    seePlayer = true;
                    currentState = states.followPlayer;
                }
                else {
                    seePlayer = false;
                    currentState = states.followPath;
                }
            }
            

            if (currentState == states.followPlayer) {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                transform.position += transform.forward * speed * Time.deltaTime;

                
            }

            if (lastState != currentState) {
                if (lastState == states.followPlayer) {
                    StartCoroutine(LostPlayer());
                }
            }

            if(distance < 3) {
                animator.SetBool("canHit", true);
            }
            else {
                animator.SetBool("canHit", false);
            }
        }
    }


    public void NextNode() {
        if(currentState == states.followPath && canStartFollow) {
            if (path.Count > 0) {
                currentNode = path[path.Count - 1];
                targetPos = new Vector3(CurrentNode.pos.x, transform.position.y, CurrentNode.pos.y);
                path.RemoveAt(path.Count - 1);
            }
            
        }
    }


    IEnumerator LostPlayer() {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("#ENEMY BEHAVIOUR# Lost player, finding a new path");
        if (currentState == states.followPath) {
            FindPath();
        }
    }

    bool ValidatePosition(Vector3 pos1, Vector3 pos2, float allowedDistance) {
        return Vector3.Distance(pos1, pos2) < allowedDistance;
    }
}
