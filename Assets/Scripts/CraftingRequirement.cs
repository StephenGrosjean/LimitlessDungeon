using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirement : MonoBehaviour
{
    [SerializeField] private int goldRequired, ironRequired, copperRequired;
    public int GoldRequired { get { return goldRequired; } }
    public int IronRequired { get { return ironRequired; } }
    public int CopperRequired { get { return copperRequired; } }

    [SerializeField] private int goldCount, ironCount, copperCount;
    [Range(0, 100)]
    [SerializeField] private int percentageGold, percentageIron, percentageCopper;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateStart", 1);
       
    }

    void LateStart() {
        GameObject[] golds = GameObject.FindGameObjectsWithTag("GoldOre");
        GameObject[] irons = GameObject.FindGameObjectsWithTag("IronOre");
        GameObject[] coppers = GameObject.FindGameObjectsWithTag("CopperOre");

        goldCount = golds.Length;
        ironCount = irons.Length;
        copperCount = coppers.Length;

        goldRequired = Mathf.RoundToInt((goldCount * percentageGold)/100);
        ironRequired = Mathf.RoundToInt((ironCount * percentageIron) / 100);
        copperRequired = Mathf.RoundToInt((copperCount * percentageCopper) / 100);
    }
}
