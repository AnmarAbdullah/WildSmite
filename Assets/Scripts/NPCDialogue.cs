using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject Icon;
    PlayerController player;
    void Start()
    {
        GameObject gb = GameObject.Find("Canvas");
        player = FindObjectOfType<PlayerController>();
        //Icon.text = GetComponentInChildren<RawImage>().texture;
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Icon = GameObject.Find("Icon");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
