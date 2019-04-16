using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMapGenerator : MonoBehaviour
{
    [SerializeField] private int percentageGoldOre, percentageCopperOre, percentageIronOre;
    [SerializeField] private GameObject ground, wall, goldOre, copperOre, ironOre;

    [SerializeField] private GameController gameController;

    public void Generate(CellularAutomata.Cell[,] world, int size, Texture2D noise) {
        for (int x = 0; x < size; x++) {
            for (int z = 0; z < size; z++) {

                Color heightColor = noise.GetPixel(x, z);

                if (world[x, z].isAlive) {
                    float height = heightColor.grayscale * 1.2f;
                    GameObject cube = Instantiate(ground, new Vector3(x, height, z), Quaternion.identity);
                    gameController.cubes[x, 0, z] = cube;
                }
                else {
                    for (int i = 0; i < 10; i++) {
                        GameObject cube;

                        if(world[x,z].neighborsNumber > 0 && i <= 4 && i > 1 && !world[x,z].isBorder) {
                            int RDMN = Random.Range(0, 100);

                            if(RDMN < percentageGoldOre) {
                                cube = Instantiate(goldOre, new Vector3(x, i, z), Quaternion.identity);
                                cube.GetComponent<CubeProperty>().itemType = InventorySystem.itemType.gold;
                            }
                            else if(RDMN < percentageCopperOre) {
                                cube = Instantiate(copperOre, new Vector3(x, i, z), Quaternion.identity);
                                cube.GetComponent<CubeProperty>().itemType = InventorySystem.itemType.copper;
                            }
                            else if(RDMN < percentageIronOre) {
                                cube = Instantiate(ironOre, new Vector3(x, i, z), Quaternion.identity);
                                cube.GetComponent<CubeProperty>().itemType = InventorySystem.itemType.iron;
                            }
                            else {
                                cube = Instantiate(wall, new Vector3(x, i, z), Quaternion.identity);
                                cube.GetComponent<CubeProperty>().itemType = InventorySystem.itemType.none;
                            }
                            
                        }
                        else {
                            cube = Instantiate(wall, new Vector3(x, i, z), Quaternion.identity);
                            cube.GetComponent<CubeProperty>().itemType = InventorySystem.itemType.none;
                        }

                        if (!world[x,z].isBorder && i >= 1) {
                            if(i > 4) {
                                cube.GetComponent<CubeProperty>().isInvincible = true;
                            }
                        }
                        else if(world[x,z].isBorder || i <= 1){
                            cube.GetComponent<CubeProperty>().isInvincible = true;
                        }
                        gameController.cubes[x, i ,z] = cube;
                    }
                }
            }
        }

        gameController.canClean = true;

        //Generate NavMap
        GetComponent<NavMeshGeneration>().GenerateNodes(world, size);
    }

}
