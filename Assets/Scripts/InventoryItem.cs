using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{

    public Sprite inventoryImage;

    public virtual void Use(Vector3 position, Vector3 direction) { }
}
