using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CardData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {


    public Card card;
    private CardDatabase cardDatabase;
    private CardInGame ingame;
    private Tooltip tooltip;
    public int initialPosition;
    public Vector3 originalPosition;
    public Transform originalParent = null;
    public Transform placeholderParent = null;
    GameObject placeholder = null;
    GameObject canvas;
    Menu menu;
    Show show;
    public bool facedown;
    public bool tapped;
    public bool flipped;
    public int counter;
    public int counter1;
    public int counter2;

    void start()
    {
        show = GameObject.Find("Database").GetComponent<Show>();
        menu = GameObject.Find("Database").GetComponent<Menu>();
        cardDatabase = GameObject.Find("Database").GetComponent<CardDatabase>();
        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
        canvas = GameObject.Find("Canvas");
    }


	public void OnBeginDrag(PointerEventData eventData){


		//Debug.Log ("OnBeginDrag");
        GameObject.Find("Database").GetComponent<Tooltip>().Deactivate();
        placeholder = new GameObject ();
		placeholder.transform.SetParent (this.transform.parent);
        placeholder.name = this.GetComponentInChildren<Text>().text+" placeholder";
		LayoutElement le = placeholder.AddComponent<LayoutElement> (); 
		le.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex ());

        //originalParent = this.transform.parent;
        placeholderParent = originalParent;
        this.transform.SetParent(placeholderParent);

		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
        //Debug.Log("Posicao inicial "+placeholder.transform.GetSiblingIndex());
        //initialPosition = placeholder.transform.GetSiblingIndex();
        //Debug.Log("Original de " + originalParent.name);

    }
	public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag");
		this.transform.position = eventData.position;

		if (placeholderParent.transform.parent != placeholderParent)
			placeholder.transform.SetParent (placeholderParent);

		int newSiblingIndex = placeholderParent.childCount;

		for (int i=0;i < placeholderParent.childCount; i++){
			if (this.transform.position.x < placeholderParent.GetChild(i).position.x){
				newSiblingIndex = i;
				if (placeholder.transform.GetSiblingIndex () < newSiblingIndex) {
					newSiblingIndex--;
				}
				break;
			}
		}
		placeholder.transform.SetSiblingIndex (newSiblingIndex);
	}
	public void OnEndDrag(PointerEventData eventData){
        //Debug.Log ("OnEndDrag");
        this.transform.SetParent (originalParent);
        this.placeholderParent = null;
        this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
        Destroy (placeholder);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = gameObject.GetComponent<RectTransform>().position;
        initialPosition = gameObject.transform.GetSiblingIndex();
        originalParent = this.transform.parent;
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Find("Database").GetComponent<Menu>().Deactivate();
            Debug.Log("right click on " + gameObject.name);
            GameObject.Find("Database").GetComponent<Menu>().Menus(this.gameObject);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Database").GetComponent<Menu>().Deactivate();
            Debug.Log("left click on " + gameObject.name);
            GameObject.Find("Database").GetComponent<Tooltip>().Activate(card);
        }else if (Input.GetMouseButtonDown(2))
        {
            if (this.gameObject.transform.parent.name=="BattlefieldArea")
                GameObject.Find("Database").GetComponent<CardInGame>().Tap(this.gameObject);
        }


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("unclick");
        GameObject.Find("Database").GetComponent<Tooltip>().Deactivate();
    }

}
