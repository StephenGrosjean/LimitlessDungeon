using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGeneration : MonoBehaviour
{
    [SerializeField] private Transform navigationPoint;

    //Node Class
    public class Node {
        public string nodeName;
        public Vector2 pos;
        public List<Node> neighbors = new List<Node>();
        public bool isWaypoint;
        public Node cameFrom;
        public bool isPath;
        public bool hasBeenVisited;
        public float cost = 0;
        public float baseModifier = 1;
        public bool isWalkable = true;

    }

    public Node[,] nodes;

    public void GenerateNodes(CellularAutomata.Cell[,] level, int size) {
        nodes = new Node[size, size];

        for(int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                nodes[x, y] = new Node();
            }
        }

        int pointIndex = 0; //Used to set a name to the navPoint
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (level[x, y].isAlive) {
                   Transform navPoint = Instantiate(navigationPoint, new Vector3(level[x,y].position.x,4, level[x, y].position.y), Quaternion.identity);
                    navPoint.GetComponent<NavPoint>().IsNearWall = level[x, y].isNearWall;
                    navPoint.name = "NavPoint_" + pointIndex.ToString();
                    nodes[x, y].nodeName = navPoint.name;
                    if (level[x, y].isNearWall) {
                        nodes[x, y].baseModifier = 2;
                    }
                    pointIndex++;
                }
            }
        }


    }

    public void AddPoint(Transform point, List<Transform> neighbors) {
        Node node = nodes[(int)point.position.x, (int)point.position.z];
        node.pos = new Vector2(point.position.x, point.position.z);
        foreach(Transform t in neighbors) {
            Node neighbor = nodes[(int)t.position.x, (int)t.position.z];
            node.neighbors.Add(neighbor);
        }
        node.isWaypoint = true;
    }
}
