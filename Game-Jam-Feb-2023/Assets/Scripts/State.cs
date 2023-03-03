using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class State : MonoBehaviour
{
    public string dialogue;
    public float dialogueDistance;
    Text dialogueBox;
    protected float dialogueTimer = 5f;
    protected bool dialogueShown = false;
    public abstract State Tick(EnemyManager enemyManager);
    public abstract void HandleDialogue(EnemyManager enemyManager);

    private void Awake()
    {
        dialogueBox = this.transform.parent.GetComponentInChildren<Text>();
        HideDialogue();
    }
    public float HandleMoveDirection(string moveDirection)
    {
        float moveDirectionModifier;
        switch (moveDirection)
        {
            case "Left":
                moveDirectionModifier = -1f;
                break;
            case "Right":
                moveDirectionModifier = 1f;
                break;
            default:
                moveDirectionModifier = 0f;
                break;
        }

        return moveDirectionModifier;
    }

    public void ShowDialogue()
    {
        if(dialogueBox != null)
        {
            dialogueBox.enabled = true;
            dialogueBox.text = dialogue;
            dialogueShown = true;
        }
    }

    public void HideDialogue()
    {
        if(dialogueBox != null)
        {
            dialogueBox.enabled = false;
        }
    }

    public void MoveDialogue(Transform dialogueTransform)
    {
        if (dialogueBox != null)
        {
            dialogueBox.rectTransform.position = Camera.main.WorldToScreenPoint(dialogueTransform.position);
        }
    }
}
