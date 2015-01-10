using UnityEngine;
using System.Collections;

public class CollectMultipleSMItems : CollectionObjective {

    public CollectMultipleSMItems(int firstitem, string firstitemname, int seconditem, string seconditemname)
    {
        answerset = new System.Collections.Generic.List<string>();
        collectedset = new System.Collections.Generic.Dictionary<string, int>();
        description = childlist[2].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#",language.GetObjectText(firstitemname)+"s");
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

    public CollectMultipleSMItems(int firstitem, string firstitemname, int seconditem, string seconditemname, int thirditem, string thirditemname,
                                  int fourthitem, string fourthitemname)
    {
      
        answerset = new System.Collections.Generic.List<string>();
        collectedset = new System.Collections.Generic.Dictionary<string, int>();
        description = childlist[3].InnerText;
        description = description.Replace("@", firstitem.ToString());
        description = description.Replace("#", language.GetObjectText(firstitemname) + "s");
        description = description.Replace("!", seconditem.ToString());
        description = description.Replace("$", language.GetObjectText(seconditemname) + "s");
        description = description.Replace("%",thirditem.ToString());
        description = description.Replace("^", language.GetObjectText(thirditemname)+ "s");
        description = description.Replace("|",fourthitem.ToString());
        description = description.Replace("*",language.GetObjectText(fourthitemname)+"s");

        FirstItemName = firstitemname;
        SecondItemName = seconditemname;
        ThirdItemName = thirditemname;
        FourthItemName = fourthitemname;
        answerset.Add(firstitemname);
        answerset.Add(seconditemname);
        answerset.Add(thirditemname);
        answerset.Add(fourthitemname);
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

        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(thirditemname))
        {
            NumberofThirdItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[thirditemname] + thirditem;
        }
        else
        {
            NumberofThirdItem = thirditem;
        }

        if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(fourthitemname))
        {
            NumberofFourthItem = invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[fourthitemname] + fourthitem;
        }
        else
        {
            NumberofFourthItem = fourthitem;
        }

        name = "ShoppingMall";
    }

    public override void CheckForCompletion()
    {
        /*  if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(FirstItemName)&&
              invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart.ContainsKey(SecondItemName))
          {
              if (invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[FirstItemName] >= NumberofFirstItem &&
                  invmanager.GetCategoryList.Find(Category => Category.ToString() == "ShoppingMall").Cart[SecondItemName] >= NumberofSecondItem)
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
          base.CheckForCompletion();
      }*/
      //  if(collectedSet.c)
        if (collectedset.Count == 0) return;
        if (answerset.Count == 0)
        {
            completed = true;
        }
        if (FirstItemName!=null && CollectedSet.ContainsKey(FirstItemName) &&CollectedSet[FirstItemName] >= NumberofFirstItem)
        {
            answerset.Remove(FirstItemName);
        }
        if (SecondItemName != null && CollectedSet.ContainsKey(SecondItemName) && CollectedSet[SecondItemName] >= NumberofSecondItem)
        {
            answerset.Remove(SecondItemName);
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
