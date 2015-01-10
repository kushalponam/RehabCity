using UnityEngine;
using System.Collections;

public class GotoPostoffice : LocationObjective {

	public GotoPostoffice()
	{
		description = childlist[1].InnerText;
        name = "PostOffice";
        RequiredSceneToSpawn = 0;
	}
	
	public override void SetTargetlocation()
	{
		base.SetTargetlocation();
		targetlocation = LocationHolder.transform.FindChild("PostOffice").transform.position;
		location = TargetLocation;
	}
	
	public override void CheckForCompletion ()
	{
        if (Application.loadedLevelName == "PostOffice")
        {
            completed = true;
            CanAddNextObjective = true;
        }
		base.CheckForCompletion();
	}
	
}
