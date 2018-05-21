using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWheel : MonoBehaviour {

    public SpriteRenderer T, TR, R, BR, B, BL, L, TL;
    private List<SpriteRenderer> selections = new List<SpriteRenderer>();
    private bool showWheel = false;
    private GameObject currentlySelected;

	// Use this for initialization
	void Start ()
    {
        SetVisibility(showWheel);
        selections.Add(T);
        selections.Add(TR);
        selections.Add(R);
        selections.Add(BR);
        selections.Add(B);
        selections.Add(BL);
        selections.Add(L);
        selections.Add(TL);
    }
	
	// Update is called once per frame
	void Update ()
    {
        

        if (Input.GetButtonDown("LeftBumper"))
        {
            if(!showWheel)
            {
                showWheel = true;
                SetVisibility(showWheel);
            }

        }
        else if (Input.GetButtonUp("LeftBumper"))
        {
            if (showWheel)
            {
                showWheel = false;
                SetVisibility(showWheel);
            }
        }

        if (showWheel)
        {
            var move = new Vector3(Input.GetAxis("Look_Horizontal"), Input.GetAxis("Look_Vertical"), 0);

            if (move.magnitude > .04f)
            {
                float angle = Vector3.Angle(move, transform.up);

                if (move.x > 0f)
                    angle = -angle;

                angle += 180f;

                Select(angle);

            }
        }
    }

    private void SetVisibility(bool visible)
    {
        Component[] spriteRenderers;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        //TODO: Remove these two lines and fix the if statement below
        int count = 0;
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = visible;

            //TODO: Replace this horrible if bollocks with something, anything
            if (visible)
            {
                if (spriteRenderer.name.Equals("Icon"))
                {
                    if(count < inventory.items.Count)
                    {
                        spriteRenderer.sprite = inventory.items[count].inventoryImage;
                    }
                    else
                    {
                        spriteRenderer.sprite = null;
                    }
                    count++;
                }
            }
        }
        
    }

    private void Select(float angle)
    {
        DeselectItem();

        float turn = 22.5f;

        int newSelection;

        if (angle < turn)
            newSelection = 0;
        else if (angle < (turn += 45f))
            newSelection = 1;
        else if (angle < (turn += 45f))
            newSelection = 2;
        else if (angle < (turn += 45f))
            newSelection = 3;
        else if (angle < (turn += 45f))
            newSelection = 4;
        else if (angle < (turn += 45f))
            newSelection = 5;
        else if (angle < (turn += 45f))
            newSelection = 6;
        else if (angle < (turn += 45f))
            newSelection = 7;
        else
            newSelection = 0;

        SelectItem(newSelection);
    }

    private void SelectItem(int selection)
    {
        currentlySelected = selections[selection].gameObject;

        currentlySelected.transform.localScale = Vector3.one * 1.2f;
        currentlySelected.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8f);

        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        if (selection < inventory.items.Count)
        {
            inventory.selectedItem = inventory.items[selection];
        }
        else
        {
            inventory.selectedItem = null;
        }
    }

    private void DeselectItem()
    {
        if (currentlySelected) {
            currentlySelected.transform.localScale = Vector3.one * 0.9f;
            currentlySelected.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);

            currentlySelected = null;
        }
    }
    
}
