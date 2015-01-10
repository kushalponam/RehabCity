using UnityEngine;
using System.Collections;

public class GotoPHandgetMeds : LocationandCollectionObjective {

    public GotoPHandgetMeds(int numberofitems,bool moreabstract)
    {
        answerset = new System.Collections.Generic.List<string>();
        if (moreabstract)
        {
            description = childlist[9].InnerText;
            answerset.Add("Benuron");
            answerset.Add("Asprin");
        }
        else
        {
            description = childlist[6].InnerText;
            answerset.Add("Bandaid");
        }
        description = description.Replace("@", numberofitems.ToString());
       
        numberofitemsforthisobjective = numberofitems;
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
            if (NumberofItemsCollected >= numberofitemsforthisobjective)
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
