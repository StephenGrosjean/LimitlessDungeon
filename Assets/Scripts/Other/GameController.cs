using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private TextMeshProUGUI levelIDText;
    [SerializeField] private GameObject player, enemy;
    [Range(10, 100)][SerializeField] private float maxRenderDistance = 10;
    public GameObject[,,] cubes;
    public CellularAutomata cellularAutomata;
    public bool canClean;

    //SINGLETON//
    public static GameController instance;

    private void Awake() {
        instance = this;
        Init();
    }
    //END SINGLETON//

    private void Init() {
        Time.timeScale = 1;
        cubes = new GameObject[cellularAutomata.size, 10, cellularAutomata.size];
        levelIDText.text = "Dungeon Level \n - " + LevelCounter.instance.LevelID.ToString();
        StartCoroutine("FadeToWhite");
    }

    public void SpawnPlayer(Vector2 position) {
        player.transform.position = new Vector3(position.x+0.5f,5, position.y +0.5f);
        player.SetActive(true);
    }

    public void SpawnEnemy(Vector2 position) {
        enemy.transform.position = new Vector3(position.x + 0.5f, 5, position.y + 0.5f);
        enemy.GetComponent<EnemyLife>().BackPosition = new Vector3(position.x, 5, position.y);
        enemy.SetActive(true);
    }

    IEnumerator FadeToBlack() {
        float alpha = 0.0f;
        while (alpha <= 1.1f) {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
            alpha += 0.1f;
        }
    }

    IEnumerator FadeToWhite() {
        float alpha = 1.0f;
        while (alpha >= -0.1f) {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
            alpha -= 0.1f;
        }
    }
    private void Update() {
        /*if (canClean) {
            for (int x = 0; x < cellularAutomata.size; x++) {
                for (int z = 0; z < cellularAutomata.size; z++) {
                    for (int y = 0; y < 10; y++) {
                        if (cubes[x, y, z] != null) {
                            MeshRenderer rd = cubes[x, y, z].GetComponent<MeshRenderer>();
                            BoxCollider col = cubes[x, y, z].GetComponent<BoxCollider>();
                            float distance = Vector3.Distance(cubes[x, y, z].transform.position, player.transform.position);
                            if (distance > maxRenderDistance) {
                                rd.enabled = false;
                                col.enabled = false;

                            }
                            else {
                                rd.enabled = true;
                                col.enabled = true;

                            }
                        }
                    }

                }
            }
        }*/
    }

}
