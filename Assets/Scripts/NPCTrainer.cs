using System;
using UnityEngine;
using TMPro;

public class NPCTrainer : Interactable
{
    public string name;
    public int difficulty = 1; //this will be on a 1-10 scale
    public Array pocketMenTeam; //array of pocket men as objects
    public string[] dialogue;


   public void Interact()
   {
        //show dialogue
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            UIManager.Instance.NextDialogueLine();
        }
    }

}
