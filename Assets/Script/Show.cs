using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Show : MonoBehaviour {
    private GameObject show;
    public GameObject cardRef;
    CardInGame ingame;
    public List<Card> actualList;
    public string whichList;
    GameObject zoneArea;
    GameObject exileZone;
    GameObject graveyardZone;
    GameObject deckZone;

    void Start() {
        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
        show = GameObject.Find("Show");
        show.SetActive(false);
        zoneArea = GameObject.Find("ZoneArea");
        exileZone = zoneArea.transform.FindChild("Exile").gameObject;
        graveyardZone = zoneArea.transform.FindChild("Graveyard").gameObject;
        deckZone = zoneArea.transform.FindChild("Deck").gameObject;

    }

    public void Activate()
    {
        show.SetActive(true);
    }
    public void Deactivate()
    {
        show.SetActive(false);
    }
    public void ShowShow(List<Card> list, string what)
    {
        whichList = what;
        this.actualList = list;
        ClearShow();
        Activate();
        foreach (Card card in list)
        {
            CreateShow(card);
        }
    }
    private void CreateShow(Card card)
    {
        GameObject cardObj = Instantiate(cardRef);
        cardObj.GetComponent<CardData>().card = card;
        cardObj.transform.SetParent(show.transform, false);
        cardObj.name = card.name;
        cardObj.GetComponentInChildren<Text>().text = card.name;
        ingame.LoadCardImage(cardObj, card);
    }
    private void ClearShow()
    {
        foreach (Transform child in show.transform)
        {
            foreach (Transform childchild in child)
            {
                Destroy(childchild.gameObject);
            }
            Destroy(child.gameObject);
        }
    }
    public void ReList()
    {
        ClearShow();
        foreach (Card card in actualList)
        {
            CreateShow(card);
        }
    }

    public bool Status()
    {
        return show.activeSelf;
    }
    public GameObject getShow()
    {
        return show;
    }
}
