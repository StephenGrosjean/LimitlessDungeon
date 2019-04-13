using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Information : MonoBehaviour
{
    private int goldRequired, ironRequired, copperRequired;
    private int goldInside, ironInside, copperInside;

    [SerializeField] private CraftingRequirement craftingRequirement;
    [SerializeField] private PortalProperty portalProperty;
    [SerializeField] private TextMeshProUGUI textGold, textIron, textCopper;
    private bool canCheck;
    private void Start() {
        Invoke("LateStart",2f);
    }

    void LateStart()
    {
        portalProperty = GameObject.FindGameObjectWithTag("Portal").GetComponent<PortalProperty>();
        canCheck = true;
    }

    void Update()
    {
        if (canCheck) {
            textGold.text = "Gold [ " + portalProperty.GoldInside + " / " + craftingRequirement.GoldRequired + " ]";
            textIron.text = "Iron [ " + portalProperty.IronInside + " / " + craftingRequirement.IronRequired + " ]";
            textCopper.text = "Copper [ " + portalProperty.CopperInside + " / " + craftingRequirement.CopperRequired + " ]";
        }

    }


}
