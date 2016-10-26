using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour {

    int lifeNumber = 20;
    int infectNumber = 0;
    int wNumber = 0;
    int uNumber = 0;
    int bNumber = 0;
    int rNumber = 0;
    int gNumber = 0;
    int cNumber = 0;
    int sNumber = 0;

    GameObject lifePanel;
    GameObject infectPanel;
    GameObject wPanel;
    GameObject uPanel;
    GameObject bPanel;
    GameObject rPanel;
    GameObject gPanel;
    GameObject cPanel;
    GameObject sPanel;

    void Start () {
        lifePanel = GameObject.Find("LifeNumber");
        infectPanel = GameObject.Find("InfectNumber");
        wPanel = GameObject.Find("WNumber");
        uPanel = GameObject.Find("UNumber");
        bPanel = GameObject.Find("BNumber");
        rPanel = GameObject.Find("RNumber");
        gPanel = GameObject.Find("GNumber");
        cPanel = GameObject.Find("CNumber");
        sPanel = GameObject.Find("SNumber");

    
    }

    void Update()
    {
        UpdateValues();
    }



    private void UpdateValues()
    {
        lifePanel.GetComponent<Text>().text = lifeNumber.ToString();
        infectPanel.GetComponent<Text>().text = infectNumber.ToString();
        wPanel.GetComponent<Text>().text = wNumber.ToString();
        uPanel.GetComponent<Text>().text = uNumber.ToString();
        bPanel.GetComponent<Text>().text = bNumber.ToString();
        rPanel.GetComponent<Text>().text = rNumber.ToString();
        gPanel.GetComponent<Text>().text = gNumber.ToString();
        cPanel.GetComponent<Text>().text = cNumber.ToString();
        sPanel.GetComponent<Text>().text = sNumber.ToString();
    }
	
    public void PlusLife() { this.lifeNumber += 1; }
    public void MinusLife() { this.lifeNumber -= 1; }

    public void PlusInfect() { this.infectNumber += 1; }
    public void MinusInfect() { this.infectNumber -= 1; }

    public void WPlus() { this.wNumber += 1; }
    public void WMinus() { this.wNumber -= 1; }

    public void UPlus() { this.uNumber += 1; }
    public void UMinus() { this.uNumber -= 1; }

    public void BPlus() { this.bNumber += 1; }
    public void BMinus() { this.bNumber -= 1; }

    public void RPlus() { this.rNumber += 1; }
    public void RMinus() { this.rNumber -= 1; }

    public void GPlus() { this.gNumber += 1; }
    public void GMinus() { this.gNumber -= 1; }

    public void CPlus() { this.cNumber += 1; }
    public void CMinus() { this.cNumber -= 1; }

    public void SPlus() { this.sNumber += 1; }
    public void SMinus() { this.sNumber -= 1; }




}

