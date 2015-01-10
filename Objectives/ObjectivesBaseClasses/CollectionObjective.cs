using UnityEngine;
using System.Collections;
using System.Xml;
public class CollectionObjective : Objectives {

    protected InventoryManager invmanager;
    /// <summary>
    /// total number of items to collect to complete this objective
    /// </summary>
    protected int NumberOfItemstoCollect=0;

    protected LanguageManager language;
    protected int NumberofFirstItem = 0;
    protected int NumberofSecondItem = 0;
    protected int NumberofThirdItem = 0;
    protected int NumberofFourthItem = 0;

    /// <summary>
    /// used by collect**Items
    /// </summary>
  //  protected string itemname;

    /// <summary>
    /// used by collectmultiple**item
    /// </summary>
  //  protected string FirstItemName;
  //  protected string SecondItemName;
    public CollectionObjective()
    {
        childlist = nodelist[2].ChildNodes;
        invmanager = InventoryManager.Instance;
        language = LanguageManager.Instance;
    }

}
