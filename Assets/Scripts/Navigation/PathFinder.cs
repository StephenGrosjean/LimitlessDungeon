using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform player;
    [SerializeField] private bool hideGizmo;

    private NavMeshGeneration.Node startNode, endNode;
    private bool asStart, asEnd;
    public NavMeshGeneration.Node[,] nodes;
    private NavMeshGeneration.Node currentNodeExamined;
    private bool finished;
    private bool initialized;
    
    private void OnDrawGizmos() {
        if (!asStart || !asEnd || hideGizmo) return;

        foreach (NavMeshGeneration.Node node in nodes) {
            if (node == null) continue;            

            if (node.hasBeenVisited) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(new Vector3(node.pos.x, 6, node.pos.y), Vector3.one * 0.75f);
            }

            if (node.isPath) {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(new Vector3(node.pos.x, 6, node.pos.y), Vector3.one * 0.75f);
            }

            if (!node.isPath && !node.hasBeenVisited) {
                Gizmos.color = Color.grey;
                Gizmos.DrawCube(new Vector3(node.pos.x, 6, node.pos.y), Vector3.one * 0.75f);
            }


            Gizmos.color = Color.white;
            foreach (NavMeshGeneration.Node nodeNeighbor in node.neighbors) {
                Gizmos.DrawLine(new Vector3(node.pos.x, 7, node.pos.y), new Vector3(nodeNeighbor.pos.x, 7, nodeNeighbor.pos.y));
            }

           Gizmos.color = Color.blue;
                if (node.isPath) {
                    if (node.cameFrom != null) {
                        Gizmos.DrawLine(new Vector3(node.pos.x, 7, node.pos.y), new Vector3(node.cameFrom.pos.x, 7, node.cameFrom.pos.y));
                    }
                }


            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(startNode.pos.x, 9, startNode.pos.y), Vector3.one * 0.5f);

            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(endNode.pos.x, 9, endNode.pos.y), Vector3.one * 0.5f);

            /*Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(new Vector3(currentNodeExamined.pos.x, 6, currentNodeExamined.pos.y), Vector3.one * 0.75f);*/
        }
    
    
    }
    
    private void CleanNodes() {
        foreach(NavMeshGeneration.Node node in nodes) {
            node.cameFrom = null;
            node.hasBeenVisited = false;
            node.isPath = false;
            node.cost = 0;
        }
    }

    private void Update() {
        if(CellularAutomata.instance.FinishedGeneration && !initialized) {
            initialized = true;
            Init();
        }
    }

    private void Init() {
        finished = true;
        nodes = GameObject.FindGameObjectWithTag("Generator").GetComponent<NavMeshGeneration>().nodes;
    }

    public /*IEnumerator*/ List<NavMeshGeneration.Node> FindPath() {
        if (finished) {
            finished = false;
            Debug.Log("#PATHFINDING# Searching path");

            CleanNodes();

            startNode = FindNearestNode();
            endNode = FindPlayerNearestNode();

            List<NavMeshGeneration.Node> openList = new List<NavMeshGeneration.Node> { startNode };
            List<NavMeshGeneration.Node> closedList = new List<NavMeshGeneration.Node>();
            List<NavMeshGeneration.Node> path = new List<NavMeshGeneration.Node>();
            int failsafe = 0;

            while (openList.Count > 0 && ++failsafe < 3000) {
                //yield return null;
                openList = openList.OrderBy(x => x.cost).ToList();
                NavMeshGeneration.Node currentNode = openList[0];
                openList.RemoveAt(0);

                currentNode.hasBeenVisited = true;
                closedList.Add(currentNode);

                if (currentNode.pos == endNode.pos) {
                    break;
                }
                else {
                    foreach (NavMeshGeneration.Node nb in currentNode.neighbors) {
                        float modifier = 1;
                        if(currentNode.pos.x == nb.pos.x || currentNode.pos.y == nb.pos.y) {
                            modifier = 1 * nb.baseModifier;
                        }
                        else {
                            modifier = 2 * nb.baseModifier;
                        }

                        float newCost = 2*Vector2.Distance(currentNode.pos, startNode.pos) + 2*Vector2.Distance(nb.pos, endNode.pos) + modifier;
                        if (closedList.Contains(nb) || openList.Contains(nb)) {
                            continue;
                        }

                        if (nb.cost == 0 || nb.cost > newCost) {
                            nb.cost = newCost;
                            currentNodeExamined = nb;
                            nb.cameFrom = currentNode;
                            openList.Add(nb);
                        }
                    }
                }



            }

            if (failsafe >= 3000) {
                Debug.Log("#PATHFINDING# OOPS");
            }

            {
                NavMeshGeneration.Node currentNode = endNode;
                while (currentNode.cameFrom != null) {
                    currentNode.isPath = true;
                    path.Add(currentNode);
                    currentNode = currentNode.cameFrom;
                }
                path.Add(currentNode);
                currentNode.isPath = true;
            }


            finished = true;
            return path;
        }
        return null;
    }

    NavMeshGeneration.Node FindNearestNode() {
        Collider[] nb = Physics.OverlapSphere(transform.position, 5, mask);
        Transform closestPoint = null;

        float closestDistance = 100;
        foreach (Collider col in nb) {
                float dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
                if (dist < closestDistance) {
                closestPoint = col.gameObject.transform;
                    closestDistance = dist;
                }
            
        }
        asStart = true;

        return nodes[(int)closestPoint.position.x, (int)closestPoint.position.z];
    }

    NavMeshGeneration.Node FindPlayerNearestNode() {
        Collider[] nb = Physics.OverlapSphere(player.position, 20, mask);

        Transform closestPoint = null;

        float closestDistance = 100;
        foreach (Collider col in nb) {
            float dist = Vector3.Distance(col.gameObject.transform.position, player.position);
            if (dist < closestDistance) {
                closestPoint = col.gameObject.transform;
                closestDistance = dist;
            }

        }

        asEnd = true;

        return nodes[(int)closestPoint.position.x, (int)closestPoint.position.z];
    }
}
