using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class CardInGame : MonoBehaviour
{
    int travamultidownload = 0; //para impedir de tentar baixar varias vezes
    public int originalPosition; //para remover do originalparent

    CardDatabase cardDatabase;
    Tooltip tooltip;
    Show show;
    GameObject tabletop;
    GameObject handArea;
    GameObject landArea;
    GameObject creatureArea;
    GameObject nonCreatureArea;
    GameObject zoneArea;
    GameObject exileZone;
    GameObject graveyardZone;
    GameObject deckZone;
    GameObject handZone;
    GameObject showZone;

    Image exileZoneImage;
    Image graveyardZoneImage;
    public GameObject cardRef;

    public List<GameObject> handList = new List<GameObject>();
    public List<Card> deckList = new List<Card>();
    public List<Card> graveyardList = new List<Card>();
    public List<Card> exileList = new List<Card>();


    void Start() {
        //procurando objetos
                
        cardDatabase = GameObject.Find("Database").GetComponent<CardDatabase>();
        tooltip = GameObject.Find("Database").GetComponent<Tooltip>();
        show = GameObject.Find("Database").GetComponent<Show>();
        tabletop = GameObject.Find("Tabletop");
        handArea = GameObject.Find("HandArea");
        landArea = GameObject.Find("LandArea");
        creatureArea = GameObject.Find("CreatureArea");
        nonCreatureArea = GameObject.Find("NonCreatureArea");
        zoneArea = GameObject.Find("ZoneArea");
        exileZone = zoneArea.transform.FindChild("Exile").gameObject;
        exileZoneImage = exileZone.transform.FindChild("ExileImage").GetComponent<Image>();
        graveyardZone = zoneArea.transform.FindChild("Graveyard").gameObject;
        graveyardZoneImage = graveyardZone.transform.FindChild("GraveyardImage").GetComponent<Image>();
        deckZone = zoneArea.transform.FindChild("Deck").gameObject;
        handZone = zoneArea.transform.FindChild("Hand").gameObject;
        showZone = GameObject.Find("Show");


        deckList = LoadDeck("decktest");
        Shuffle(deckList);

        Draw();
        Draw();
        Draw();

        Draw();
        Draw();
        Debug.Log("Fim");
    }

    void Update()
    {
        //atualizando imagem da deckzone
        if (deckZone.transform.FindChild("DeckImage").GetComponent<Image>().color.a != 255)
        {
            Image deckZoneImage = deckZone.transform.FindChild("DeckImage").GetComponent<Image>();
            deckZoneImage.type = Image.Type.Simple;
            deckZoneImage.preserveAspect = true;
            Color alpha = deckZoneImage.color;
            alpha.a = 255;
            deckZoneImage.color = alpha;
        }
        //atualizando imagem da handzone
        if (handZone.transform.FindChild("HandImage").GetComponent<Image>().color.a != 255)
        {
            Image handZoneImage = handZone.transform.FindChild("HandImage").GetComponent<Image>();
            handZoneImage.type = Image.Type.Simple;
            handZoneImage.preserveAspect = true;
            Color alpha = handZoneImage.color;
            alpha.a = 255;
            handZoneImage.color = alpha;
        }
        //atualizando imagem da exilezone
        if ((exileList.Count > 0) && (exileZoneImage.name != exileList[exileList.Count - 1].name))
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/Sprites/CardImg/" + exileList[exileList.Count - 1].name.ToString() + ".jpg"));
            Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.name = exileList[exileList.Count - 1].name;
            exileZoneImage.sprite = image;
            if (exileZoneImage.color.a != 255)
            {
                exileZoneImage.type = Image.Type.Simple;
                exileZoneImage.preserveAspect = true;
                Color alpha = exileZoneImage.color;
                alpha.a = 255;
                exileZoneImage.color = alpha;
            }
        }
        else if ((exileList.Count == 0) && (exileZoneImage.sprite.name != "NoSprite"))
        {
            exileZoneImage.sprite.name = "NoSprite";
            if (exileZoneImage.color.a == 255)
            {
                Debug.Log("Reduzi alpha de "+exileZone.name);
                exileZoneImage.type = Image.Type.Simple;
                exileZoneImage.preserveAspect = true;
                Color alpha = exileZoneImage.color;
                alpha.a = 0;
                exileZoneImage.color = alpha;
            }
        }
        
    
        //atualizando imagem da graveyardzone            
        if ((graveyardList.Count > 0) && (graveyardZoneImage.name != graveyardList[graveyardList.Count - 1].name))
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/Sprites/CardImg/" + graveyardList[graveyardList.Count - 1].name.ToString() + ".jpg"));
            Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.name = graveyardList[graveyardList.Count - 1].name;
            graveyardZoneImage.sprite = image;
            if (graveyardZoneImage.color.a != 255)
            {
                graveyardZoneImage.type = Image.Type.Simple;
                graveyardZoneImage.preserveAspect = true;
                Color alpha = graveyardZoneImage.color;
                alpha.a = 255;
                graveyardZoneImage.color = alpha;
            }
        } else if ((graveyardList.Count == 0) && (graveyardZoneImage.sprite.name!= "NoSprite"))
        {
            graveyardZoneImage.sprite.name = "NoSprite";
            if (graveyardZoneImage.color.a == 255)
            {
                Debug.Log("Reduzi alpha de "+graveyardZone.name);
                graveyardZoneImage.type = Image.Type.Simple;
                graveyardZoneImage.preserveAspect = true;
                Color alpha = graveyardZoneImage.color;
                alpha.a = 0;
                graveyardZoneImage.color = alpha;
            }
        }
        
        //atualizando quantidade de cartas da deckzone
        deckZone.transform.FindChild("DeckText").GetComponent<Text>().text = "Deck\n" + deckList.Count;
        //atualizando quantidade de cartas da handzone
        handZone.transform.FindChild("HandText").GetComponent<Text>().text = "Hand\n" + handList.Count;
        //atualizando quantidade de cartas da exilezone
        exileZone.transform.FindChild("ExileText").GetComponent<Text>().text = "Exile\n" + exileList.Count;
        //atualizando quantidade de cartas da graveyardzone
        graveyardZone.transform.FindChild("GraveyardText").GetComponent<Text>().text = "Grave\nyard\n" + graveyardList.Count;
        //atualizando imagens de cartas
        GameObject[] objInGame = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObj in objInGame)
        {
            if (gameObj.GetComponent<CardInGame>())
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (this.transform.GetChild(i).name == "CardImage")
                    {
                        if (this.transform.GetChild(i).GetComponent<Image>().sprite.name != this.gameObject.GetComponent<CardData>().card.name)
                        {
                            LoadCardImage(this.gameObject, this.gameObject.GetComponent<CardData>().card);
                        }
                        break;
                    }
                }
        }
        //-----
    }

    public void Draw()
    {
        Card cardDraw = deckList[0];
        Create(cardDraw);
        deckList.RemoveAt(0);
    }
    public void Create(Card cardToAdd)
    {
        GameObject cardObj = Instantiate(cardRef);
        cardObj.GetComponent<CardData>().card = cardToAdd;
        cardObj.transform.SetParent(handArea.transform, false);
        cardObj.name = cardToAdd.name;
        cardObj.GetComponentInChildren<Text>().text = cardToAdd.name;
        cardObj.GetComponent<LayoutElement>().preferredHeight = 125;
        cardObj.GetComponent<LayoutElement>().preferredWidth = 100;
        foreach (RectTransform child in cardObj.GetComponentsInChildren<RectTransform>())
            child.sizeDelta = new Vector2(100, 125);
        LoadCardImage(cardObj, cardToAdd);
        handList.Add(cardObj);
    }
    public void LoadCardImage(GameObject cObj, Card cardToAdd)
    {
        if (File.Exists(Application.dataPath + "/Sprites/CardImg/" + cardToAdd.name.ToString() + ".jpg"))
        {
            //Debug.Log("loading image of " + cardToAdd.name);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.dataPath + "/Sprites/CardImg/" + cardToAdd.name.ToString() + ".jpg"));
            Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            image.name = cardToAdd.name;
            if (cObj.GetComponent<CardData>().card.name == cardToAdd.name)
            {
                cObj.transform.FindChild("CardImage").GetComponent<Image>().sprite = image;
            }
        }
        else
        {
            StartCoroutine(DownloadCardImage(cardToAdd));
        }

    }
    public IEnumerator DownloadCardImage(Card cardToAdd)
    {
        if ((!File.Exists(Application.dataPath + "/Sprites/CardImg/" + cardToAdd.name.ToString() + ".jpg")) && (travamultidownload == 0))
        {
            Debug.Log("downloading " + cardToAdd.name + " from magiccards.info/scans/en/" + cardToAdd.magiccardsedition + "/" + cardToAdd.magiccardseditionnumber + ".jpg");
            Texture2D texture = new Texture2D(1, 1);
            WWW magiccards = new WWW("magiccards.info/scans/en/" + cardToAdd.magiccardsedition + "/" + cardToAdd.magiccardseditionnumber + ".jpg");
            travamultidownload = 1;
            yield return magiccards;
            magiccards.LoadImageIntoTexture(texture);
            byte[] savebyte = texture.EncodeToJPG();
            File.WriteAllBytes(Application.dataPath + "/Sprites/CardImg/" + cardToAdd.name.ToString() + ".jpg", savebyte);

        }
    }

    public List<Card> LoadDeck(string fileName) {
        List<Card> decklistLoading = new List<Card>();
        try
        {
            //Debug.Log("Passo1");
            string line;
            //Debug.Log(Application.dataPath + "/" + fileName + ".txt");
            StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName + ".txt", Encoding.Default);

            do
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    //Debug.Log("Passo2");
                    string[] lineSplit = line.Split(' ');
                    int numberofCard;
                    if (int.TryParse(lineSplit[0], out numberofCard))
                    {
                        //Debug.Log("Passo3");
                        lineSplit[0] = null;
                        string nameofCard = string.Join(" ", lineSplit).TrimStart();
                        //Debug.Log("Procurando por " + nameofCard);
                        Card cardinList = cardDatabase.FetchByName(nameofCard);
                        if (cardinList == null)
                        {
                            Debug.Log("Não achei " + nameofCard);
                            return null;
                        }
                        for (int i = 0; i < numberofCard; i++)
                            decklistLoading.Add(cardinList);

                    }
                }


            } while (line != null);

            reader.Close();
            Debug.Log("Deck carregado");
            return decklistLoading;
        }

        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return null;
        }

    }
    static System.Random _random = new System.Random();

    static void Randomize<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {

            int r = i + (int)(_random.NextDouble() * (n - i));
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
    public void Shuffle(List<Card> original)
    {
        System.Random r = new System.Random();
        Card[] numbers = new Card[original.Count];
        for (int i = 0; i < original.Count; i++)
        {
            numbers[i] = original[i];
        }
        Randomize<Card>(numbers);
        for (int i = 0; i < original.Count; i++)
        {
            original[i] = numbers[i];
        }
    }

    public void MoveTo(GameObject targetCard, Transform toWhere)
    {
        if (targetCard.GetComponent<CardData>().placeholderParent == exileZone.transform)
        {
            MoveToExile(targetCard);
        }
        else if (targetCard.GetComponent<CardData>().placeholderParent == graveyardZone.transform)
        {
            MoveToGraveyard(targetCard);
        }
        else if (targetCard.GetComponent<CardData>().placeholderParent == handZone.transform)
        {
            MoveToHand(targetCard);
        }
        else if (targetCard.GetComponent<CardData>().placeholderParent == deckZone.transform)
        {
            MoveToDeck(targetCard);
        }
        else
        {
            if (toWhere == handArea.transform)
                handList.Add(targetCard);
            targetCard.transform.SetParent(toWhere);
            targetCard.GetComponent<LayoutElement>().preferredHeight = 125;
            targetCard.GetComponent<LayoutElement>().preferredWidth = 100;
            foreach (RectTransform child in targetCard.GetComponentsInChildren<RectTransform>())
                child.sizeDelta = new Vector2(100, 125);
        }
        RemoveFromOriginal(targetCard);
    }


    public void MoveToGraveyard(GameObject targetCard)
    {
        graveyardList.Add(targetCard.GetComponent<CardData>().card);
        Destroy(targetCard.GetComponent<CardData>().placeholderParent.FindChild(targetCard.GetComponent<CardData>().card.name + " placeholder").gameObject);
        Destroy(targetCard);
    }

    public void MoveToExile(GameObject targetCard)
    {
        exileList.Add(targetCard.GetComponent<CardData>().card);
        Destroy(targetCard.GetComponent<CardData>().placeholderParent.FindChild(targetCard.GetComponent<CardData>().card.name + " placeholder").gameObject);
        Destroy(targetCard);
    }

    public void MoveToHand(GameObject targetCard)
    {
        Create(targetCard.GetComponent<CardData>().card);
        Destroy(targetCard.GetComponent<CardData>().placeholderParent.FindChild(targetCard.GetComponent<CardData>().card.name + " placeholder").gameObject);
        Destroy(targetCard);
    }

    public void MoveToDeck(GameObject targetCard)
    {
        deckList.Add(targetCard.GetComponent<CardData>().card);
        Card[] deckCopy = new Card[deckList.Count];
        deckList.CopyTo(deckCopy);
        for (int i = 0; i < deckList.Count; i++)
        {
            if (i == deckList.Count - 1)
            {
                deckList[0] = deckCopy[deckList.Count - 1];
            }
            else
            {
                deckList[i + 1] = deckCopy[i];
            }
        }
        Destroy(targetCard.GetComponent<CardData>().placeholderParent.FindChild(targetCard.GetComponent<CardData>().card.name + " placeholder").gameObject);
        Destroy(targetCard);
    }
    private void RemoveFromOriginal(GameObject targetCard)
    {
        Transform originalParent = targetCard.GetComponent<CardData>().originalParent;
        int initialPosition = targetCard.GetComponent<CardData>().initialPosition;
        if (originalParent == handArea.transform)
        {
            handList.RemoveAt(initialPosition);
        }
        if (originalParent == show.getShow().transform)
        {
            switch (show.whichList)
            {
                case ("Exile"):
                    exileList.RemoveAt(initialPosition);
                    break;
                case ("Deck"):
                    deckList.RemoveAt(initialPosition);
                    break;
                case ("Graveyard"):
                    graveyardList.RemoveAt(initialPosition);
                    break;
            }
        }
    }

}

