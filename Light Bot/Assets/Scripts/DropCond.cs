using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropCond
{
    public abstract bool Check(DraggableComponent draggable);
}
