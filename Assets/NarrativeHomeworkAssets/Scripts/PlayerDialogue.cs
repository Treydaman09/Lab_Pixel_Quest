using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public List<string> dialogue = new List<string>();
    private bool canSpeak = false;
    private bool isSpeaking = false;
    private GameObject _talkPanel;
    private TextMeshProUGUI _talkText;
    private int _talkIndex = 0;
    public SpriteRenderer holdingObject; 
    public bool canPickUp = false;
    private GameObject pickUpItem; 

    private void Start()
    {
        _talkText = GameObject.Find(Structs.GameObjects.talkText).GetComponent<TextMeshProUGUI>();

        _talkPanel = GameObject.Find(Structs.GameObjects.talkPanel);
        _talkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpeaking && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogue.Count - 1 == _talkIndex)
            {
                isSpeaking = false;
                _talkPanel.SetActive(false);
                if (canPickUp)
                {
                    holdingObject.sprite = pickUpItem.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
                    Destroy(pickUpItem);
                    canPickUp = false;
                    return;
                }
            }
            else
            {
                _talkIndex++;
                _talkText.text = dialogue[_talkIndex];
            }
        }
        else if (canSpeak && Input.GetKeyDown(KeyCode.E))
        {
            isSpeaking = true;
            _talkPanel.SetActive(true);
            _talkIndex = 0;
            _talkText.text = dialogue[_talkIndex]; 
        }
    }

    public void SetCanSpeak(bool newCanSpeak)
    {
        canSpeak = newCanSpeak;
    }

    public bool IsSpeaking()
    {
        return isSpeaking;
    }

    public void CopyDialogue(List<string> newDialogue)
    {
        dialogue.Clear();
        dialogue.AddRange(newDialogue);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PickUp")
        {
            canPickUp = true;
            pickUpItem = collision.transform.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (collision.tag == "PickUp")
            {
                canPickUp = false;
                pickUpItem = null;
            }
        }
    }
}
