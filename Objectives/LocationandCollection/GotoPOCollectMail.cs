using UnityEngine;
using System.Collections;

public class GotoPOCollectMail : LocationandCollectionObjective {

    public GotoPOCollectMail(int numbertocollect, bool moreabstract)
    {
        answerset = new System.Collections.Generic.List<string>();
        if (moreabstract)
        {
            description = childlist[10].InnerText;
            answerset.Add("AdultBook");
        }
        else
        {
            description = childlist[7].InnerText;
            answerset.Add("Letter");
        }
        description = description.Replace("@",numbertocollect.ToString());
       
        numberofitemsforthisobjective = numbertocollect;
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
