using UnityEngine;
using System.Collections;

public class GotoPOandCollect : LocationandCollectionObjective {

    public GotoPOandCollect(int firstitem, string firstitemname, int seconditem, string seconditemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        description = childlist[2].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#", language.GetObjectText(firstitemname)+"s");
        description = description.Replace("!", seconditem.ToString());
        description = description.Replace("$", language.GetObjectText(seconditemname)+"s");
        FirstItemName = firstitemname;
        SecondItemName = seconditemname;
        answerset.Add(firstitemname);
        answerset.Add(seconditemname);
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart.ContainsKey(firstitemname))
        {
            NumberofFirstItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart[firstitemname] + firstitem;
        }
        else
        {
            NumberofFirstItem = firstitem;
        }
        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart.ContainsKey(seconditemname))
        {
            NumberofSecondItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart[seconditemname] + seconditem;
        }
        else
        {
            NumberofSecondItem = seconditem;
        }
        name = "PostOffice";
    }
    public override void SetTargetlocation()
    {
        base.SetTargetlocation();
        targetlocation = LocationHolder.transform.FindChild("PostOffice").transform.position;
        location = TargetLocation;
    }
    public override void CheckForCompletion()
    {
        if (Application.loadedLevelName == "PostOffice")
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart.ContainsKey(FirstItemName) &&
               invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart.ContainsKey(SecondItemName))
            {
                if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart[FirstItemName] >= NumberofFirstItem &&
                    invmanager.GetCategoryList.Find(Category => Category.ToString() == "PostOffice").Cart[SecondItemName] >= NumberofSecondItem)
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
