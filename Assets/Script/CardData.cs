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

    void start()
    {
        cardDatabase = GameObject.Find("Database").GetComponent<CardDatabase>();
        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
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

        originalParent = this.transform.parent;
        placeholderParent = originalParent;
		this.transform.SetParent (this.transform.parent.parent);

		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;
        //Debug.Log("Posicao inicial "+placeholder.transform.GetSiblingIndex());
        initialPosition = placeholder.transform.GetSiblingIndex();
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
		this.transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
        
        Destroy (placeholder);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click on " + gameObject.name);
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Find("Database").GetComponent<Menu>().Menus(this.gameObject);
        }
        if (Input.GetMouseButtonDown(0))
            GameObject.Find("Database").GetComponent<Tooltip>().Activate(card);
        originalPosition = gameObject.GetComponent<RectTransform>().position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("unclick");
        GameObject.Find("Database").GetComponent<Tooltip>().Deactivate();
    }

}
