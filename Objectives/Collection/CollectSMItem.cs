using UnityEngine;
using System.Collections;

public class CollectSMItem : CollectionObjective {

    public CollectSMItem(int numberofitems, string _itemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        itemname = _itemname;
        description = childlist[1].ChildNodes[0].InnerText;
        description = description.Replace("@", numberofitems.ToString());
        description = description.Replace("!", language.GetObjectText(itemname)+"s");
        answerset.Add(itemname);
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(itemname))
        {
            NumberOfItemstoCollect = invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[itemname] + numberofitems;
        }
        else
        {
            NumberOfItemstoCollect = numberofitems;
        }
        name = "ShoppingMall";
    }
    public override void CheckForCompletion()
    {
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(itemname))
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[itemname] == NumberOfItemstoCollect)
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
