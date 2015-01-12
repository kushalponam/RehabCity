using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Attach this class to any object
 * This class draws the entire objecive window
 * Coder : Kushal
*/

public class DrawObjectiveList : MonoBehaviour {
	
	// skin for the entire objective window
	public GUISkin skin;
	
	private GUIStyle objwindowstyle;
	
	private Texture2D ObjMaximizedTexture;
	private Texture2D ObjMinimizedTexture;
	
	// offset for the text area
    private float TextAreaLeftOffset_Maximized= 400;
	private float TextAreaTopOffset_Maximized = 200;
    private float TextAreaLeftOffset_Minimized = 70;
    private float TextAreaTopOffset_Minimized =  90;
    private int TextAreaFontSize_Maximized =     80;
    private int TextAreaFontSize_Minimized =     35;
   
    private int TickMarkLeftOffset_Maximized;
    private int TickMarkTopOffset_Maximized =    80;
    private int TickMarkLeftOffset_Minimized;
    private int TickMarkTopOffset_Minimized =    80;
    private int TickMarkScale_Maximized =        150;
    private int TickMarkScale_Minimized =        50;
   // private int TickMarkTopOffset_Minimized;
    
	// Main rect for Objective Window
	private Rect ObjectiveWindow ;
	
	// boolean used to minimize/maximize
	[HideInInspector]
    public bool MinimizeObjective=false;
    
	//variable to get objective manager
	private ObjectiveManager ObjManager;
	
	// string that will contain the description of objective
	private string description;
	
	// tickmark texture that will be assigned to the label
	private Texture2D tickmark;
	// used to store the tickmark texture instead of loading it everytime we want it.
	private Texture2D tickmarktexture;
	
	private Rect Minimized = new Rect(Screen.width-Screen.width/5,0,Screen.width/5,Screen.height/2);
	private Rect Maximized = new Rect(0,0,Screen.width,Screen.height/2);

    /// <summary>
    /// set by startscenesettings script
    /// </summary>
    public bool showObjectiveWindow = true;

	// this has the dimensions to change how objective window will look
	private Rect SetWindow;
	
	private Camera MiniMapCamera;
	
	private Rect MiniMapMaximized = new Rect(0,-0.5f,1,1);
	private Rect MiniMapMinimized = new Rect(0,-0.5f,0.2f,0.8f);
	
	private Rect SetMinimap;

    private LanguageManager language;

