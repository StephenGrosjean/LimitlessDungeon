﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ChestProperty : MonoBehaviour
{
    [SerializeField] private float timeBeforeGiveObject;
    [SerializeField] private LayerMask mask;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject smokeParticles;
    [SerializeField] private GameObject swordObject, pickaxeObject;

    [SerializeField] private InventorySystem.itemType itemType;
    public InventorySystem.itemType ItemType { set { itemType = value; } }

    private Animator animator;
    private bool isHit, isOpen;
    private GameObject player;
    private PathFinder enemy;

    private bool initialized;

    private void Start() {
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PathFinder>();
        player = GameObject.FindGameObjectWithTag("Player");

        if(itemType == InventorySystem.itemType.pickaxe) {
            pickaxeObject.SetActive(true);
        }
        else if(itemType == InventorySystem.itemType.sword){
            swordObject.SetActive(true);
        }


    }

    private void Update() {
        text.enabled = isHit;

        if (CellularAutomata.instance.FinishedGeneration && !initialized) {
            initialized = true;
            Init();
        }
    }

    void Init() {
        RaycastHit hit;
        float height = 0;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask)) {
            height = hit.point.y + 0.5f;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        //Set the cost of the node to be "Ignored" by the pathfinding 
        enemy.nodes[(int)transform.position.x, (int)transform.position.z].baseModifier = 500;
    }
    
    public void OpenChest() {
        if (!isOpen) {
           
            isOpen = true;
            animator.SetBool("opened", true);
            Invoke("GiveItem", timeBeforeGiveObject);
            Invoke("DestroyMe", 5);
        }
    }

    public void HitByRay() {
        isHit = true;
    }

    private void DestroyMe() {
        Instantiate(smokeParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator resetHit() {
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    void GiveItem() {

        player.GetComponent<InventorySystem>().AddItem(itemType, 1);
        SoundManager.instance.PlaySound(SoundManager.sound.Item_Pick);
        Invoke("DisableInsideObject", 0.5f);
    }

    void DisableInsideObject() {
        if (itemType == InventorySystem.itemType.pickaxe) {
            pickaxeObject.SetActive(false);
        }
        else {
            swordObject.SetActive(false);
        }
    }
}
