using UnityEngine;
using System.Collections;

public class GotoSMandgetFood : LocationandCollectionObjective
{
    public GotoSMandgetFood(int numberofitems, bool breakfast, bool lunch, bool snacks)
    {
        answerset = new System.Collections.Generic.List<string>();
        if (breakfast)
        {
            description = childlist[3].InnerText;
            answerset.Add("Milk");
            answerset.Add("Bread");
            answerset.Add("Juice");
        }
        else if (lunch)
        {
            description = childlist[4].InnerText;
            answerset.Add("Bread");
            answerset.Add("Macoroni");
            answerset.Add("Guloso");
        }
        else if (snacks)
        {
            description = childlist[5].InnerText;
            answerset.Add("Orange");
            answerset.Add("Apple");
            answerset.Add("Yogurt");
        }
        description = description.Replace("@", numberofitems.ToString());
        numberofitemsforthisobjective = numberofitems;
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
            if(NumberofItemsCollected>=numberofitemsforthisobjective)
               {
                    completed = true;
                }
                if (completed && Controller.B2())
                {
                    Application.LoadLevel("NewCity");
                }
        }
        if (completed && Application.loadedLevelName == "NewCity")
        {
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }
}
