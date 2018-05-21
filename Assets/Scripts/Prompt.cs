using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour {
    
    public TextMeshProUGUI promptText;

    private void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Show(string text)
    {
        promptText.text = text;
    }

    public void Hide()
    {
        promptText.SetText("");
    }
}
