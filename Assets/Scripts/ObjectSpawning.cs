using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    [SerializeField] private GameObject torchObject, chestObject, portalObject, objectHolder;
    [Range(0,100)]
    [SerializeField] float torchRarety = 97.0f;
    private Vector2 portalSpawnCellPos;

    public void Start() {
        
    }
    public void SpawnTorches(CellularAutomata.Cell[,] map, int size) {
        for(int x = 0; x < size; x++) {
            for (int z = 0; z < size; z++) {
                if (map[x, z].isNearWall) {
                    if(Random.Range(0.0f,100.0f) > torchRarety && map[x,z].canPutTorch) {
                        GameObject torch = Instantiate(torchObject, new Vector3(map[x, z].position.x, 5, map[x, z].position.y), Quaternion.identity);
                        torch.name = "Torch";
                        Vector3 targetRot = new Vector3(map[x, z].wallPosition.x, 5, map[x, z].wallPosition.y);
                        torch.GetComponent<Torch>().StartRot(Quaternion.Euler(targetRot), targetRot);
                        torch.transform.SetParent(objectHolder.transform);
                    }
                }
            }
        }

        SpawnChests(map, size);
    }

    public void SpawnChests(CellularAutomata.Cell[,] map, int size) {
        CellularAutomata.Cell firstCell = new CellularAutomata.Cell();
        firstCell.isNearWall = true;
        firstCell.isBorder = false;
        while (firstCell.isNearWall || firstCell.isBorder || !firstCell.isAlive || firstCell.position == portalSpawnCellPos) {
            firstCell = map[Random.Range(0, size), Random.Range(0, size)];
        }

        CellularAutomata.Cell secondCell = new CellularAutomata.Cell();
        secondCell.isNearWall = true;
        secondCell.isBorder = false;
        while (secondCell.isNearWall || secondCell.isBorder || !secondCell.isAlive || secondCell.position == firstCell.position || secondCell.position == portalSpawnCellPos) {
            secondCell = map[Random.Range(0, size), Random.Range(0, size)];
        }

        GameObject firstChest = Instantiate(chestObject, new Vector3(firstCell.position.x, 2, firstCell.position.y), Quaternion.identity);
        firstChest.GetComponent<ChestProperty>().ItemType = InventorySystem.itemType.sword;

        GameObject SecondChest = Instantiate(chestObject, new Vector3(secondCell.position.x, 2, secondCell.position.y), Quaternion.identity);
        SecondChest.GetComponent<ChestProperty>().ItemType = InventorySystem.itemType.pickaxe;
    }

    public void SpawnPortal(List<CellularAutomata.Room> rooms, CellularAutomata.Cell[,] map, int size) {
        CellularAutomata.Cell spawnCell = new CellularAutomata.Cell();
        spawnCell.isNearWall = true;
        spawnCell.isBorder = false;

        CellularAutomata.Room spawnRoom = new CellularAutomata.Room();
        spawnRoom.cells = new List<CellularAutomata.Cell>();
        
        //spawnRoom = rooms[Random.Range(0, rooms.Count)];
        
        
        while(spawnCell.isNearWall || spawnCell.isBorder)
        {
            spawnCell = map[Random.Range(10, 40), Random.Range(10, 40)];
        }

        Instantiate(portalObject, new Vector3(spawnCell.position.x, 2, spawnCell.position.y), Quaternion.identity);
        cleanPortalCells(spawnCell,map);
        portalSpawnCellPos = spawnCell.position;
    }

    void cleanPortalCells(CellularAutomata.Cell cell, CellularAutomata.Cell[,] map) {

        List<CellularAutomata.Cell> cellsToClean = new List<CellularAutomata.Cell>();

        cellsToClean.Add(cell);

        for (int i = 0; i < 7; i++) {
            foreach (CellularAutomata.Cell c in cellsToClean.ToArray()) {
                List<CellularAutomata.Cell> nCells = GetComponent<CellularAutomata>().FindNeighbors(c);
                foreach (CellularAutomata.Cell ce in nCells) {
                    if (!cellsToClean.Contains(ce) && !map[ce.position.x, ce.position.y].isBorder) {
                        cellsToClean.Add(ce);
                    }
                }
            }
        }
        Debug.Log(cellsToClean.Count);

        GetComponent<CellularAutomata>().ClearPath(cellsToClean, true);
    }
}
