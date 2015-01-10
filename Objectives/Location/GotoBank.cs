using UnityEngine;
using System.Collections;

public class GotoBank : LocationObjective {

    public GotoBank()
    {
        description = childlist[3].InnerText;
        name = "Bank";
        RequiredSceneToSpawn = 0;
    }
    public override void SetTargetlocation()
    {
        base.SetTargetlocation();
        targetlocation = LocationHolder.transform.FindChild("Bank").transform.position;
        location = TargetLocation;
    }
    public override void CheckForCompletion()
    {
        if (Application.loadedLevelName == "Bank")
        {
            completed = true;
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }
}
