using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct ItemEntry
{
    public string itemName;
    public int Entryamount;
    public GameObject buttonRef;
}

public class Button : MonoBehaviour
{
    public GameObject button;
    public Transform parent;
    public TMP_InputField input;
    private bool addPressed;
    private bool addText = true;
    private bool removeText = true;
    private string Name;
    private int Amount;


    public List<ItemEntry> Items = new List<ItemEntry>();
    public void AddEntry()
    {
        GameObject tempButton = button;
        tempButton = Instantiate(button, parent, false);
        TextMeshProUGUI tempText = tempButton.GetComponentInChildren<TextMeshProUGUI>();
        tempText.text = (Name + " " + Amount);
        Items.Add(new ItemEntry { itemName = Name, Entryamount = Amount, buttonRef = tempButton});
        TextShow textShow = gameObject.GetComponent<TextShow>();
        textShow.Hide();
    }

    public void RemoveEntry()
    {
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (Items[i].itemName == Name)
            {
                ItemEntry entry = Items[i];
                entry.Entryamount -= Amount;

                if (entry.Entryamount <= 0)
                {
                    Destroy(Items[i].buttonRef);
                    Items.RemoveAt(i);
                }
                else
                {
                    Items[i] = entry; 
                    TextMeshProUGUI Text = Items[i].buttonRef.GetComponentInChildren<TextMeshProUGUI>();
                    Text.text = (Name + " " + entry.Entryamount);
                }

                break;
            }
        }
        TextShow textShow = gameObject.GetComponent<TextShow>();
        textShow.Hide();
    }

    public void Trigger()
    {
        if (addPressed)
        {
            Add();
        }
        else
        {
            Remove();
        }
    }

    public void Add()
    {
        if (addText == true)
        {
            Name = input.text;
            input.contentType = TMP_InputField.ContentType.IntegerNumber;
            input.characterLimit = 7;
            addText = false;
            input.text = "";
        }
        else
        {
            int amount;
            if (int.TryParse(input.text, out amount))
                Amount = amount;
            input.contentType = TMP_InputField.ContentType.Standard;
            input.characterLimit = 0;
            addText = true;
            input.text = "";
            AddEntry();
        }
        
    }

    public void Remove()
    {
        if (removeText == true)
        {
            Name = input.text;
            input.contentType = TMP_InputField.ContentType.IntegerNumber;
            input.characterLimit = 7;
            removeText = false;
            input.text = "";
        }
        else
        {
            int amount;
            if (int.TryParse(input.text, out amount))
                Amount = amount;
            input.contentType = TMP_InputField.ContentType.Standard;
            input.characterLimit = 0;
            removeText = true;
            input.text = "";
            RemoveEntry();
        }
    }

    public void RemovePressed() 
    { 
        addPressed = false;
        addText = true;
        input.characterLimit = 0;
    }
    public void AddPressed() 
    { 
        addPressed = true;
        removeText = true;
        input.characterLimit = 0;
    }
}
