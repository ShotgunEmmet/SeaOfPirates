using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : InventoryItem {

    public GameObject bomb;

    public override void Use(GameObject emitter, Vector3 direction)
    {
        var newProjectile = Instantiate(bomb);
        newProjectile.transform.position += emitter.transform.position + direction.normalized * 0.5f;
    }
}
