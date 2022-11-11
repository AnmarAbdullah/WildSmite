using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    CharacterController controller;
    PlayerController playerController;
    public float speed;
    public float health;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = playerController.transform.position - transform.position;
        movement.y = -9.89f;
        controller.Move(movement.normalized * speed * Time.deltaTime);

        if(health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= 200;
        }
    }
}
