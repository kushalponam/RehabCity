using UnityEngine;
using System.Collections;
/* Attach this class to Player
 * this class does raycasting to objects that will be picked or selected
*/

public class SelectObjects : MonoBehaviour {
	public Texture2D crosshairImage;
	Ray ray;

    [HideInInspector]
	public float left,top;
	
    RaycastHit hit;
	public GameObject SelectedObject;
	public bool Selected = false;
    public bool SelectedArrow = false;
	private Color SelectedColor = new Color(0.23f,0.95f,0.21f,0.35f);
	private Color DeSelectedColor = new Color(0,0,0,0);
    private DrawObjectiveList drawobjective;

    private bool CantakeScreenShot=false;

    private float SelectionImageWidth;
    private float SelectionImageHeight;

    private static SelectObjects s_Instance = null;
    public static SelectObjects Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(SelectObjects)) as SelectObjects;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate Selectobject");
                }
            }
            return s_Instance;
        }
    }
    void Start()
    {
        left = Screen.width / 2;
        top = Screen.height / 2;
        SelectionImageWidth = crosshairImage.width / 4;
        SelectionImageHeight = crosshairImage.height / 4;
        drawobjective = DrawObjectiveList.Instance;
        CantakeScreenShot = true;    
#if UNITY_WEBPLAYER
        SelectionImageWidth = crosshairImage.width / 5;
        SelectionImageHeight = crosshairImage.height / 5;
#endif
#if UNITY_ANDROID
        SelectionImageWidth = crosshairImage.width / 5;
        SelectionImageHeight = crosshairImage.height / 5;
#endif
    }

	void OnGUI()
	{
        // this is just to test on scenes without loading game manager.
        if (drawobjective == null)
        {
            GUI.DrawTexture(new Rect(left, top, SelectionImageWidth, SelectionImageHeight), crosshairImage);
            return;
        }
        if (drawobjective.MinimizeObjective)
        {
            GUI.DrawTexture(new Rect(left, top, SelectionImageWidth, SelectionImageHeight), crosshairImage);
        }
	}

    #region WriteData
    public string WriteGitterObjectName;
    private float elapsedTime = 0;
    void WriteData()
    {
        if (DataCollector.Instance == null || ObjectiveManager.Instance.GetCurrentObjective == null) return;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 0.2f)
        {
            DataCollector.Instance.WriteGitter(ObjectiveManager.Instance.GetScore, ObjectiveManager.Instance.Level, GameTimer.Instance.timetext, ObjectiveManager.Instance.GetCurrentObjective.Description, WriteGitterObjectName,left,top);
            WriteGitterObjectName = "None";
            elapsedTime = 0;
        }
    }
    #endregion

    #region CaptureScreenShot
    private float elapsedTimeforScreenShot = 0;
    void CaptureScreenShot()
    {
        if (!CantakeScreenShot ||DrawObjectiveList.Instance == null || DataCollector.Instance==null || GameTimer.Instance==null) return;
        if (CantakeScreenShot && Application.loadedLevelName != "NewCity" && DrawObjectiveList.Instance.MinimizeObjective)
        {
            //delay to let the objective window minimize completely
            elapsedTimeforScreenShot += Time.fixedDeltaTime;
            if (elapsedTimeforScreenShot > 1.5f)
            {
                DataCollector.Instance.CaptureScreenShot(Application.loadedLevelName, GameTimer.Instance.timetext);
                CantakeScreenShot = false;
                elapsedTimeforScreenShot = 0;
            }
        }
        else
        {
            elapsedTimeforScreenShot = 0;
        }
    }
    #endregion
    void Update()
	{
     // Debug.Log(Input.mousePosition);
	  ray = Camera.main.ScreenPointToRay(new Vector3(left + SelectionImageWidth/2,Screen.height-(top+20),0));
	  Debug.DrawRay(ray.origin,ray.direction,Color.red);
	  if(Physics.Raycast(ray,out hit,100))
		{
            if (hit.collider.tag == "item_1")
            {
                //   Debug.Log("Hitting ");
                Selected = true;
                SelectedObject = hit.collider.gameObject;
                SelectedObject.transform.FindChild("Cylinder").renderer.material.color = SelectedColor;
            }
            else if (hit.collider.tag == "directionalarrow")
            {
                SelectedArrow = true;
                SelectedObject = hit.collider.gameObject;
                SelectedObject.transform.FindChild("Cylinder").renderer.material.color = SelectedColor;
            }else if(hit.collider.tag == "SelectionArrows")
            {
                SelectedObject = hit.collider.gameObject;
                SelectedObject.transform.FindChild("Cylinder").renderer.material.color = SelectedColor;
                SelectedObject.GetComponent<BankSelection>().Select();
            }
            else
            {
                Selected = false;
                SelectedArrow = false;
                if (SelectedObject != null)
                {
                    SelectedObject.transform.FindChild("Cylinder").renderer.material.color = DeSelectedColor;
                }
            }
		}
      WriteData();
	}

    void FixedUpdate()
    {
        CaptureScreenShot();
    }

}
