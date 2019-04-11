using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    /// <summary>
    /// INFO: 
    /// Slot 0 -> pickaxe -1
    /// Slot 1 -> sword   -2
    /// Slot 2 -> iron    -3
    /// Slot 3 -> copper  -4 
    /// Slot 4 -> gold    -5
    /// Slot 5 ->         -6
    /// </summary>

    private static int inventorySize = 6;
    [SerializeField] private Sprite goldSprite, ironSprite, copperSprite, pickaxeSprite, swordSprite, emptySprite;
    public enum itemType { none, gold, iron, copper, pickaxe, sword};

    [SerializeField] private GameObject pickaxeObject, swordObject;

    struct Slot {
        public itemType type;
        public int number;
    }

    private Slot[] inventory = new Slot[inventorySize];
    [SerializeField] private Image[] slots;
    [SerializeField] private TextMeshProUGUI[] itemNumber;
    [SerializeField] private bool swordActive;
    public bool SwordActive { get { return swordActive; } }

    [SerializeField] private bool pickaxeActive;
    public bool PickaxeActive { get { return pickaxeActive; } }

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < inventorySize; i++) {
            if (inventory[i].number != 0) {
                switch (inventory[i].type) {
                    case itemType.gold:
                        slots[i].sprite = goldSprite;
                        break;
                    case itemType.iron:
                        slots[i].sprite = ironSprite;
                        break;
                    case itemType.copper:
                        slots[i].sprite = copperSprite;
                        break;
                    case itemType.pickaxe:
                        slots[i].sprite = pickaxeSprite;
                        break;
                    case itemType.sword:
                        slots[i].sprite = swordSprite;
                        break;
                    case itemType.none:
                        slots[i].sprite = emptySprite;
                        break;

                    default:
                        slots[i].sprite = emptySprite;
                        break;

                }
                itemNumber[i].text = inventory[i].number.ToString();

            }
            else {
                itemNumber[i].text = " ";
                slots[i].sprite = emptySprite;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //Select pickaxe
            if(inventory[0].number != 0) {
                pickaxeObject.SetActive(true);
                swordObject.SetActive(false);
                pickaxeActive = true;
                swordActive = false;

            }
            else {
                swordObject.SetActive(false);
                swordActive = false;
            }
        }
        //Select sword
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (inventory[1].number != 0) {
                pickaxeObject.SetActive(false);
                swordObject.SetActive(true);
                pickaxeActive = false;
                swordActive = true;
            }
            else {
                pickaxeObject.SetActive(false);
                pickaxeActive = false;
            }
        }
    }

    public void AddItem(itemType type, int number) {
        switch (type) {
            case itemType.pickaxe:
                inventory[0].type = itemType.pickaxe;
                inventory[0].number += number;
                break;

            case itemType.sword:
                inventory[1].type = itemType.sword;
                inventory[1].number += number ;
                break;
 
            case itemType.iron:
                inventory[2].type = itemType.iron;
                inventory[2].number += number;
                break;

            case itemType.copper:
                inventory[3].type = itemType.copper;
                inventory[3].number += number;
                break;
            case itemType.gold:
                inventory[4].type = itemType.gold;
                inventory[4].number += number;
                break;
        }
    }

    public void RemoveItem(itemType type, int number) {
        switch (type) {
            case itemType.pickaxe:
                inventory[0].type = itemType.pickaxe;
                inventory[0].number -= number;
                break;

            case itemType.sword:
                inventory[1].type = itemType.sword;
                inventory[1].number -= number;
                break;

            case itemType.iron:
                inventory[2].type = itemType.iron;
                inventory[2].number -= number;
                break;

            case itemType.copper:
                inventory[3].type = itemType.copper;
                inventory[3].number -= number;
                break;
            case itemType.gold:
                inventory[4].type = itemType.gold;
                inventory[4].number -= number;
                break;
        }
    }

    public int CheckItem(int slotNumber) {
        return inventory[slotNumber].number;
    }

}
