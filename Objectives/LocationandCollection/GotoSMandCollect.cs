using UnityEngine;
using System.Collections;

public class GotoSMandCollect : LocationandCollectionObjective {

    public GotoSMandCollect(int firstitem, string firstitemname, int seconditem, string seconditemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        description = childlist[0].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#", language.GetObjectText(firstitemname)+"s");
        description = description.Replace("!", seconditem.ToString());
        description = description.Replace("$", language.GetObjectText(seconditemname)+"s");
        FirstItemName = firstitemname;
        SecondItemName = seconditemname;
        answerset.Add(firstitemname);
        answerset.Add(seconditemname);
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(firstitemname))
        {
            NumberofFirstItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[firstitemname] + firstitem;
        }
        else
        {
            NumberofFirstItem = firstitem;
        }
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(seconditemname))
        {
            NumberofSecondItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[seconditemname] + seconditem;
        }
        else
        {
            NumberofSecondItem = seconditem;
        }
        name = "ShoppingMall";
    }
    public override void SetTargetlocation()
    {
        base.SetTargetlocation();
        targetlocation = LocationHolder.transform.FindChild("ShoppingMall").transform.position;
        location = TargetLocation;
    }
    public override void CheckForCompletion()
    {
        if (Application.loadedLevelName == "SuperMarketNew")
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(FirstItemName) &&
               invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(SecondItemName))
                {
                    if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[FirstItemName] >= NumberofFirstItem &&
                        invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[SecondItemName] >= NumberofSecondItem)
                    {
                        completed = true;
                    }
                    if (completed && Controller.B2())
                    {
                        Application.LoadLevel("NewCity");
                    }
            }
        }
        if (completed && Application.loadedLevelName == "NewCity")
        {
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }

}
