using UnityEngine;
using System.Collections;

public class GotoBankandWithdraw : ActionandCollectionObjective {

    public GotoBankandWithdraw(int amount)
    {
        description = childlist[0].InnerText;
        ButtonToPress = amount.ToString();
        name = "Bank";
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
            if (BankManager.ButtonSelected == ButtonToPress)
            {
                completed = true;
                BankManager.ButtonSelected = null;
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
