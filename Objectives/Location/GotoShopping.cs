using UnityEngine;
using System.Collections;

public class GotoShopping : LocationObjective {

	public GotoShopping()
	{
		description = childlist[0].InnerText;
        name = "ShoppingMall";
        RequiredSceneToSpawn = 0;
	}
	
	public override void SetTargetlocation()
	{
		base.SetTargetlocation();
		targetlocation = LocationHolder.transform.FindChild("ShoppingMall").transform.position;
		location = TargetLocation;
	}
	public override void CheckForCompletion ()
	{
        if (Application.loadedLevelName == "SuperMarketNew")
        {
            completed = true;
            CanAddNextObjective = true;
        }
		base.CheckForCompletion();
	}
}
