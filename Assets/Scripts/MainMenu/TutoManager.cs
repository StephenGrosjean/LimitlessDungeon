using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> firstPanel, secondPanel, thirdPanel;
    [SerializeField] private GameObject helpMenu;

    private int panelID;

    public void NextPanel() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);

        panelID++;
        if(panelID > 2) {
            panelID = 0;
        }

        switch (panelID) {
            case 0:
                ToggleFirstPanel(true);
                ToggleSecondPanel(false);
                ToggleThirdPanel(false);
                break;

            case 1:
                ToggleFirstPanel(false);
                ToggleSecondPanel(true);
                ToggleThirdPanel(false);
                break;

            case 2:
                ToggleFirstPanel(false);
                ToggleSecondPanel(false);
                ToggleThirdPanel(true);
                break;

        }
    }

    void ToggleFirstPanel(bool activate) {
        foreach (GameObject g in firstPanel) {
            g.SetActive(activate);
        }
    }

    void ToggleSecondPanel(bool activate) {
        foreach (GameObject g in secondPanel) {
            g.SetActive(activate);
        }
    }

    void ToggleThirdPanel(bool activate) {
        foreach (GameObject g in thirdPanel) {
            g.SetActive(activate);
        }
    }

    public void ToggleHelp() {
        SoundManager.instance.PlaySound(SoundManager.sound.Button_Click);
        helpMenu.SetActive(!helpMenu.activeInHierarchy);
    }
}
