using UnityEngine;
using System.Collections;
using System.Xml;


public class LanguageManager : MonoBehaviour {

    [HideInInspector]
    public bool English = false;
    [HideInInspector]
    public bool Portuguese = false;

    private XmlDocument xmldoc;
    /// <summary>
    /// this gets to the node of either english or portuguese
    /// </summary>
    private XmlNodeList languagelist;
    /// <summary>
    /// this gets the child nodes of languagelist
    /// </summary>
    private XmlNodeList objectivelist;

    private static LanguageManager s_Instance = null;
    public static LanguageManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(LanguageManager)) as LanguageManager;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate ObjectiveManager");
                }
            }
            return s_Instance;
        }
    }
    
   public void SetLanguage()
    {
        TextAsset xmltext = Resources.Load("XMLData/DialoguesXML") as TextAsset;
        xmldoc = new XmlDocument();
        xmldoc.LoadXml(xmltext.text);
        if(English&&Portuguese){
            Debug.LogError("Cannot Choose both languages. Choose only one");
            Debug.Log("Two Languages are chosen. So, changing language to English");
            Portuguese = false;
        }
        languagelist = English  ? xmldoc.GetElementsByTagName("English") : languagelist;
        languagelist = Portuguese ? xmldoc.GetElementsByTagName("Portuguese") : languagelist;
        foreach (XmlNode node in languagelist)
        {
            objectivelist = node.ChildNodes;
        }
	}

    /*Look into DialoguesXML to understand next text
     * objectivelist[0] gives EnterRoom
     * objectivelist[0].childnodes[0] gives shoppingmall and innertext gives all the text inside that node
    */
    public string EnterRoomtext(string name,bool webbuild)
    {
        switch (name)
        {
            case "ShoppingMall":
                if(webbuild)
                     return objectivelist[0].ChildNodes[0].ChildNodes[13].InnerText;
                else
                     return objectivelist[0].ChildNodes[0].ChildNodes[0].InnerText;
            case "PostOffice":
                if (webbuild)
                    return objectivelist[0].ChildNodes[2].ChildNodes[6].InnerText;
                else
                    return objectivelist[0].ChildNodes[2].ChildNodes[0].InnerText;
            case "Pharmacy":
                if (webbuild)
                    return objectivelist[0].ChildNodes[1].ChildNodes[8].InnerText;
                else
                    return objectivelist[0].ChildNodes[1].ChildNodes[0].InnerText;
            case "Bank":
                if (webbuild)
                    return objectivelist[0].ChildNodes[3].ChildNodes[7].InnerText;
                else
                    return objectivelist[0].ChildNodes[3].ChildNodes[0].InnerText;
            default:
                return " Language Was Not Chosen";
        }
    }
    // used for objects that needs name
    // both by Postoffice
    public string GetObjectText(string name)
    {
        switch (name)
        {
            case "Stamp":
                return objectivelist[0].ChildNodes[2].ChildNodes[1].InnerText;
            case "Letter":
                return objectivelist[0].ChildNodes[2].ChildNodes[2].InnerText;
            case "Package":
                return objectivelist[0].ChildNodes[2].ChildNodes[3].InnerText;
            case "ChildBook":
                return objectivelist[0].ChildNodes[2].ChildNodes[4].InnerText;
            case "AdultBook":
                return objectivelist[0].ChildNodes[2].ChildNodes[5].InnerText;
            case "Milk":
                return objectivelist[0].ChildNodes[0].ChildNodes[1].InnerText;
            case "Juice":
                return objectivelist[0].ChildNodes[0].ChildNodes[2].InnerText;
            case "Bread":
                return objectivelist[0].ChildNodes[0].ChildNodes[3].InnerText;
            case "Water":
                return objectivelist[0].ChildNodes[0].ChildNodes[4].InnerText;
            case "Apple":
                return objectivelist[0].ChildNodes[0].ChildNodes[5].InnerText;
            case "Orange":
                return objectivelist[0].ChildNodes[0].ChildNodes[6].InnerText;
            case "Nutella":
                return objectivelist[0].ChildNodes[0].ChildNodes[7].InnerText;
            case "Shampoo":
                return objectivelist[0].ChildNodes[0].ChildNodes[8].InnerText;
            case "Coffee":
                return objectivelist[0].ChildNodes[0].ChildNodes[9].InnerText;
            case "Guloso":
                return objectivelist[0].ChildNodes[0].ChildNodes[10].InnerText;
            case "Yogurt":
                return objectivelist[0].ChildNodes[0].ChildNodes[11].InnerText;
            case "Butter":
                return objectivelist[0].ChildNodes[0].ChildNodes[12].InnerText;
            case "Cream":
                return objectivelist[0].ChildNodes[1].ChildNodes[1].InnerText;
            case "Pills":
                return objectivelist[0].ChildNodes[1].ChildNodes[2].InnerText;
            case "Bandaid":
                return objectivelist[0].ChildNodes[1].ChildNodes[3].InnerText;
            case "Asprin":
                return objectivelist[0].ChildNodes[1].ChildNodes[4].InnerText;
            case "Betadin":
                return objectivelist[0].ChildNodes[1].ChildNodes[5].InnerText;
            case "SunScreen":
                return objectivelist[0].ChildNodes[1].ChildNodes[6].InnerText;
            case "Beuron":
                return objectivelist[0].ChildNodes[1].ChildNodes[7].InnerText;
            default:
                return "No Lang";
        }
    }

    public string GetbankOptionsString(string name)
    {
        switch (name)
        {
            case "WithDraw":
                return objectivelist[0].ChildNodes[3].ChildNodes[1].InnerText;
            case "Consults":
                return objectivelist[0].ChildNodes[3].ChildNodes[2].InnerText;
            case "Services":
                return objectivelist[0].ChildNodes[3].ChildNodes[4].InnerText;
            case "Back":
                return objectivelist[0].ChildNodes[3].ChildNodes[5].InnerText;
            case "Quit":
                return objectivelist[0].ChildNodes[3].ChildNodes[6].InnerText;
            case "Payments":
                return objectivelist[0].ChildNodes[3].ChildNodes[3].ChildNodes[0].InnerText;
            case "Electricity":
                return objectivelist[0].ChildNodes[3].ChildNodes[3].ChildNodes[1].InnerText;
            case "Water":
                return objectivelist[0].ChildNodes[3].ChildNodes[3].ChildNodes[2].InnerText;
            case "Telephone":
                return objectivelist[0].ChildNodes[3].ChildNodes[3].ChildNodes[3].InnerText;
            default:
                return "No Lang";
        }
    }
    public string WellDoneText()
    {
        return objectivelist[1].ChildNodes[0].InnerText;
    }
    public string ScoreText()
    {
        return objectivelist[1].ChildNodes[1].InnerText;
    }
    public string LevelCompleteString()
    {
        return objectivelist[1].ChildNodes[2].InnerText;
    }

}
