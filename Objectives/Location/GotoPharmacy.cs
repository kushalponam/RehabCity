using UnityEngine;
using System.Collections;

public class GotoPharmacy : LocationObjective{

    public GotoPharmacy()
    {
        description = childlist[2].InnerText;
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
            completed = true;
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }
}
