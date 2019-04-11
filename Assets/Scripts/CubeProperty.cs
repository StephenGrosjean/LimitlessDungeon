using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProperty : MonoBehaviour
{

    public int hardness = 3;
    private int startHardness;
    private bool canHit = true;
    public bool isHitting = false;
    public bool isSelected = false;

    public bool isInvincible;
    public InventorySystem.itemType itemType;

    [SerializeField] private Color tier3, tier2, tier1;
    [SerializeField] private MeshRenderer rd;

    void Start()
    {

        startHardness = hardness;

        rd.material.color = tier3;

        RandomRotation();
    }

    private void Update() {
        if (!Input.GetMouseButton(0) && isHitting && !isInvincible) {
            isHitting = false;
            hardness = startHardness;
            ChangeColor();
        }
        
    }


    public void Hit()
    {
        if (!isInvincible) {
            if (canHit && isHitting) {
                StartCoroutine(canHitAgain());
                hardness--;
            }

            if (hardness <= 0) {
                if (itemType != InventorySystem.itemType.none) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>().AddItem(itemType, 1);
                }
                Destroy(gameObject);
            }

            ChangeColor();

        }
    }

    IEnumerator canHitAgain() {
        canHit = false;
        yield return new WaitForSeconds(0.1f);
        canHit = true;
    }

    void ChangeColor() {
        if (hardness >= (startHardness / 3) * 2) {
            rd.material.color = tier3;
        }
        if (hardness == (startHardness / 3) * 2) {
            rd.material.color = tier2;

        }
        if (hardness == (startHardness / 3) * 1) {
            rd.material.color = tier1;

        }
    }

    void RandomRotation() {
        int rot = Random.Range(0, 4);

        switch (rot) {
            case 0:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
    }
}
