using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private int levelID = 1;
    public int LevelID { get { return levelID; } }


    //SINGLETON//
    public static LevelCounter instance;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    //END SINGLETON//

    public void ResetCounter() {
        levelID = 1;
    }

    public void DestroyCounter() {
        Destroy(this.gameObject);
    }

    public void NextLevel() {
        levelID++;
    }
}
