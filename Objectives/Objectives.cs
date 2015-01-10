using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class Objectives {

    /// <summary>
    /// used to store the names of objects that have to be collected in this 
    /// objective
    /// </summary>
    protected List<string> answerset;
    public List<string> AnswerSet
    {
        get
        {
            return answerset;
        }
    }

    protected Dictionary<string, int> collectedset;
    public Dictionary<string, int> CollectedSet
    {
        get
        {
            return collectedset;
        }
    }

	protected string description;
	protected Vector3 location;
	protected bool completed;
	
	protected TextAsset xmltext;
	protected XmlDocument xmldoc;
    /// <summary>
    /// gets to the node of either english or portuguese
    /// </summary>
    protected XmlNodeList languagelist;
    /// <summary>
    /// gets to the node of eiter LocationObjective, ActionObjective, CollectionObjective
    /// </summary>
	protected static XmlNodeList nodelist; 
    /// <summary>
    /// gets to the node of individual objectives
    /// </summary>
	protected XmlNodeList childlist;
	
	protected Transform player;
    /// <summary>
    /// used by objindicator to get the texture based on objective name
    /// </summary>
    public string name;
    /// <summary>
    ///  boolean to check if an objective can be pushed to the queue
    /// </summary>
    public bool CanbePushed=false ;

    /// <summary>
    /// stores the correct scene number to push this objective to the queue
    /// </summary>
    public int RequiredSceneToSpawn=-1;
    /// <summary>
    /// This is used when an objective is completed and if next objective is in other scene then, it should wait.
    /// </summary>
    public bool CanAddNextObjective = false;

    /// <summary>
    /// used by abstract objectives
    /// </summary>
    public int NumberofItemsCollected = 0;


    #region UsedbyCollectionObjectives
    public string itemname;
    public string FirstItemName;
    public string SecondItemName;
    public string ThirdItemName;
    public string FourthItemName;

    #endregion

    public string Description
	{
		get
		{
			return description;
		}
	}
	
	public Vector3 Location
	{
		get
		{
			return location;
		}
	}
	public bool Completed
	{
		get
		{
			return completed;
		}
	}
	public Objectives()
	{
		if(player==null)player = GameObject.FindGameObjectWithTag("Player").transform;
		this.description = "None";
        name = "None";
        itemname = null;
        FirstItemName = null;
        SecondItemName = null;
        NumberofItemsCollected = 0;
		//Debug.Log("Created Objective"+this);
	}
    // called from ObjectiveManager
    public void SetLanguage(bool English, bool Portuguese)
    {
        TextAsset xmltext = Resources.Load("XMLData/ObjectiveXML") as TextAsset;
        xmldoc = new XmlDocument();
        xmldoc.LoadXml(xmltext.text);
        languagelist = English ? xmldoc.GetElementsByTagName("English"):languagelist;
        languagelist = Portuguese ? xmldoc.GetElementsByTagName("Portuguese"): languagelist;
        foreach (XmlNode node in languagelist)
        {
            nodelist = node.ChildNodes;
        }
    }

	public virtual void CheckForCompletion()
	{}
}
