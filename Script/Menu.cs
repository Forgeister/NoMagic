using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class Menu : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    GameObject menu;
    GameObject escMenu;
    CardInGame ingame;
    Show show;
    Card card;
    Transform parent;
    string[] cardMoves = 
    {
        "Move to Top Deck",                 //0
        "Move to Bottom Deck",              //1
        "Move to Deck Position",            //2
        "Move to Hand",                     //3
        "Move to Graveyard",                //4
        "Move to Exile",                    //5
        "Move to Battlefield"               //6
    };
    string[] zoneMoves =
    {
        "Draw",                             //0
        "Show",                             //1
        "Shuffle",                          //2
        "Move all to Graveyard",            //3
        "Move all to Exile",                //4
        "Move all to Hand",                 //5
        "Move all to Top Deck",             //6
        "Move all to Bottom Deck",          //7
        "Move all to Battlefield"           //8
    };
    public GameObject refButton;
  
    void Start() {

        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
        show = GameObject.Find("Database").GetComponent<Show>();
        menu = GameObject.Find("Menu");
        escMenu = GameObject.Find("EscMenu");
        menu.SetActive(false);
        escMenu.SetActive(false);
	}
    void Update()
    {
        //esc
        if (Input.GetKeyDown("escape"))
        {
            if (!EscStatus())
            {
                CreateEscButton();
            }
            else
            {
                EscDeactivate();
            }
        }
    }
    public void Menus(GameObject gObj)
    {
        menu.transform.position = new Vector3(500f, 500f);
        if (gObj.GetComponent<CardData>() != null)
        {
            CardMenu(gObj);
        }
        else
        {
            if((gObj.name!="ZoneArea") && (gObj.name != "BattlefieldArea") && (gObj.name != "HandArea") && (gObj.name != "LifePanel"))
                ZoneMenu(gObj);
        }
        
    }

    public void ZoneMenu(GameObject gObj)
    {
        menu.GetComponentInChildren<Text>().text = gObj.name;
        switch (gObj.name)
        {
            case ("Exile"):
                for (int i = 0; i < 9; i++)
                {
                    if ((i != 4)&&(i != 0))
                    {
                        CreateZoneButton(i, gObj, ingame.exileList);
                    }
                }
                break;
            case ("Graveyard"):
                for (int i = 0; i < 9; i++)
                {
                    if ((i != 3)&&(i!=0))
                    {
                        CreateZoneButton(i, gObj, ingame.graveyardList);
                    }
                }
                break;
            case ("Deck"):
                for (int i = 0; i < 9; i++)
                {
                    if ((i != 6)&&(i!=7))
                    {
                        CreateZoneButton(i, gObj, ingame.deckList);
                    }
                }
                break;
            case ("Hand"):
                for (int i = 0; i < 9; i++)
                {
                    if ((i != 5)&&(i!=0))
                    {
                        CreateZoneButton(i, gObj, ingame.handList);
                    }
                }
                break;

        }
        menu.SetActive(true);
    }
    private void CreateZoneButton(int btn, GameObject gObj, List<Card> list)
    {
        GameObject buttonObj = Instantiate(refButton);
        buttonObj.transform.SetParent(menu.transform, false);

        switch (btn)
        {
            case (0):   // 0 Draw
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    ingame.Draw();
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (1):   // 1 Show
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ShowZone(gObj); Debug.Log(gObj.name + zoneMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn]; 
                break;
            case (2):   // 2 Shuffle
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.Shuffle(list); Debug.Log(gObj.name + zoneMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (3):   // 3 Move all to Graveyard
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    if (!list.Equals(ingame.handList)) //se lista
                    { 
                        do
                        {
                            ingame.Create(list[0]);
                            GameObject moving = ingame.handCardList[ingame.handCardList.Count - 1];
                            moving.GetComponent<CardData>().originalParent = GameObject.Find("HandArea").transform;
                            moving.GetComponent<CardData>().initialPosition = 0;
                            list.RemoveAt(0);
                            ingame.MoveToGraveyard(moving);
                        } while (list.Count != 0);
                    }else
                    {
                        do
                        {
                            ingame.MoveToGraveyard(ingame.handCardList[0]);
                        } while (ingame.handCardList.Count > 0);
                    }

                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (4):   // 4 Move all to Exile
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    if (!list.Equals(ingame.handList))
                    {
                        do
                        {
                            ingame.Create(list[0]);
                            GameObject moving = ingame.handCardList[ingame.handCardList.Count - 1];
                            moving.GetComponent<CardData>().originalParent = GameObject.Find("HandArea").transform;
                            moving.GetComponent<CardData>().initialPosition = 0;
                            list.RemoveAt(0);
                            ingame.MoveToExile(moving);
                        } while (list.Count != 0);
                    }
                    else
                    {
                        do
                        {
                            ingame.MoveToExile(ingame.handCardList[0]);
                        } while (ingame.handCardList.Count > 0);


                    }
                    ClearList(list);
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (5):   // 5 Move all to Hand
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    foreach (Card card in list)
                    {
                        ingame.Create(card);
                    }
                    ClearList(list);
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (6):   // 6 Move all to Top Deck
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    if (!list.Equals(ingame.handList))
                    {

                        do
                        {
                            ingame.Create(list[0]);
                            GameObject moving = ingame.handCardList[ingame.handCardList.Count - 1];
                            moving.GetComponent<CardData>().originalParent = GameObject.Find("HandArea").transform;
                            moving.GetComponent<CardData>().initialPosition = 0;
                            list.RemoveAt(0);
                            ingame.MoveToDeck(moving, 1);
                        } while (list.Count != 0);

                    }
                    else
                    {
                        do
                        {
                            ingame.MoveToDeck(ingame.handCardList[0], 1);;

                        } while (ingame.handCardList.Count > 0);

                    }
                    ClearList(list);
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (7):   // 7 Move all to Bottom Deck
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    if (!list.Equals(ingame.handList))
                    {
                        do
                        {
                            ingame.Create(list[0]);
                            GameObject moving = ingame.handCardList[ingame.handCardList.Count - 1];
                            moving.GetComponent<CardData>().originalParent = GameObject.Find("HandArea").transform;
                            moving.GetComponent<CardData>().initialPosition = 0;
                            list.RemoveAt(0);
                            ingame.MoveToDeck(moving, 0);
                        } while (list.Count != 0);
                    }
                    else
                    {
                        do
                        {
                            ingame.MoveToDeck(ingame.handCardList[0], 0); ;
                        } while (ingame.handCardList.Count > 0);


                    }
                    ClearList(list);
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;
            case (8):   // 8 Move all to Battlefield
                buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                    if (!list.Equals(ingame.handList))
                    {
                        foreach (Card card in list)
                        {
                            ingame.Create(card);
                            GameObject moving = ingame.handCardList[ingame.handCardList.Count - 1];
                            moving.GetComponent<CardData>().originalParent = GameObject.Find("HandArea").transform;
                            ingame.MoveToBattlefield(moving);
                        }
                    }
                    else
                    {
                        foreach (GameObject card in ingame.handCardList)
                        {
                            ingame.MoveToBattlefield(card);
                        }
                        ingame.handCardList.Clear();
                    }
                    ClearList(list);
                    Debug.Log(gObj.name + zoneMoves[btn]);
                    Deactivate();
                });
                buttonObj.GetComponentInChildren<Text>().text = zoneMoves[btn];
                break;

        }

    }
    private void ClearList(List<Card> list)
    {
        list.Clear();
    }

    public void CreateEscButton()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case (0):
                    GameObject buttonObj0 = Instantiate(refButton);
                    buttonObj0.transform.SetParent(escMenu.transform, false);
                    buttonObj0.GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Game resumed"); EscDeactivate(); });
                    buttonObj0.GetComponentInChildren<Text>().text = "Resume Game";
                    break;
                case (1):
                    GameObject buttonObj1 = Instantiate(refButton);
                    buttonObj1.transform.SetParent(escMenu.transform, false);
                    buttonObj1.GetComponent<Button>().onClick.AddListener(() => { ingame.LoadDeck("decktest"); Debug.Log("Loading deck"); EscDeactivate(); });
                    buttonObj1.GetComponentInChildren<Text>().text = "Load Deck";
                    break;/*
                case (2):
                    GameObject buttonObj2 = Instantiate(refButton);
                    buttonObj2.transform.SetParent(menu.transform, false);
                    buttonObj2.GetComponent<Button>().onClick.AddListener(() => { ingame.LoadDeck("abc"); Debug.Log("Loading deck"); Deactivate(); });
                    buttonObj2.GetComponentInChildren<Text>().text = "Save Deck";
                    break;*/
                case (2):
                    GameObject buttonObj3 = Instantiate(refButton);
                    buttonObj3.transform.SetParent(escMenu.transform, false);
                    buttonObj3.GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); Debug.Log("Game exited"); EscDeactivate(); });
                    buttonObj3.GetComponentInChildren<Text>().text = "Exit Game";
                    break;
                
            }
            escMenu.SetActive(true);
        }

    }

    private void CreateCardButton(int btn, GameObject gObj)
    {
        GameObject buttonObj = Instantiate(refButton);
        buttonObj.transform.SetParent(menu.transform, false);
        
        switch (btn)
        {
            case (0):   // 0 Move to TopDeck
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.MoveToDeck(gObj, 1); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (1):   // 1 Move to BotDeck
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.MoveToDeck(gObj, 0); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (2):   // 2 Move to DeckPosition
                //buttonObj.GetComponent<Button>().onClick.AddListener(() => { int p;  Int32.TryParse(Console.ReadLine(), out p); ingame.MoveToDeck(gObj, p); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); });
                //buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                Destroy(buttonObj);
                break;
            case (3):   // 3 Move to Hand
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.MoveToHand(gObj); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (4):   // 4 Move to Graveyard
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.MoveToGraveyard(gObj); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (5):   // 5 Move to Exile
                buttonObj.GetComponent<Button>().onClick.AddListener( () => { ingame.MoveToExile(gObj); Debug.Log(gObj.name + cardMoves[btn]); Deactivate(); }) ;
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (6):   // 6 Move to Battlefield
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.MoveToBattlefield(gObj); Deactivate(); });
                buttonObj.GetComponentInChildren<Text>().text = cardMoves[btn];
                break;
            case (7):   // Turn 
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.Turn(gObj); Deactivate(); });
                if (gObj.GetComponent<CardData>().facedown)
                    buttonObj.GetComponentInChildren<Text>().text = "Faceup";
                else
                    buttonObj.GetComponentInChildren<Text>().text = "Facedown";
                break;
            case (8):   // Flip
                buttonObj.GetComponent<Button>().onClick.AddListener(() => { ingame.Flip(gObj); Deactivate(); });
                if (gObj.GetComponent<CardData>().flipped)
                    buttonObj.GetComponentInChildren<Text>().text = "Flipup";
                else
                    buttonObj.GetComponentInChildren<Text>().text = "Flipdown";
                break;

        }

    }

    public void CardMenu(GameObject gObj)
    {
        menu.GetComponentInChildren<Text>().text = gObj.name;
        Card card = gObj.GetComponent<CardData>().card;
        Debug.Log(gObj.GetComponent<CardData>().originalParent.name);
        switch (gObj.GetComponent<CardData>().originalParent.name)
        {
            case ("HandArea"):
                for(int i = 0; i < 7; i++)
                {
                    if (i != 3)
                        CreateCardButton(i, gObj);
                }
                break;
            case ("BattlefieldArea"):
                for (int i = 0; i < 7; i++)
                {
                    if (i != 6)
                        CreateCardButton(i, gObj);
                }
                CreateCardButton(7, gObj);
                CreateCardButton(8, gObj);
                break;
            case ("Show"):
                switch(show.whichList)
                {
                    case ("Exile"):
                        for (int i = 0; i < 7; i++)
                        {
                            if (i != 5)
                                CreateCardButton(i, gObj);
                        }
                        break;
                    case ("Graveyard"):
                        for (int i = 0; i < 7; i++)
                        {
                            if (i != 4)
                                CreateCardButton(i, gObj);
                        }
                        break;
                    case ("Deck"):
                        for (int i = 0; i < 7; i++)
                        {
                            if ((i != 0)&&(i!=1)&&(i!=2))
                                CreateCardButton(i, gObj);
                        }
                        break;
                }
                
                break;
        }
        menu.SetActive(true);
    }

    public void ShowZone(GameObject gObj)
    {
        switch (gObj.name)
        {
            case ("Exile"):
                if (show.whichList == "Exile")
                    Debug.Log(show.whichList + " ja aberto");
                show.ShowShow(ingame.exileList, gObj.name);
                break;
            case ("Deck"):
                if (show.whichList == "Deck")
                    Debug.Log(show.whichList + " ja aberto");
                show.ShowShow(ingame.deckList, gObj.name);
                break;
            case ("Graveyard"):
                if (show.whichList == "Graveyard")
                    Debug.Log(show.whichList + " ja aberto");
                show.ShowShow(ingame.graveyardList, gObj.name);
                break;
            case ("HandArea"):
                //show.ShowShow(ingame.handList);
                break;
        }
    }


    public void Deactivate()
    {
        if (Status())
        {
            foreach (Transform child in menu.transform)
            {
                if (child.name != "MenuText")
                {
                    foreach (Transform childchild in child)
                    {
                        Destroy(childchild.gameObject);
                    }
                    Destroy(child.gameObject);
                }
            }
        }
        menu.SetActive(false);
    }
    public void EscDeactivate()
    {
        if (EscStatus())
        {
            foreach (Transform child in escMenu.transform)
            {
                if (child.name != "escMenuText")
                {
                    foreach (Transform childchild in child)
                    {
                        Destroy(childchild.gameObject);
                    }
                    Destroy(child.gameObject);
                }
            }
        }
        escMenu.SetActive(false);
    }
    public bool Status()
    {
        return menu.activeSelf;
    }
    public bool EscStatus()
    {
        return escMenu.activeSelf;
    }
}
/*
   if(Input.GetMouseButtonDown(0)) Debug.Log("Pressed left click.");
   if(Input.GetMouseButtonDown(1)) Debug.Log("Pressed right click.");
   if(Input.GetMouseButtonDown(2)) Debug.Log("Pressed middle click.");
   */

