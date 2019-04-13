using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalProperty : MonoBehaviour
{
    private PathFinder enemy;
    private InventorySystem inventorySystem;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject portal;
    [SerializeField] private List<TextMeshProUGUI> goldTexts, copperTexts, ironTexts;
    [SerializeField] private GameObject goldKey, copperKey, ironKey;
    [SerializeField] private List<TextActivator> goldTextActivators, ironTextActivators, copperTextActivators;
    private CraftingRequirement requirements;
    private int goldInside, ironInside, copperInside;
    public int GoldInside { get { return goldInside; } }
    public int IronInside { get { return ironInside; } }
    public int CopperInside { get { return copperInside; } }

    private bool goldKeyActived, ironKeyActived, copperKeyActived;


    // Start is called before the first frame update
    void Start()
    {
        requirements = GameObject.FindGameObjectWithTag("GameController").GetComponent<CraftingRequirement>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PathFinder>();
        inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();

        Invoke("LateStart", 3f);

    }


    void LateStart() {

        UpdateTexts();

        RaycastHit hit;
        float height = 0;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask)) {


            height = hit.point.y - 0.1f;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        //Set the cost of the node to be "Ignored" by the pathfinding 
        enemy.nodes[(int)transform.position.x, (int)transform.position.z].baseModifier = 500;
        enemy.nodes[(int)transform.position.x+1, (int)transform.position.z].baseModifier = 500;
        enemy.nodes[(int)transform.position.x+2, (int)transform.position.z].baseModifier = 500;
        enemy.nodes[(int)transform.position.x-1, (int)transform.position.z].baseModifier = 500;
        enemy.nodes[(int)transform.position.x-2, (int)transform.position.z].baseModifier = 500;
    }

    private void UpdateActivatorsText() {
        if (goldKeyActived) {
            foreach (TextActivator text in goldTextActivators) {
                if (text.CanEnable) {
                    text.CanEnable = false;
                }
            }
        }

        if (ironKeyActived) {
            foreach (TextActivator text in ironTextActivators) {
                if (text.CanEnable) {
                    text.CanEnable = false;
                }
            }
        }

        if (copperKeyActived) {
            foreach (TextActivator text in copperTextActivators) {
                if (text.CanEnable) {
                    text.CanEnable = false;
                }
            }
        }

        if(copperKeyActived && ironKeyActived && goldKeyActived) {
            if (!portal.activeInHierarchy) {
                portal.SetActive(true);
            }
        }
    }

    public void FeedGold() {
        if (inventorySystem.CheckItem(4) != 0 && goldInside != requirements.GoldRequired) {
            goldInside += inventorySystem.CheckItem(4);
            inventorySystem.RemoveItem(InventorySystem.itemType.gold, inventorySystem.CheckItem(4));
            if (goldInside > requirements.GoldRequired) {
                goldInside = requirements.GoldRequired;
            }
            UpdateTexts();
            if (goldInside >= requirements.GoldRequired) {
                goldKey.SetActive(true);
                goldKeyActived = true;
            }
            UpdateActivatorsText();
        }
    }

    public void FeedIron() {
        if (inventorySystem.CheckItem(2) != 0 && ironInside != requirements.IronRequired) {
            ironInside += inventorySystem.CheckItem(2);
            inventorySystem.RemoveItem(InventorySystem.itemType.iron, inventorySystem.CheckItem(2));

            if(ironInside > requirements.IronRequired) {
                ironInside = requirements.IronRequired;
            }

            UpdateTexts();
            if (ironInside >= requirements.IronRequired) {
                ironKey.SetActive(true);
                ironKeyActived = true;
            }
            UpdateActivatorsText();
        }
    }

    public void FeedCopper() {
        if (inventorySystem.CheckItem(3) != 0 && copperInside != requirements.CopperRequired) {
            copperInside += inventorySystem.CheckItem(3);
            inventorySystem.RemoveItem(InventorySystem.itemType.copper, inventorySystem.CheckItem(3));

            if (copperInside > requirements.CopperRequired) {
                copperInside = requirements.CopperRequired;
            }

            UpdateTexts();
            if (copperInside >= requirements.CopperRequired) {
                copperKey.SetActive(true);
                copperKeyActived = true;
            }
            UpdateActivatorsText();
        }
    }


    void UpdateTexts() {
        foreach (TextMeshProUGUI text in goldTexts) {
            text.text = goldInside + " / " + requirements.GoldRequired;
        }
        foreach (TextMeshProUGUI text in ironTexts) {
            text.text = ironInside + " / " + requirements.IronRequired;

        }
        foreach (TextMeshProUGUI text in copperTexts) {
            text.text = copperInside + " / " + requirements.CopperRequired;
        }

    }
}
