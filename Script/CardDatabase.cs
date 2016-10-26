using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;


public class CardDatabase : MonoBehaviour
{

    private List<Card> database = new List<Card>();
    private JsonData cardData;



    void Start()
    {

        cardData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Json/SomeSets.json"));
        BuildDB();

    }

    public Card FetchByName(string name)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].name.Equals(name))
            {
                //Debug.Log("Achei " + database[i].name);
                return database[i];
            }
        }
        Debug.Log("failed");
        return null;
    }

    void BuildDB()
    {

        for (int s = 0; s < cardData.Count; s++)
        {
            if (cardData[s].Keys.Contains("cards"))
                for (int i = 0; i < cardData[s]["cards"].Count; i++)
                {

                    if (cardData[s]["cards"][i].Keys.Contains("name"))
                        database.Add(new Card(cardData[s]["cards"][i]["name"].ToString()));
                    if (cardData[s]["cards"][i].Keys.Contains("names"))
                        for (int k = 0; k < cardData[s]["cards"][i]["names"].Count; k++)
                            database[i].names.Add(cardData[s]["cards"][i]["names"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("manaCost"))
                        database[i].manaCost = cardData[s]["cards"][i]["manaCost"].ToString();
                    if (cardData[s]["cards"][i].Keys.Contains("cmc"))
                        database[i].cmc = (int)cardData[s]["cards"][i]["cmc"];
                    if (cardData[s]["cards"][i].Keys.Contains("colors"))
                        for (int k = 0; k < cardData[s]["cards"][i]["colors"].Count; k++)
                            database[i].colors.Add(cardData[s]["cards"][i]["colors"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("colorIdentity"))
                        for (int k = 0; k < cardData[s]["cards"][i]["colorIdentity"].Count; k++)
                            database[i].colorIdentity.Add(cardData[s]["cards"][i]["colorIdentity"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("type"))
                        database[i].type = cardData[s]["cards"][i]["type"].ToString().Replace('�', '—');
                    if (cardData[s]["cards"][i].Keys.Contains("supertypes"))
                        for (int k = 0; k < cardData[s]["cards"][i]["supertypes"].Count; k++)
                            database[i].supertypes.Add(cardData[s]["cards"][i]["supertypes"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("types"))
                        for (int k = 0; k < cardData[s]["cards"][i]["types"].Count; k++)
                            database[i].types.Add(cardData[s]["cards"][i]["types"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("subtypes"))
                        for (int k = 0; k < cardData[s]["cards"][i]["subtypes"].Count; k++)
                            database[i].subtypes.Add(cardData[s]["cards"][i]["subtypes"][k].ToString());
                    if (cardData[s]["cards"][i].Keys.Contains("text"))
                        database[i].text = cardData[s]["cards"][i]["text"].ToString();
                    if (cardData[s]["cards"][i].Keys.Contains("power"))
                        database[i].power = cardData[s]["cards"][i]["power"].ToString();
                    if (cardData[s]["cards"][i].Keys.Contains("toughness"))
                        database[i].toughness = cardData[s]["cards"][i]["toughness"].ToString();
                    if (cardData[s]["cards"][i].Keys.Contains("loyality"))
                        database[i].loyality = (int)cardData[s]["cards"][i]["loyality"];
                    //pra buscar imagens
                    if (cardData[s]["cards"][i].Keys.Contains("multiverseid"))
                        database[i].multiverseid = (int)cardData[s]["cards"][i]["multiverseid"];
                    if (cardData[s].Keys.Contains("magicCardsInfoCode"))
                        database[i].magiccardsedition = cardData[s]["magicCardsInfoCode"].ToString();   
                    if (cardData[s]["cards"][i].Keys.Contains("mciNumber"))
                        database[i].magiccardseditionnumber = cardData[s]["cards"][i]["mciNumber"].ToString();     

                }



        }

    }





}
public class Card
{
    public string name;
    public List<string> names = new List<string>();
    public string manaCost;
    public int cmc;
    public List<string> colors = new List<string>();
    public List<string> colorIdentity = new List<string>();
    public string type;
    public List<string> supertypes = new List<string>();
    public List<string> types = new List<string>();
    public List<string> subtypes = new List<string>();
    public string text;
    public string power;
    public string toughness;
    public int loyality;
    //pra buscar imagens
    public int multiverseid;
    public string magiccardsedition;       
    public string magiccardseditionnumber;  

    public Card(string name)
    {
        this.name = name;
    }

   
}







