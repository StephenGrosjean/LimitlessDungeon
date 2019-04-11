using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Image damageUI;

    [SerializeField] private int life;
    public int Life { set { life = value; } }
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0) {
            Debug.Log("PlayerDEAD");
        }
    }

    public void Hit() {
        life--;
        
    }

}