    public bool CanReset = false;
    private static DrawObjectiveList s_Instance = null;
    public static DrawObjectiveList Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(DrawObjectiveList)) as DrawObjectiveList;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate DrawObjectiveList");
                }
            }
            return s_Instance;
        }
    }
    void Start()
	{
		if(skin==null)
		{  
			Debug.LogWarning("Objective GUI skin is not attached");
		}
		ObjManager = GetComponent<ObjectiveManager>();
		SetWindow = Maximized;
		SetMinimap = MiniMapMaximized;
		tickmarktexture = Resources.Load("Images/Objectives/tick") as Texture2D;
		
		objwindowstyle = new GUIStyle();
        ObjMaximizedTexture =  Resources.Load("Images/Objectives/Objective2E") as Texture2D;
        ObjMinimizedTexture = Resources.Load("Images/Objectives/Objective2MEN") as Texture2D;

		MiniMapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
		skin.textArea.normal.textColor = Color.black;
        language = GetComponent<LanguageManager>();
#if UNITY_WEBPLAYER
        TextAreaLeftOffset_Maximized=  225;
	    TextAreaTopOffset_Maximized =  150;
        TextAreaLeftOffset_Minimized = 40;
        TextAreaTopOffset_Minimized =  70;
        TextAreaFontSize_Maximized =   50;
        TextAreaFontSize_Minimized =   20;

        TickMarkTopOffset_Minimized = 65;
        TickMarkScale_Maximized =      100;
        TickMarkScale_Minimized =      40;
#endif
#if UNITY_ANDROID
        TextAreaLeftOffset_Maximized = 200;
        TextAreaTopOffset_Maximized = 100;
        TextAreaLeftOffset_Minimized = 50;
        TextAreaTopOffset_Minimized = 50;
        TextAreaFontSize_Maximized = 40;
        TextAreaFontSize_Minimized = 20;

        TickMarkTopOffset_Minimized = 65;
        TickMarkScale_Maximized = 100;
        TickMarkScale_Minimized = 30;
#endif
	}

    public void SetObjectiveWindowText(bool english)
    {
        ObjMaximizedTexture = english ? Resources.Load("Images/Objectives/Objective2E") as Texture2D : Resources.Load("Images/Objectives/Objective2P") as Texture2D;
        ObjMinimizedTexture = english ? Resources.Load("Images/Objectives/Objective2MEN") as Texture2D : Resources.Load("Images/Objectives/Objective2MPN") as Texture2D;
#if UNITY_WEBPLAYER
        ObjMaximizedTexture = english ? Resources.Load("Images/Objectives/Objective2E_W") as Texture2D : Resources.Load("Images/Objectives/Objective2P") as Texture2D;
#endif
#if UNITY_ANDROID
        ObjMaximizedTexture = english ? Resources.Load("Images/Objectives/Objective2E_W") as Texture2D : Resources.Load("Images/Objectives/Objective2P") as Texture2D;
#endif
    }
    void ResetAfterLevelLoads()
    {
        MinimizeObjective = true;
        MiniMapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
    }

    private string levelloadedname;
    void OnLevelWasLoaded(int level)
    {
        levelloadedname = Application.loadedLevelName;
        if (levelloadedname == "NewCity")
        {
            if (!CanReset) return;
            ResetAfterLevelLoads();
        }
        
    }
	void OnGUI()
	{
		GUI.skin = skin;
        
		//objwindowstyle.normal.background = MinimizeObjective?null:Resources.Load("Images/Objectives/Objective")as Texture2D;
		ObjectiveWindow = GUI.Window(1,ObjectiveWindow,DrawObjective,"",objwindowstyle);
		if(MinimizeObjective)
		{
            Minimized = showObjectiveWindow ? new Rect(Screen.width - Screen.width / 5, 0, Screen.width / 5, Screen.height / 2) : new Rect(Screen.width, 0, 0, 0);
			// screen upper right coner
			MoveTowards(ref SetWindow,Minimized,50.0f);
			ObjectiveWindow.Set(SetWindow.x,SetWindow.y,SetWindow.width,SetWindow.height);
			MoveTowards(ref SetMinimap,MiniMapMinimized,50.0f);
			if(MiniMapCamera!=null)MiniMapCamera.rect = SetMinimap;
		}else     
		{	
			// screen middle center
			MoveTowards(ref SetWindow,Maximized,50.0f);
			ObjectiveWindow.Set(SetWindow.x,SetWindow.y,SetWindow.width,SetWindow.height);
			MoveTowards(ref SetMinimap,MiniMapMaximized,50.0f);
            if (MiniMapCamera != null) MiniMapCamera.rect = SetMinimap;
		}
	}
	
	// method to draw the content inside objective window
	void DrawObjective(int WindowId)
	{	
		// minimized objective window
		if(MinimizeObjective && showObjectiveWindow)
		{
			objwindowstyle.normal.background = ObjMinimizedTexture;
			// draw content for all the objectives in the queue
			for(int i=0;i<ObjManager.GetObjQueue.Length;i++)
			{
				    // set tickmark to completed objectives
			        tickmark = ObjManager.GetObjQueue[i].Completed? tickmarktexture : null;
				    
					// only after the texture is set, assign it to the corresponding label
				    skin.label.normal.background = tickmark;
				
					// tickmark label
					GUI.Label(new Rect(10,
							           TickMarkTopOffset_Minimized + (i*TickMarkTopOffset_Minimized),
									   TickMarkScale_Minimized,
							           TickMarkScale_Minimized),
									   "  ",
									   skin.label);
					// set the description for each objective
				    description = ObjManager.GetCurrentObjective==null ? " ":ObjManager.GetObjQueue[i].Description;
				   
				    // description text area for each objective
					GUI.Label(new Rect(TextAreaLeftOffset_Minimized,
									  	  TextAreaTopOffset_Minimized + (i*TextAreaTopOffset_Minimized),
					                	  200,
					                 	  500),
					                 	  description,
					                 	  skin.textArea);
				    // font size after minimized
				skin.textArea.normal.textColor = Color.black;	
				skin.textArea.fontSize = TextAreaFontSize_Minimized;
			}
			
		}
		else
		// maximized window
		{
			objwindowstyle.normal.background = ObjMaximizedTexture;
			
			tickmark = ObjManager.transition ? tickmarktexture : null;
			skin.label.normal.background = tickmark;
			GUI.Label(new Rect(50,
					           170,
							   TickMarkScale_Maximized,
					           TickMarkScale_Maximized),
							   " ",
							   skin.label);
				
			description = ObjManager.GetCurrentObjective==null ? " ":ObjManager.GetCurrentObjective.Description;
            if (ObjManager.GetCurrentObjective != null)
            {
                description = ObjManager.GetCurrentObjective.Completed ? language.WellDoneText() : ObjManager.GetCurrentObjective.Description;
                description = ObjManager.LevelComplete ? language.LevelCompleteString().Replace("@",ObjManager.PreviousLevel.ToString()) :description;
            }
           // description = ObjManager.GetCurrentObjective!=null && ObjManager.GetCurrentObjective.Completed ? " Well Done " : ObjManager.GetCurrentObjective.Description;
			
			GUI.Label(new Rect(TextAreaLeftOffset_Maximized,
								  TextAreaTopOffset_Maximized,
				                 Screen.width-50 ,
				                 Screen.height/2),
				                 description,
				                 skin.textArea);
			
			skin.textArea.fontSize = TextAreaFontSize_Maximized;
		}
	}
	public void Toggleobjective()
	{
		MinimizeObjective = !MinimizeObjective;
	}
	void Update()
	{
#if UNITY_ANDROID
        if (!MinimizeObjective && !DigitalStick.Instance.GetIKey())
        {
            if (DigitalStick.Instance.GetAnyTouch())
            {
                MinimizeObjective = true;
            }
        }else if(MinimizeObjective && DigitalStick.Instance.GetIKey())
        {
            MinimizeObjective = false;
        }
        return;
#endif
        if (Controller.B2())
        {
            Toggleobjective();
        }
	}
	// lerp function
	void MoveTowards(ref Rect source, Rect Destination,float duration)
	{
		Rect difference = new Rect(0,0,0,0);
		difference.x = (Destination.x - source.x)/duration;
		difference.y = (Destination.y - source.y)/duration;
		difference.width = (Destination.width - source.width)/duration;
		difference.height = (Destination.height - source.height)/duration;
		source.x += difference.x;
		source.y += difference.y;
		source.width += difference.width;
		source.height += difference.height;
	}
	
}
