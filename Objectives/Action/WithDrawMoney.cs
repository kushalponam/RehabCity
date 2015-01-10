using UnityEngine;
using System.Collections;

public class WithDrawMoney : ActionObjective {
    
    public WithDrawMoney(int amount)
    {
        description = childlist[0].InnerText;
        description = description.Replace("@", amount.ToString());
        RequiredSceneToSpawn = 4;
        ButtonToPress = amount.ToString();
        name = "Bank";
    }
    public override void CheckForCompletion()
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
        if (Application.loadedLevelName == "NewCity")
        {
            CanAddNextObjective = true;
        }
        base.CheckForCompletion();
    }
}
