using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{

    public Sprite inventoryImage;

    public virtual void Use(GameObject emitter, Vector3 direction) { }
}
