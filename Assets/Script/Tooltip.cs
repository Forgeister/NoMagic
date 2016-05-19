using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class Tooltip : MonoBehaviour
{
    public Card card;
    private string text;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }
    void update()
    {
        tooltip.transform.position = Input.mousePosition;
    }

    public void Activate(Card card)
    {
        //Debug.Log("ativ");
        this.card = card;
        tooltip.SetActive(true);
        ConstructText(card);

       
    }


    public void Deactivate()
    {
        //Debug.Log("desativ");
        this.tooltip.SetActive(false);
    }

    public void ConstructText(Card card)
    {
        string cardInfo = "";
        cardInfo += (!(String.IsNullOrEmpty(card.name)) ? card.name : "") + "\n";
        cardInfo += (!(String.IsNullOrEmpty(card.manaCost)) ? card.manaCost : "") + " (" + card.cmc.ToString() + ")\n";
        cardInfo += (!(String.IsNullOrEmpty(card.type)) ? card.type : "") + "\n";
        cardInfo += (!(String.IsNullOrEmpty(card.text)) ? card.text : "") + "\n";
        cardInfo += (!(String.IsNullOrEmpty(card.power)) && !(String.IsNullOrEmpty(card.toughness)) ? card.power + "/" + card.toughness : "");
        cardInfo += (card.loyality != 0 ? card.loyality.ToString() : ""  );
        tooltip.GetComponentInChildren<Text>().text = cardInfo;
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/Sprites/CardImg/" + card.name.ToString() + ".jpg"));
        Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        for (int i = 0; i < tooltip.transform.childCount; i++)
            if (tooltip.transform.GetChild(i).name == "CardImage")
                tooltip.transform.GetChild(i).GetComponent<Image>().sprite = image;
    }


}
