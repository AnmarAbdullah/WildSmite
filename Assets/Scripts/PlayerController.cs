using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float playerSpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    Vector3 velocity;
    [SerializeField] GameObject sword;
    /*[SerializeField] float sphereRadius;
    [SerializeField] float AOERadius;*/
    [SerializeField] bool Hitting;
    [SerializeField] float timer;
    Animator animator;
    [SerializeField] bool isAttacking;
    [SerializeField] float gravity;
    [SerializeField] GameObject bullet;
    //[SerializeField] public bool inDialogue;
    //[SerializeField] public NPC currentNPC;
    float currentspeed;
    [SerializeField] bool isGrounded;
    //[SerializeField] public NPCDialogue DialogueUI;
    //[SerializeField] GameObject gun;
    [SerializeField]GameObject Companion;
    [SerializeField] float OffsetX;
    [SerializeField] float OffsetY;
    [SerializeField] float OffsetZ;
    [SerializeField] float OffsetXRot;
    [SerializeField] bool shooting;

    bool DamagingEnemies;
    [SerializeField]float swordRadius = 4;


    float comboDelay = 1;
    bool inDelay;
    [SerializeField]int numbOfClicks;
    float lastClickedTime;
    [SerializeField]float shootDelay;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        currentspeed = playerSpeed;
        animator = GetComponent<Animator>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire2"))
        {
            GameObject bullets = Instantiate(bullet, transform.position,Quaternion.identity);
            bullets.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }*/
        Companion.transform.position = new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY, transform.position.z + OffsetZ);
        //Companion.transform.LookAt(cam.transform.forward);
        //Companion.transform.rotation = Camera.main.transform.rotation;
        Companion.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x + OffsetXRot, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z);
        // Companion.transform.position = new Vector3(/*cam.position.x + OffsetX*/Companion.transform.position.x, Companion.transform.position.y, cam.position.z + OffsetZ /*cam.position.y + OffsetY, cam.position.z + OffsetZ*/);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.3f);
        if(direction.magnitude >= 0.1f)
        {
            if (!isAttacking)
            {
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
            }
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }
        else if(!isAttacking)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetBool("Attack1", false);
            isAttacking = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetBool("Attack2", false);
            isAttacking = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit3") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetBool("Attack3", false);
            isAttacking = false;
            numbOfClicks = 0;
        }

        if (isAttacking)
        {
            lastClickedTime += Time.deltaTime;
            if (lastClickedTime > 0.8f)
            {
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", false);
                numbOfClicks = 0;
                lastClickedTime = 0;
                isAttacking = false;
            }
        }
        /*if(numbOfClicks >= 3)
        {
            isAttacking = false;
            inDelay = true;
            lastClickedTime = 0;
            numbOfClicks = 0;
        }*/

        if (shooting)
        {
            shootDelay += Time.deltaTime;
            if(shootDelay >= 0.1f)
            {
                shooting = false;
                shootDelay = 0; 
            }
        }
        if (Input.GetButton("Fire2") && !shooting)
        {
            Rigidbody bullets = Instantiate(bullet, Companion.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bullets.AddForce(Companion.transform.forward * 200, ForceMode.Impulse);
            bullets.AddForce(Companion.transform.up * 15, ForceMode.Impulse);
            shooting = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AreaofEffect();
        }

        if (DamagingEnemies)
        {
            StartCoroutine(Damaging());
        }
        if (inDelay)
        {
            comboDelay += Time.deltaTime;
            if(comboDelay >= 2)
            {
                inDelay = false;
            }
        }else comboDelay = 0;

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
            isAttacking = true;
            numbOfClicks++;

            if (numbOfClicks == 1)
            {
                animator.SetBool("Attack1", true);
            }
            //numbOfClicks = Mathf.Clamp(numbOfClicks, 0, 3);
            if (numbOfClicks >= 2 & animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f)
            {
                //print("Anim Ended");
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", true);
                // playerSpeed = currentspeed;
                //isAttacking = false;
            }
            if (numbOfClicks >= 3 & animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            {
                animator.SetBool("Attack3", true);
                animator.SetBool("Attack2", false);
                isAttacking = false;
                inDelay = true;
                lastClickedTime = 0;
                numbOfClicks = 0;
            }
            //onclick();
        }


        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
         controller.Move(velocity * Time.deltaTime);
        /*if (Input.GetKeyDown(KeyCode.E) & !inDialogue)
        {
            print("NPC Nearby");
            Collider[] hits;
            hits = Physics.OverlapSphere(transform.position, 3);
            foreach (Collider c in hits)
            {
                if (c.GetComponent<NPC>() != null)
                {
                    NPC npc = c.GetComponent<NPC>();
                    currentNPC = npc;
                    inDialogue = true;
                   // DialogueUI.gameObject.SetActive(true);
                }
            }
        }*/

        if (Input.GetButtonDown("Fire1") & !Hitting)
        {
            /*Collider[] hits;
            Hitting = true;
            Debug.Log("Hitting");
            hits = Physics.OverlapSphere(transform.position, sphereRadius);
            foreach(Collider c in hits)
            {
                if(c.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = c.GetComponent<Enemy>();
                    enemy.health -= 10;
                    Debug.Log("enemy hit");   
                }
            }*/
        }
        if (Hitting)
        {
            /*sword.gameObject.SetActive(true);
            timer += Time.deltaTime;
            if(timer > 0.1f)
            {
                Hitting = false;
                timer = 0;
            }*/
        }
        else
        {
            //sword.gameObject.SetActive(false);
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            AreaofEffect();
        }
        if (inDialogue)
        {
            playerSpeed = currentspeed;

        }*/
    }

    /*void onclick()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Idle", false);
        isAttacking = true;
        lastClickedTime += Time.deltaTime;
        numbOfClicks++;

        if (numbOfClicks == 1)
        {
            animator.SetBool("Attack1", true);
        }
        //numbOfClicks = Mathf.Clamp(numbOfClicks, 0, 3);
        if (numbOfClicks >= 2 & animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            //print("Anim Ended");
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
            // playerSpeed = currentspeed;
            //isAttacking = false;
        }
        if (numbOfClicks >= 3 & animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") & animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", true);
            isAttacking = false;
            inDelay = true;
            lastClickedTime = 0;
            numbOfClicks = 0;
        }
    }*/
    void AreaofEffect()
    {
        Collider[] hits;
        Hitting = true;
        Debug.Log("Hitting");
        hits = Physics.OverlapSphere(sword.transform.position, 8);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<Enemy>() != null)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                enemy.health = 0;
                Debug.Log("enemy hit");
            }  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(sword.transform.position, sphereRadius);
        //Gizmos.DrawWireSphere(transform.position, AOERadius);
    }

    void DamageEnemy()
    {
        DamagingEnemies = true;
    }

    IEnumerator Damaging()
    {
        Debug.Log("DAMAGING!!");
        Collider[] hits;
        Hitting = true;
        Debug.Log("Hitting");
        hits = Physics.OverlapSphere(sword.transform.position, swordRadius);
        foreach (Collider c in hits)
        {
            if (c.GetComponent<Enemy>() != null)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                enemy.health -= 10;
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    void unDamageEnemy()
    {
        DamagingEnemies = false;
    }
}
