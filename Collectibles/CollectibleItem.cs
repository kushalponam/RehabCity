using UnityEngine;
using System.Collections;
using System.Xml;
public class CollectibleItem : MonoBehaviour {
	
    // Set by GameManager
    public Vector3 MovePoint;
    public bool CanMove=false;
    public enum type
	{
		ShoppingMall,
		Utensils,
		PostOffice,
        Pharmacy
	}
	
	public enum itemname
	{
		Milk,
		Juice,
		Chocopic,
        Bread,
        Water,
        Kellogs,
        Coke,
        Apple,
        Orange,
        Nutella,
        Garnier,
        Coffee,
        Guloso,
        Yogurt,
        Mantega,
        Macoroni,
        Letter,
        Package,
        Stamp,
        ChildBook,
        AdultBook,
        Cream,
        Pills,
        Bandaid,
        Asprin,
        Betadin,
        Sunscreen,
        Benuron
	}
    public type ItemType;
    public itemname Itemname;
    private LanguageManager language;
    private TextMesh text;
    public int randomnumber;
    void Start()
    {
        MovePoint = Vector3.zero;
        if (ItemType == type.PostOffice)
        {
            language = LanguageManager.Instance;
            text = transform.FindChild("Text").GetComponent<TextMesh>();
            text.text = language.GetObjectText(Itemname.ToString());
        }
    }

    /// <summary>
    /// used by item shuffler. Each object get a random number and item shuffler sorts it
    /// </summary>
    public void GenerateRandomNumber()
    {
        randomnumber = Random.Range(0, 100);
    }

    void Update()
    {
        if (CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoint, 3f);
        }
    }


}
