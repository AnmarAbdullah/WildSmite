using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    SpriteRenderer backGround;
    SpriteRenderer Icon;
    TextMeshPro Text;
    PlayerController player;
   

    void Awake()
    {
        backGround = transform.Find("Background").GetComponent<SpriteRenderer>();
        Icon = transform.Find("Icon").GetComponent<SpriteRenderer>();
        Text = transform.Find("Text").GetComponent<TextMeshPro>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        setup("Hello chris");
    }

    void setup(string text)
    {
        Text.text = text;
    }
}
