using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float moveSpeed, runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject hologram;
    [SerializeField] private bool canHitAgain = true;
    [SerializeField] private LayerMask digLayer;
    [SerializeField] private float selectDistance;
    [SerializeField] private Animator toolAnimator;
    private Animator animator;
    private float currentSpeed;
    private float rotationX = 0;
    private InventorySystem inventorySystem;
    private bool canHitEnemy;
    public bool CanHitEnemy { get { return canHitEnemy; } }

    private void Start() {
        rigid = GetComponent<Rigidbody>();
        animator = Camera.main.GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        inventorySystem = GetComponent<InventorySystem>();
    }

    void Update() {
        bool gamePaused = PauseMenu.instance.GamePaused;
        if (gamePaused) return;
        RaycastHit hit;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -80.0f, 80.0f);

        
        Camera.main.transform.localEulerAngles = new Vector3(-rotationX, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0)));
        rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * currentSpeed) + (transform.right * Input.GetAxis("Horizontal") * currentSpeed));

       //WALK
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);

            currentSpeed = moveSpeed;
        }
        //RUN
        else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);

            currentSpeed = runSpeed;
        }
        //STOP
        else if(Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0){
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            rigid.velocity = new Vector3(rigid.velocity.x, jumpForce, rigid.velocity.z);
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, selectDistance, digLayer)) {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.red);

            if (hit.collider.tag == "Wall") {
                hologram.transform.position = hit.collider.transform.position;
                hologram.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            //OPEN CHEST
            else if(hit.collider.tag == "Chest") {
                hit.collider.SendMessage("HitByRay");
                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.collider.GetComponent<ChestProperty>().OpenChest();
                }
            }else if(hit.collider.tag == "PortalFeeder") {
                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.collider.GetComponent<PortalFeeder>().Feed();
                }
            }
            else {
                hologram.transform.position = new Vector3(-1, -1, -1);
                hologram.GetComponent<MeshRenderer>().material.color = Color.clear;

            }

            if (Input.GetMouseButton(0)) {
                //MINE
                if (inventorySystem.PickaxeActive) {
                    toolAnimator.SetBool("isMining", true);
                    if (hit.collider.tag == "Wall") {
                        hit.collider.gameObject.GetComponent<CubeProperty>().Hit();
                        hit.collider.gameObject.GetComponent<CubeProperty>().isHitting = true;
                    }
                }
                //HIT
                else if(inventorySystem.SwordActive){
                    toolAnimator.SetBool("isHitting", true);
                    if(hit.collider.tag == "Enemy") {
                        canHitEnemy = true;
                    }
                    else {
                        canHitEnemy = false;
                    }
                }
            }
            else {
                toolAnimator.SetBool("isMining", false);
                toolAnimator.SetBool("isHitting", false);
            }

            if (Input.GetMouseButtonUp(0)) {
                toolAnimator.SetBool("isMining", false);
                toolAnimator.SetBool("isHitting", false);
            }
        }
    }


}
