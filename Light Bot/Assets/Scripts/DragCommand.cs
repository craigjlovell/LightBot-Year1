using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCommand : DraggableComponent
{

    public PlayerController.ActionType actionType;

    public override void Awake()
    {
        base.Awake();
        OnEndDragHandler += DragStopped;
    }

    private void DragStopped(PointerEventData eventData, bool dropSuccess)
    {
        if (StartParent.GetComponent<EquipmentSlot>() != null && !dropSuccess)
        {
            DestroyImmediate(gameObject);

            StartParent.GetComponentInParent<ActionPanelActionCreator>()?.UpdatePlayerActionList();            
        }
        
    }
}
