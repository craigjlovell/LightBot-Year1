using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    protected DropArea DropArea;

    protected virtual void Awake()
    {
        DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        DropArea.OnDropHandler += OnItemDropped;
        DropArea.DropConditions.Add(new CommandSlotDrop(this));
    }

    private void OnItemDropped(DraggableComponent draggable)
    {
        if(draggable.StartParent.GetComponent<CommandToolBarItem>() != null)
        {
            var obj = Instantiate(draggable.gameObject, draggable.StartParent.transform);
            obj.transform.localPosition = Vector3.zero;
        }
        
        draggable.transform.SetParent(transform);
        draggable.transform.localPosition = Vector3.zero;        
    }
}

public class CommandSlotDrop : DropCond
{
    public EquipmentSlot slot;

    public CommandSlotDrop(EquipmentSlot commandSlot)
    {
        slot = commandSlot;
    }

    public override bool Check(DraggableComponent draggable)
    {
        return slot.transform.childCount == 0;
    }
}
