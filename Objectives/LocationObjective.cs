using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
public class LocationObjective : Objectives {
	
	
	protected GameObject LocationHolder;
	protected Vector3 targetlocation;
    protected InventoryManager invmanager;
    /// <summary>
    /// total number of items to collect to complete this objective
    /// </summary>
    protected int NumberOfItemstoCollect = 0;

    protected LanguageManager language;
    protected int NumberofFirstItem = 0;
    protected int NumberofSecondItem = 0;
  
	public Vector3 TargetLocation
	{
		get
		{
			return targetlocation;
		}
	}
	
	public LocationObjective()
	{
        childlist = nodelist[0].ChildNodes;  
        invmanager = InventoryManager.Instance;
        language = LanguageManager.Instance;
        SetTargetlocation();
	}
	 
	public virtual void SetTargetlocation()
	{
		LocationHolder = GameObject.Find("Objectivelocations");
	}
	
    
}
