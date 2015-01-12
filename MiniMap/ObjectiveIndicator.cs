using UnityEngine;
using System.Collections;
/* Attach this class to the current objective .
 * If the objective is out of minimap range then it's icon will
 * float on borders of the minimap.
 * Coder: Kushal
 */

public class ObjectiveIndicator : MonoBehaviour {
	
	public Transform player;
	// cast the ray only to the minimap border ignoring all other layers
	public LayerMask minimapplayer;
	// stucture that holds all the info about where the raycast hits
	RaycastHit hit;

	private Vector3 ObjPos;
	public Vector3 CurrentObjPosition
	{
		get{
			return ObjPos;
		}
		set{
			ObjPos = value;
		}
	}
	
	public Vector3 GetContactPoint
	{
		get
		{
			return hit.point;
		}
	}
	
	[HideInInspector]
	public bool floating=false;

    private Texture2D tex;

    private static ObjectiveIndicator s_Instance = null;
    public static ObjectiveIndicator Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(ObjectiveIndicator)) as ObjectiveIndicator;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate ObjectiveIndicator");
                }
            }
            return s_Instance;
        }
    }
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
        tex = Resources.Load("Images/Objectives/Star") as Texture2D;
	}

    public void SetIndicatorTexture(string name)
    {
        switch (name)
        {
            case "ShoppingMall":
                tex = Resources.Load("Images/Inventory/Supermarket") as Texture2D;
                renderer.material.SetTexture("_MainTex", tex);
                break;
            case "Pharmacy":
                tex = Resources.Load("Images/Inventory/PharmacyIcon") as Texture2D;
                renderer.material.SetTexture("_MainTex", tex);
                break;
            case "PostOffice":
                tex = Resources.Load("Images/Inventory/PostOffice") as Texture2D;
                renderer.material.SetTexture("_MainTex", tex);
                break;
            case "Bank":
                tex = Resources.Load("Images/Inventory/Bank") as Texture2D;
                renderer.material.SetTexture("_MainTex", tex);
                break;
            default:
                tex = Resources.Load("Images/Objectives/Star") as Texture2D;
                renderer.material.SetTexture("_MainTex", tex);
                break;
        }
    }


	void raycast()
	{
		// throw the ray not more than the distance between player and objective.
		float distance = Vector3.Distance(player.transform.position,CurrentObjPosition); 
		//direction in which the ray has to be projected.
		Vector3 direction = player.transform.position-CurrentObjPosition;
	    
		Debug.DrawRay(CurrentObjPosition,direction,Color.red);
		//raycast from CurrentObjectivePosition to the player ignoring all other layers
		if(Physics.Raycast(CurrentObjPosition,direction,out hit,distance,minimapplayer))
		{
			//checking the raycast collision with the MinimapBorder 
			// the collider is attached to the MinimapCamera.
			if(hit.collider.tag == "MapBorder")
			{
				// change the position of objective icon to the colliding point on minimap.
                transform.position = new Vector3(hit.point.x, 7, hit.point.z);
				floating = true;
			}
		}else
		{
			if(transform.position!=CurrentObjPosition)transform.position = CurrentObjPosition;
			floating = false;
		}
	}
	void FixedUpdate()
	{
		raycast();
	}
}
