using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Menu : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    GameObject menu;
    CardInGame ingame;
    Show show;
    Card card;
    Transform parent;
  
    void Start() {
        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
        show = GameObject.Find("Database").GetComponent<Show>();
        menu = GameObject.Find("Menu");
        //menu.SetActive(false);
	}

    public void Menus(GameObject gObj)
    {
        parent = gObj.transform.parent;
        if (gObj.GetComponent<CardData>() != null)
        {
            card = gObj.GetComponent<CardData>().card;
        }
        else
        {
            switch (gObj.name)
            {
                case ("Exile"):
                    //moveAlltoGraveyard
                    //moveAlltoHand
                    //moveAlltoTopDeck
                    //moveAlltoBottomDeck
                    //
                    show.ShowShow(ingame.exileList, gObj.name);
                    break;
                case ("Deck"):
                    show.ShowShow(ingame.deckList, gObj.name);
                    break;
                case ("Graveyard"):
                    show.ShowShow(ingame.graveyardList, gObj.name);
                    break;
                case ("HandArea"):
                    //show.ShowShow(ingame.handList);
                    break;
            }
        }
    }


}
/*
   if(Input.GetMouseButtonDown(0)) Debug.Log("Pressed left click.");
   if(Input.GetMouseButtonDown(1)) Debug.Log("Pressed right click.");
   if(Input.GetMouseButtonDown(2)) Debug.Log("Pressed middle click.");
   */
