using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class CommandToolBarItem : MonoBehaviour
{
    protected DropArea DropArea;

    protected virtual void Awake()
    {
        DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        DropArea.OnDropHandler += OnItemDropped;
        DropArea.DropConditions.Add(new CommandToolBarItemSlotDrop(this));
    }

    private void OnItemDropped(DraggableComponent draggable)
    {
        draggable.transform.SetParent(transform);
        draggable.transform.localPosition = Vector3.zero;
    }
}

public class CommandToolBarItemSlotDrop : DropCond
{
    public CommandToolBarItem slot;

    public CommandToolBarItemSlotDrop(CommandToolBarItem commandSlot)
    {
        slot = commandSlot;
    }

    public override bool Check(DraggableComponent draggable)
    {
        return false;
    }
}
