using UnityEngine;
using System.Collections;

public class CollectMultiplePHItems : CollectionObjective {

    public CollectMultiplePHItems(int firstitem, string firstitemname, int seconditem, string seconditemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        collectedset = new System.Collections.Generic.Dictionary<string, int>();
        description = childlist[2].InnerText;
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

    public CollectMultiplePHItems(int firstitem, string firstitemname, int seconditem, string seconditemname, int thirditem,
                                   string thirditemname, int fourthitem, string fourthitemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        collectedset = new System.Collections.Generic.Dictionary<string, int>();
        description = childlist[3].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#", language.GetObjectText(firstitemname) + "s");
        description = description.Replace("!", seconditem.ToString());
        description = description.Replace("$", language.GetObjectText(seconditemname) + "s");
        FirstItemName = firstitemname;
        SecondItemName = seconditemname;
        ThirdItemName = thirditemname;
        FourthItemName = fourthitemname;
        answerset.Add(firstitemname);
        answerset.Add(seconditemname);
        answerset.Add(thirditemname);
        answerset.Add(fourthitemname);
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

        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(thirditemname))
        {
            NumberofThirdItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[thirditemname] + thirditem;
        }
        else
        {
            NumberofThirdItem = thirditem;
        }

        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(fourthitemname))
        {
            NumberofFourthItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[fourthitemname] + fourthitem;
        }
        else
        {
            NumberofFourthItem = fourthitem;
        }
        name = "Pharmacy";
    }


     public override void CheckForCompletion()
    {
     /*   if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(FirstItemName) &&
            invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart.ContainsKey(SecondItemName))
        {
            if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[FirstItemName] >= NumberofFirstItem &&
                invmanager.GetCategoryList.Find(Category => Category.ToString() == "Pharmacy").Cart[SecondItemName] >= NumberofSecondItem)
            {
                completed = true;
            }
            if (completed && Controller.B2())
            {
                Application.LoadLevel(1);
            }
            if (Application.loadedLevel == 1)
            {
                CanAddNextObjective = true;
            }
        }
        base.CheckForCompletion();*/
        if (collectedset.Count == 0) return;
        if (answerset.Count == 0)
        {
            completed = true;
        }
        if (FirstItemName != null && CollectedSet.ContainsKey(FirstItemName) && CollectedSet[FirstItemName] >= NumberofFirstItem)
        {
            answerset.Remove(FirstItemName);
            Debug.Log("removing first itemname");
        }
        if (SecondItemName != null && CollectedSet.ContainsKey(SecondItemName) && CollectedSet[SecondItemName] >= NumberofSecondItem)
        {
            answerset.Remove(SecondItemName);
            Debug.Log("removing first itemname");
        }
        if (ThirdItemName != null && CollectedSet.ContainsKey(ThirdItemName) && CollectedSet[ThirdItemName] >= NumberofThirdItem)
        {
            answerset.Remove(ThirdItemName);
        }
        if (FourthItemName != null && CollectedSet.ContainsKey(FourthItemName) && CollectedSet[FourthItemName] >= NumberofFourthItem)
        {
            answerset.Remove(FourthItemName);
        }
        if (completed && Controller.B2())
        {
            Application.LoadLevel("NewCity");
        }
        if (Application.loadedLevelName == "NewCity")
        {
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }
}
