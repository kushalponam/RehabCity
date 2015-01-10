using UnityEngine;
using System.Collections;

public class GotoPHandCollect : LocationandCollectionObjective {

    public GotoPHandCollect(int firstitem, string firstitemname, int seconditem, string seconditemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        description = childlist[1].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#", language.GetObjectText(firstitemname)+"s");
        description = description.Replace("!", seconditem.ToString());
        description = description.Replace("$", language.GetObjectText(seconditemname)+"s");
        FirstItemName = firstitemname;
        SecondItemName = seconditemname;
        answerset.Add(firstitemname);
        answerset.Add(seconditemname);
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(firstitemname))
        {
            NumberofFirstItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[firstitemname] + firstitem;
        }
        else
        {
            NumberofFirstItem = firstitem;
        }
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(seconditemname))
        {
            NumberofSecondItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[seconditemname] + seconditem;
        }
        else
        {
            NumberofSecondItem = seconditem;
        }
        name = "Pharmacy";
    }
    public override void SetTargetlocation()
    {
        base.SetTargetlocation();
        targetlocation = LocationHolder.transform.FindChild("Pharmacy").transform.position;
        location = TargetLocation;
    }
    public override void CheckForCompletion()
    {
        if (Application.loadedLevelName == "Pharmacy")
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(FirstItemName) &&
               invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(SecondItemName))
            {
                if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[FirstItemName] >= NumberofFirstItem &&
                    invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[SecondItemName] >= NumberofSecondItem)
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
