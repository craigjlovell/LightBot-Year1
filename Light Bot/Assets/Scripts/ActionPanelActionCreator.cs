using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanelActionCreator : MonoBehaviour
{

    public PlayerController player;

    public GameObject forwardDrag;
    public GameObject rotLeftDrag;
    public GameObject rotRightDrag;
    public GameObject lightDrag;
    public GameObject jumpDrag;

    // Start is called before the first frame update
    void Start()
    {
        int nextChildIndex = 0;
        var children = GetComponentsInChildren<EquipmentSlot>();
        foreach(var action in player.actions)
        {
            if (nextChildIndex >= children.Length)
                break;
            var child = children[nextChildIndex];
            DragCommand cmd = null;
            switch(action)
            {
                case PlayerController.ActionType.MOVE_FORWARD: Instantiate(forwardDrag, child.transform); break;
                case PlayerController.ActionType.ROT_LEFT: Instantiate(rotLeftDrag, child.transform); break;
                case PlayerController.ActionType.ROT_RIGHT: Instantiate(rotRightDrag, child.transform); break;
                case PlayerController.ActionType.TOGGLE_LIGHT: Instantiate(lightDrag, child.transform); break;
                case PlayerController.ActionType.JUMP: Instantiate(jumpDrag, child.transform); break;
            }

            nextChildIndex++;
        }

    }

    public void UpdatePlayerActionList()
    {
        player.actions.Clear();
        var childDraggableCommand = GetComponentsInChildren<DragCommand>();
        foreach(var cmd in childDraggableCommand)
        {
            player.actions.Add(cmd.actionType);
        }
    }
}
