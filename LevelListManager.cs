using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelListManager {
    public static Dictionary<int, List<Objectives>> LevelList;
    public static void AddLevelsToList(int Level)
    {
        LevelList = new Dictionary<int, List<Objectives>>();
        switch (Level)
        {
            case 1:
                LevelList.Add(0, GetLevel1ListA());
                LevelList.Add(1, GetLevel1ListB());
                LevelList.Add(2, GetLevel1ListC());
                LevelList.Add(3, GetLevel1ListD());
                break;
            case 2:
                LevelList.Add(0, GetLevel2ListA());
                LevelList.Add(1, GetLevel2ListB());
                LevelList.Add(2, GetLevel2ListC());
                LevelList.Add(3, GetLevel2ListD());
                break;
            case 3:
                LevelList.Add(0, GetLevel3ListA());
                LevelList.Add(1, GetLevel3ListB());
                LevelList.Add(2, GetLevel3ListC());
                LevelList.Add(3, GetLevel3ListD());
                break;
            case 4:
                LevelList.Add(0, GetLevel4ListA());
                LevelList.Add(1, GetLevel4ListB());
                LevelList.Add(2, GetLevel4ListC());
                LevelList.Add(3, GetLevel4ListD());
                break;
            case 5:
                LevelList.Add(0, GetLevel5ListA());
                LevelList.Add(1, GetLevel5ListB());
                break;
            default:
                break;
        }
    }
    static List<Objectives> GetLevel1ListA()
    {
        List<Objectives> list = new List<Objectives>();
        list.Add(new GotoShopping());
        list.Add(new CollectSMItem(3,"Milk"));
        return list;
    }
    static List<Objectives> GetLevel1ListB()
    {
        List<Objectives> list = new List<Objectives>();
        list.Add(new GotoPostoffice());
        list.Add(new CollectPOItem(3, "Package"));
        return list;
    }
    static List<Objectives> GetLevel1ListC()
    {
        List<Objectives> list = new List<Objectives>();
        list.Add(new GotoPharmacy());
        list.Add(new CollectPHItem(3, "Cream"));
        return list;
    }
    static List<Objectives> GetLevel1ListD()
    {
        List<Objectives> list = new List<Objectives>();
        list.Add(new GotoBank());
        list.Add(new WithDrawMoney(20));
        return list;
    }
    
     static List<Objectives> GetLevel2ListA()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPharmacy());
         list.Add(new CollectPHItem(3, "Pills"));
         list.Add(new GotoPostoffice());
         list.Add(new CollectPOItem(3, "Stamp"));
    
         return list;
     }
     static List<Objectives> GetLevel2ListB()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoBank());
         list.Add(new WithDrawMoney(40));
         list.Add(new GotoShopping());
         list.Add(new CollectSMItem(3, "Juice"));
         return list;
     }
     static List<Objectives> GetLevel2ListC()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoShopping());
         list.Add(new CollectSMItem(3, "Juice"));
         list.Add(new GotoBank());
         list.Add(new WithDrawMoney(20));
         return list;
     }
     static List<Objectives> GetLevel2ListD()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPostoffice());
         list.Add(new CollectPOItem(3, "Stamp"));
         list.Add(new GotoShopping());
         list.Add(new CollectSMItem(3, "Juice"));
         return list;
     }
     static List<Objectives> GetLevel3ListA()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPostoffice());
         list.Add(new CollectMultiplePOItems(3, "Package", 2, "Stamp"));
         list.Add(new GotoShopping());
         list.Add(new CollectMultipleSMItems(3, "Water", 2, "Juice"));
         list.Add(new GotoBank());
         list.Add(new PayBill(LanguageManager.Instance.GetbankOptionsString("Electricity")));
         list.Add(new GotoPharmacy());
         list.Add(new CollectMultiplePHItems(2,"Bandaid",3,"Pills"));
         return list;
     }
     static List<Objectives> GetLevel3ListB()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoShopping());
         list.Add(new CollectMultipleSMItems(3, "Water", 2, "Juice"));
         list.Add(new GotoPostoffice());
         list.Add(new CollectMultiplePOItems(3, "Package", 2, "Stamp"));
         list.Add(new GotoBank());
         list.Add(new PayBill(LanguageManager.Instance.GetbankOptionsString("Electricity")));
         list.Add(new GotoPharmacy());
         list.Add(new CollectMultiplePHItems(2, "Cream", 3, "Bandaid"));
         return list;
     }
     static List<Objectives> GetLevel3ListC()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPharmacy());
         list.Add(new CollectMultiplePHItems(2, "Bandaid", 3, "Pills"));
         list.Add(new GotoPostoffice());
         list.Add(new CollectMultiplePOItems(3, "Package", 2, "Stamp"));
         list.Add(new GotoShopping());
         list.Add(new CollectMultipleSMItems(3, "Water", 2, "Juice",3,"Milk",4,"Juice"));
         list.Add(new GotoBank());
         list.Add(new PayBill(LanguageManager.Instance.GetbankOptionsString("Electricity")));
         return list;
     }
     static List<Objectives> GetLevel3ListD()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoBank());
         list.Add(new PayBill(LanguageManager.Instance.GetbankOptionsString("Electricity")));
         list.Add(new GotoPostoffice());
         list.Add(new CollectMultiplePOItems(3, "Package", 2, "Stamp"));
         list.Add(new GotoShopping());
         list.Add(new CollectMultipleSMItems(3, "Milk", 2, "Water"));
         list.Add(new GotoPharmacy());
         list.Add(new CollectMultiplePHItems(2, "Cream", 3, "Bandaid"));
         return list;
     }
     static List<Objectives> GetLevel4ListA()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoSMandCollect(3, "Water", 2, "Juice"));
         list.Add(new GotoPHandCollect(2, "Bandaid", 3, "Cream"));
         list.Add(new GotoPOandCollect(4,"Package",2,"Letter"));
         return list;
     }
     static List<Objectives> GetLevel4ListB()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPHandCollect(3, "Pills", 2, "Bandaid"));
         list.Add(new GotoPOandCollect(4, "Package", 2, "Letter"));
         list.Add(new GotoSMandCollect(3, "Water", 2, "Juice"));
         return list;
     }
     static List<Objectives> GetLevel4ListC()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPOandCollect(4, "Package", 2, "Letter"));
         list.Add(new GotoSMandCollect(3, "Water", 2, "Juice"));
         list.Add(new GotoPHandCollect(2, "Bandaid", 3, "Cream"));
         
         return list;
     }
     static List<Objectives> GetLevel4ListD()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoSMandCollect(3, "Water", 2, "Juice"));
         list.Add(new GotoPHandCollect(2, "Pills", 3, "Cream"));
         list.Add(new GotoPOandCollect(4, "Package", 2, "Letter"));
         return list;
     }
     static List<Objectives> GetLevel5ListA()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoBankandWithdraw(20));
         list.Add(new GotoSMandgetFood(2,true,false,false));
         list.Add(new GotoPHandgetMeds(2,false));
         list.Add(new GotoPOCollectMail(3,false));
         return list;
     }
     static List<Objectives> GetLevel5ListB()
     {
         List<Objectives> list = new List<Objectives>();
         list.Add(new GotoPHandgetMeds(2,true));
         list.Add(new GotoPOCollectMail(3,true));
         list.Add(new GotoSMandgetFood(2,false,true,false));
         list.Add(new GotoBankandWithdraw(20));
         return list;
     }

}
