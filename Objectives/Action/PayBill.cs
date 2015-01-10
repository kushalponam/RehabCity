using UnityEngine;
using System.Collections;

public class PayBill : ActionObjective {
    
    public PayBill(string BillToPay)
    {
        description = childlist[1].InnerText;
        description = description.Replace("@", BillToPay);
        ButtonToPress = BillToPay;
        name = "Bank";
    }
    public override void CheckForCompletion()
    {
        if (BankManager.ButtonSelected == ButtonToPress)
        {
            completed = true;
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
