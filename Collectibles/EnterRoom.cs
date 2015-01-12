using UnityEngine;
using System.Collections;

public class EnterRoom : MonoBehaviour {
    private GameObject Player;
    /// <summary>
    /// used to check if player is near the room
    /// </summary>
    private bool canDisplay = false;
    private DrawObjectiveList drawobjective;
    public string LevelToLoad;
    public string ObjectiveRequiredToEnter;
    GUIStyle style;
    private Texture2D backgroundtex;
    private LanguageManager language;
    private ObjectiveManager objmanager;

    private float TextBackground_LeftOffset = Screen.width / 3 - 130;
    private float TextBackground_TopOffset = Screen.height - Screen.height / 3;
    private float TextBackground_Width = 1000;
    private float TextBackground_Height = 60;

    private float TextArea_LeftOffset = Screen.width / 3 - 80;
    private float TextArea_TopOffset = Screen.height - Screen.height / 3;
    private float TextArea_Width = 200;
    private float TextArea_Height = 200;
    // used by Language Manager to get text for keyboard input.
    private bool IsWebBuild = false;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        drawobjective = GameObject.FindGameObjectWithTag("Manager").GetComponent<DrawObjectiveList>();
        style = new GUIStyle();
        style.fontSize = 50;
        style.fontStyle = FontStyle.Bold;
        style.font = Resources.Load("Fonts/KGPrimaryPenmanship") as Font;
        style.normal.textColor = Color.white;
        backgroundtex = Resources.Load("Images/Inventory/TextBackground") as Texture2D;
        language = LanguageManager.Instance;
        objmanager = ObjectiveManager.Instance;
#if UNITY_WEBPLAYER
        IsWebBuild = true;
        style.fontSize = 40;
        TextBackground_LeftOffset = Screen.width / 3 - 130;
        TextBackground_TopOffset = Screen.height - Screen.height / 3;
        TextBackground_Width  =    700;
        TextBackground_Height  =   50;

        TextArea_LeftOffset = Screen.width / 3 - 80;
        TextArea_TopOffset = Screen.height - Screen.height / 3;
        TextArea_Width  = 150;
        TextArea_Height  = 150;
#endif
	}

    void OnGUI()
    {
        if (objmanager.GetCurrentObjective == null) return;
        if (objmanager.GetCurrentObjective.name == ObjectiveRequiredToEnter && canDisplay && drawobjective.MinimizeObjective)
        {
            GUI.DrawTexture(new Rect(TextBackground_LeftOffset, TextBackground_TopOffset, TextBackground_Width, TextBackground_Height),backgroundtex);
            GUI.TextArea(new Rect(TextArea_LeftOffset, TextArea_TopOffset, TextArea_Width, TextArea_Height), language.EnterRoomtext(this.name,IsWebBuild), style);
        }
    }

	// Update is called once per frame
	void Update () {
        canDisplay = Vector3.Distance(transform.position, Player.transform.position) < 5.0f ? true : false;
        if (canDisplay && drawobjective.MinimizeObjective)
        {
            if (Controller.B1() && objmanager.GetCurrentObjective.name==ObjectiveRequiredToEnter)
            {
               switch(LevelToLoad)
                {
                    case "SuperMarketNew":
                                    GameManager.PlayerData.playerposition = new Vector3(this.transform.position.x + 5, 3, this.transform.position.z);
                                    GameManager.PlayerData.playerrotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                          break;
                   case "PostOffice":
                                    GameManager.PlayerData.playerposition = new Vector3(this.transform.position.x-5, 3, this.transform.position.z);
                                    GameManager.PlayerData.playerrotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                          break;
                   case "Pharmacy":
                                    GameManager.PlayerData.playerposition = new Vector3(this.transform.position.x, 3, this.transform.position.z+5);
                                    GameManager.PlayerData.playerrotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                          break;
                   case "Bank":
                                    GameManager.PlayerData.playerposition = new Vector3(this.transform.position.x+5, 3, this.transform.position.z);
                                    GameManager.PlayerData.playerrotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
                          break;
                   default:
                    break;
                }
                Application.LoadLevel(LevelToLoad);
            }
        }
	}
}
