using UnityEngine;
using System.Collections;

public class CollectPHItem : CollectionObjective {
    
    public CollectPHItem(int numberofitems, string _itemname)
    {
        answerset = new System.Collections.Generic.List<string>();
       // collectedset = new System.Collections.Generic.Dictionary<string, int>();
        itemname = _itemname;
        description = childlist[1].ChildNodes[0].InnerText;
        description = description.Replace("@", numberofitems.ToString());
        description = description.Replace("!", language.GetObjectText(itemname)+"s");
        answerset.Add(itemname);
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(itemname))
        {
            NumberOfItemstoCollect = invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[itemname] + numberofitems;
        }
        else
        {
            NumberOfItemstoCollect = numberofitems;
        }
        name = "Pharmacy";
    }
    public override void CheckForCompletion()
    {
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(itemname))
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[itemname] == NumberOfItemstoCollect)
            {
                completed = true;
            }
            if (completed && Controller.B2())
            {
                Application.LoadLevel("NewCity");
            }
            if (Application.loadedLevelName == "NewCity")
            {
                CanAddNextObjective = true;
            }
        }
        base.CheckForCompletion();
    }
}
