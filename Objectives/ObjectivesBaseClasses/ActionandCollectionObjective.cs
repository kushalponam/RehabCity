using UnityEngine;
using System.Collections;

public class ActionandCollectionObjective : Objectives {

    protected GameObject LocationHolder;
    protected Vector3 targetlocation;
    public Vector3 TargetLocation
    {
        get
        {
            return targetlocation;
        }
    }

    protected string ButtonToPress;
    protected LanguageManager language;
    public ActionandCollectionObjective()
    {
        childlist = nodelist[4].ChildNodes;
        SetTargetlocation();
        language = LanguageManager.Instance;
    }
    public virtual void SetTargetlocation()
    {
        LocationHolder = GameObject.Find("Objectivelocations");
    }
}
