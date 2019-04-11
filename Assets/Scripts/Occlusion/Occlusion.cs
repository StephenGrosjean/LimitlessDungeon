using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occlusion : MonoBehaviour
{
    [SerializeField] private int rayAmmount = 1500;
    [SerializeField] private int rayDistance = 300;
    [SerializeField] private float stayTime = 2;
    [SerializeField] private LayerMask layer;

    private Camera cam;
    private Vector2[] rPoints;

    void Start()
    {
        Application.targetFrameRate = 60;
        cam = Camera.main;
        rPoints = new Vector2[rayAmmount];
        GetPoints();
    }

    void Update()
    {
        CastRays();
    }

    void GetPoints() {
        float x = 0;
        float y = 0;
        
        for(int i = 0; i < rayAmmount; i++) {
            if(x > 1) {
                x = 0;
                y += 1 / Mathf.Sqrt(rayAmmount);
            }

            rPoints[i] = new Vector2(x, y);
            x += 1 / Mathf.Sqrt(rayAmmount);
        }
    }

    void CastRays() {
        for(int i = 0; i < rayAmmount; i++) {
            Ray ray;
            RaycastHit hit;
            OccludedObject ocl;

            ray = cam.ViewportPointToRay(new Vector2(rPoints[i].x, rPoints[i].y));
            if(Physics.Raycast(ray, out hit, rayDistance, layer)) {
                if(ocl = hit.transform.GetComponent<OccludedObject>()) {
                    ocl.HitOcclude(stayTime);
                }
            }
        }
    }
}
