using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
    public List<InventoryItem> items;
    private InventoryItem m_selectedItem;

    // Use this for initialization
    void Start () {
        //m_selectedItem = items[0];
	}

    public InventoryItem selectedItem
    {
        get { return m_selectedItem; }
        set { m_selectedItem = value; }
    }
    
}
