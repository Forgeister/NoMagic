using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    CardInGame ingame;
    CardDatabase cardDatabase;
    CardData cardData;
    GameObject zoneArea;
    GameObject exileZone;
    GameObject graveyardZone;
    GameObject deckZone;
    GameObject handZone;
    Show show;
    Transform originalPlace;

    void Start()
    {
        show = GameObject.Find("Database").GetComponent<Show>();
        cardData = GameObject.Find("Database").GetComponent<CardData>();
        ingame = GameObject.Find("Database").GetComponent<CardInGame>();
        cardDatabase = GameObject.Find("Database").GetComponent<CardDatabase>();
        zoneArea = GameObject.Find("ZoneArea");
        exileZone = zoneArea.transform.FindChild("Exile").gameObject;
        graveyardZone = zoneArea.transform.FindChild("Graveyard").gameObject;
        deckZone = zoneArea.transform.FindChild("Deck").gameObject;
        handZone = zoneArea.transform.FindChild("Hand").gameObject;
    }
	public void OnPointerEnter (PointerEventData eventData){
		//Debug.Log ("OnPointEnter to " + gameObject.name);
		if (eventData.pointerDrag == null)
			return;
				
        if (CheckZone(gameObject))
        {
            return;
        }
		CardData d = eventData.pointerDrag.GetComponent<CardData> ();
		if (d != null) {
			d.placeholderParent = this.transform;
        }
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("clicked on " + gameObject.name);
        if (show.Status())
            show.Deactivate();
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Find("Database").GetComponent<Menu>().Menus(this.gameObject);
        }

    }

    public void OnPointerExit (PointerEventData eventData){
		//Debug.Log ("OnPointExit to " + gameObject.name);
		if (eventData.pointerDrag == null)
			return;

        CardData d = eventData.pointerDrag.GetComponent<CardData> ();
		if (d != null && d.placeholderParent==this.transform) {
            d.placeholderParent = d.originalParent;
        }

	}

	public void OnDrop (PointerEventData eventData){
        Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        CardData d = eventData.pointerDrag.GetComponent<CardData>();
        if (d != null)
        {
            if (CheckZone(gameObject))
            {
                d.GetComponent<RectTransform>().position = d.originalPosition;
            }
            else
            {
                ingame.MoveTo(d.gameObject, this.transform);
                d.originalParent = this.transform;
            }
            if (show.Status())
                show.ReList();
        }
    }
    private bool CheckZone(GameObject area)
    {
        return ((area.name == "LifePanel") || (area.name == "ZoneArea"));
    }
}
