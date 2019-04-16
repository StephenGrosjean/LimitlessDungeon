using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccludedObject : MonoBehaviour
{

    [SerializeField] private float displayTime;
    [SerializeField] private MeshRenderer rendererToOcclude;
    [SerializeField] private bool disableScript;

    void OnEnable()
    {
        displayTime = -1;
    }

    void Update()
    {
        if(displayTime > 0) {
            displayTime -= Time.deltaTime;
        }
        else {
            rendererToOcclude.enabled = false;
        }
    }

    public void HitOcclude(float time) {
        if (!disableScript) {
            displayTime = time;
            rendererToOcclude.enabled = true;
        }
    }



}
