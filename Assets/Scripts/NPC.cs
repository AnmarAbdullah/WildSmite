using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    /*public string[] texts;
    public TextMeshProUGUI Text;
    public Texture Icon;
    [SerializeField] int index;
    PlayerController player;
    [SerializeField] NPCDialogue npcDialogue;
    bool TalkedTo;
    float timer;
    //public GameObject DialogueUI;

    void Start()
    {
        npcDialogue = FindObjectOfType<NPCDialogue>();
        player = FindObjectOfType<PlayerController>();
        GameObject gb = GameObject.Find("Canvas");
        player = FindObjectOfType<PlayerController>();
        GameObject dialogue = gb.transform.GetChild(0).gameObject;
        //Icon = dialogue.transform.GetChild(1).gameObject.GetComponent<Texture>();
        Text = dialogue.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        npcDialogue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.currentNPC == this & player.inDialogue & !TalkedTo)
        {
            npcDialogue.gameObject.SetActive(true);

            npcDialogue.Icon.GetComponent<RawImage>().texture = Icon;
           // Icon = npcDialogue.Icon;
            npcDialogue.text.text = texts[index];
            //texts[index] = npcDialogue.text.text;

            if (Input.GetKeyDown(KeyCode.E))
            {
                index++;
                if(index >= texts.Length)
                {
                    //npcDialogue.gameObject.SetActive(false);
                    player.inDialogue = false;
                    index = 0;
                    TalkedTo = true;
                }
            }
        }
        if (TalkedTo)
        {
            npcDialogue.gameObject.SetActive(false);
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                TalkedTo = false;
                timer = 0;
            }
        }
    }*/
}
