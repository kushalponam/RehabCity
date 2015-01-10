using System;
using UnityEngine;
using System.Collections;

/* Game Manager is the heart of this game
 * Main actions in the game are controlled by this class
*/

public class GameManager : MonoBehaviour {

    public static bool pausegame = false;

	private InventoryManager inventorymanager;
    private DrawInventory drawinventory;
    private DrawObjectiveList drawobjective;
    private ObjectiveManager Objmanager;
    private ObjectiveIndicator objindicator;
    private SelectObjects playerselect;
    private PathFindingController pathfinding;
	private Vector3 screenpoint;
    private Vector3 ObjectFloatingPoint;
    private Transform Player;
    private FPSInputController playermovementinput;
    private MouseLook playerrotationinput;
    private CalibrationManager calibmanager;
    [HideInInspector]
    /// <summary> 
    /// use this to stop reset function for the first time game loads newcity scene and continue after the game has loaded.
    /// </summary>
    public bool CanReset = false;
    public struct PlayerData 
    {
       public static Vector3 playerposition;
       public static Vector3 playerrotation;
    };

    public GameObject PlusOne;
    public GameObject WrongMark;
    public int MaxLevel;
    [HideInInspector]
    public bool GameCompleted = false;

    private Texture2D PauseScreen;

    private GameObject points;
    private GameObject timer;
    private GameObject datacollector;

    /// <summary>
    /// set from start scene setting
    /// </summary>
    [HideInInspector]
    public bool showArrowObject = true;

    [HideInInspector]
    public bool IsCalibrationRequired = false;

    public bool IsAndroid = false;

    private AudioClip CorrectClip;
    private AudioClip ErrorClip;

    public Transform AnalogBG;
    public Transform Analog;
    public Transform DigitalIKey;
    public Transform DigitalSKey;

    private Transform AnalogBGtrans;
    private Transform Analogtrans;
    private Transform digitalIkeytrans;
    private Transform digitalSkeytrans;

    private AnalogStick analogStick;

    private int screenlevel = 0;
    private static GameManager s_Instance = null;
    public static GameManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate GameManager");
                }
            }
            return s_Instance;
        }
    }

    void SpawnVRJoysticks()
    {
#if UNITY_ANDROID
        AnalogBGtrans = Instantiate(AnalogBG) as Transform;
        Analogtrans =  Instantiate(Analog) as Transform;
        digitalIkeytrans = Instantiate(DigitalIKey) as Transform;
        digitalSkeytrans = Instantiate(DigitalSKey) as Transform;
      //  digitalButtonTransform = Instantiate(DigitalButtonKey) as Transform; 
        DontDestroyOnLoad(AnalogBGtrans.gameObject);
        DontDestroyOnLoad(Analogtrans.gameObject);
        DontDestroyOnLoad(digitalIkeytrans.gameObject);
        DontDestroyOnLoad(digitalSkeytrans.gameObject);
       // DontDestroyOnLoad(DigitalButtonKey.gameObject);
        IsAndroid = true;
#endif
    }


	void Start () {
         DontDestroyOnLoad(this.gameObject);
         MaxLevel = 5;
         SpawnVRJoysticks();
         Player = GameObject.Find("NewPlayer").transform;
         playermovementinput = Player.GetComponentInChildren<FPSInputController>();
         playerrotationinput = Player.GetComponentInChildren<MouseLook>();
	     analogStick = AnalogStick.Instance;
	     inventorymanager = GetComponent<InventoryManager>();
         Objmanager = ObjectiveManager.Instance;
         drawobjective = DrawObjectiveList.Instance;
		 drawinventory = GetComponent<DrawInventory>();
         objindicator = ObjectiveIndicator.Instance;
         playerselect = SelectObjects.Instance;
         pathfinding = PathFindingController.Instance;
         calibmanager = CalibrationManager.Instance;
         ObjectFloatingPoint = new Vector3(Screen.width - 100, Screen.height - 200, 0);
         CorrectClip = Resources.Load("Sounds/Effects/Correct") as AudioClip;
         ErrorClip = Resources.Load("Sounds/Effects/Error") as AudioClip;
         PauseScreen = Resources.Load("Images/Objectives/PauseScreen") as Texture2D;
         points = DisplayScore.Instance.gameObject;
         timer = GameTimer.Instance.gameObject;
         datacollector = GameObject.Find("DataCollector");
	}

    /// <summary>
    /// initially called from start scene setting
    /// </summary>
    public void ToggleArrowDisplay()
    {
        Player.FindChild("SimpleController").FindChild("arrow").GetComponent<CompassArrow>().ToggleArrowDisplay(showArrowObject);
    }
    void ResetAfterLevelLoads()
    {
        Player = GameObject.Find("NewPlayer").transform;
        ToggleArrowDisplay();
        playermovementinput = Player.GetComponentInChildren<FPSInputController>();
        playerrotationinput = Player.GetComponentInChildren<MouseLook>();
        analogStick = AnalogStick.Instance;
        calibmanager = CalibrationManager.Instance;
        objindicator = GameObject.Find("ObjectiveIndicator").GetComponent<ObjectiveIndicator>();
        playerselect = SelectObjects.Instance;
        pathfinding = PathFindingController.Instance;
        SetPlayerPosition();
        CorrectClip = Resources.Load("Sounds/Effects/Correct") as AudioClip;
        ErrorClip = Resources.Load("Sounds/Effects/Error") as AudioClip;
    }

    // Everytime the New City is loaded the player is set to data in Playerdata that is accessed before 
    // player enters into postoffice, supermarket , pharmacy etc...
    void SetPlayerPosition()
    {
        Player.position = PlayerData.playerposition;
        Player.FindChild("SimpleController").transform.localEulerAngles = PlayerData.playerrotation;
    }  

    void PausePlayer()
    {
        if (drawobjective == null) return;
        playermovementinput.canMove = drawobjective.MinimizeObjective ? true : false;
        playerrotationinput.canRotate = drawobjective.MinimizeObjective ? true : false;
    }

    private string levelloadedname = null;
    void OnLevelWasLoaded(int level)
    {
        levelloadedname = Application.loadedLevelName;
        if (levelloadedname == "NewCity")
        {
            // if it game just started do not reset
            if (!CanReset) return;
            Debug.Log("Game Manager Loaded Level " + level);
            ResetAfterLevelLoads();
        }
    }
	
	// Everytime a new objective is added by the objective manager it has to be updated on minimap
	// this method does that.
    // Ask pathfinder for a path
	void SetObjectivePosition()
	{
		if(Objmanager.GetCurrentObjective==null) return;
        if (objindicator == null || Objmanager== null) return;
		if(objindicator.CurrentObjPosition!=Objmanager.GetCurrentObjective.Location)
		{
			objindicator.CurrentObjPosition = Objmanager.GetCurrentObjective.Location;
            objindicator.SetIndicatorTexture(Objmanager.GetCurrentObjective.name);
            pathfinding.FindPath();
			Debug.Log("Objective postion Set"+objindicator.CurrentObjPosition);
		}
	}

	// after player approaches near a collectible item he has to collect the item
	void CollectObjects()
	{
        if (playerselect == null) playerselect = SelectObjects.Instance;
		// if the player's cursor is on object and if b1 button is pressed
        if (Controller.B1() && playerselect.Selected)
        {
            if (Objmanager.GetCurrentObjective.AnswerSet.Count != 0 && !Objmanager.GetCurrentObjective.AnswerSet.Contains(playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString()))
            {
                Instantiate(WrongMark, new Vector3(playerselect.SelectedObject.transform.position.x, playerselect.SelectedObject.transform.position.y, playerselect.SelectedObject.transform.position.z - 5), Quaternion.identity);
                audio.PlaySound(ErrorClip);
                playerselect.WriteGitterObjectName = playerselect.SelectedObject.name;
                Objmanager.AddScore(-1);
                return;
            }
            inventorymanager.AddItem(playerselect.SelectedObject.GetComponent<CollectibleItem>().ItemType.ToString(), playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString());
            if (Objmanager.GetCurrentObjective.CollectedSet != null)
            {
                if (Objmanager.GetCurrentObjective.CollectedSet.ContainsKey(playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString()))
                {
                    Objmanager.GetCurrentObjective.CollectedSet[playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString()] += 1;
                    // Debug.Log("Added " + playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString());
                }
                else
                {
                    Objmanager.GetCurrentObjective.CollectedSet.Add(playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString(), 1);
                    //   Debug.Log("Pushing " + playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString());
                }
            }
            // get the texture to draw on screen as a pickup feedback   
            // drawinventory.GetTextureToDraw(playerselect.SelectedObject.GetComponent<CollectibleItem>().ItemType.ToString(),playerselect.SelectedObject.GetComponent<CollectibleItem>().Itemname.ToString());
            // get the screen point
            screenpoint = Camera.main.ScreenToWorldPoint(ObjectFloatingPoint);
            // set the screen point
            playerselect.WriteGitterObjectName = playerselect.SelectedObject.name;
            playerselect.SelectedObject.GetComponent<CollectibleItem>().MovePoint = screenpoint;
            playerselect.SelectedObject.GetComponent<CollectibleItem>().CanMove = true;
            Objmanager.GetCurrentObjective.NumberofItemsCollected += 1;
            Instantiate(PlusOne, playerselect.SelectedObject.transform.position, Quaternion.identity);
            audio.PlaySound(CorrectClip);
            Objmanager.AddScore(1);
            // destroy that object after collecting  
            Destroy(playerselect.SelectedObject, 3.0f);
        }
	}

   
    void OnGUI()
    {
        if (pausegame)
        {
            switch (screenlevel)
            {
                case 0:
                    // to show the black screen on top of all the other GUI.
                    GUI.depth = 0;
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PauseScreen);
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 250, 800, 600));
                    GUI.Box(new Rect(0, 0, 400, 600), " ");
                    if (GUI.Button(new Rect(100, 100, 200, 100), "Resume"))
                    {
                        ResumeGame();
                    }
                    if (GUI.Button(new Rect(100, 250, 200, 100), "How to Play"))
                    {
                        screenlevel = 1;
                    }
                    if (GUI.Button(new Rect(100, 400, 200, 100), "Main Menu"))
                    {
                        Application.LoadLevel("StartScene");
                        CleanUpObjects();
                    }
                    GUI.EndGroup();
                    break;
                case 1:
                    GUI.depth = 0;
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PauseScreen);
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 250, 800, 600));
                    GUI.Box(new Rect(0, 0, 400, 600), "How to Play");
                    GUI.Label(new Rect(50, 100, 400, 50), "Up arrow  :    Move forward");
                    GUI.Label(new Rect(50,150,400,50),"Left arrow  :  Rotate Left ");
                    GUI.Label(new Rect(50,200,400,50),"Right arrow  : Rotate Right");
                    GUI.Label(new Rect(50, 250, 400, 50), " i button       : Minimize/Maximize Objective window");
                    GUI.Label(new Rect(50, 300, 400, 50), " Space         : Selection/Confirm");

                    if (GUI.Button(new Rect(100, 400, 200, 100), "Back"))
                    {
                        screenlevel = 0;
                    }
                    GUI.EndGroup();
                    break;
                default:
                    break;
            }
        }

    }
    void CleanUpObjects()
    {
        Destroy(gameObject);
        Destroy(points);
        Destroy(timer);
        Destroy(datacollector);
        Destroy(calibmanager);
        Destroy(AnalogBGtrans.gameObject);
        Destroy(Analogtrans.gameObject);
        Destroy(digitalIkeytrans.gameObject);
        Destroy(digitalSkeytrans.gameObject);
        //Destroy(digitalButtonTransform.gameObject);
        ResumeGame();
    }
    void PauseGame()
    {
        pausegame = true;
        Time.timeScale = 0;
        playermovementinput.pausegame = true;
    }
    void ResumeGame()
    {
        pausegame = false;
        Time.timeScale = 1;
        playermovementinput.pausegame = false;
    }

    void SendPlayerInput()
    {
        if (IsAndroid && playermovementinput != null && playerrotationinput != null && !IsCalibrationRequired)
        {
            playermovementinput.IsAndroid = true;
            playerrotationinput.IsAndroid = true;
            playermovementinput.VerticalAxis = analogStick.GetAxis().y;
            playerrotationinput.HorizontalAxis = analogStick.GetAxis().x;
        }

        if (IsCalibrationRequired == false) return;
        // if we are using calibration then set it true in input classes so they
        // will not use the controller input.
        if (playermovementinput!=null && !playermovementinput.IsCalibrationRequired)
        {
            playermovementinput.IsCalibrationRequired = true;
            playerrotationinput.IsCalibrationRequired = true;
        }
        if (playermovementinput != null && playerrotationinput!=null)
        {
            //Debug.Log(calibmanager.GetDirection(0));
            // inverting the vertical axis
            playermovementinput.VerticalAxis = calibmanager.GetDirection(1) * -1;
            playerrotationinput.HorizontalAxis = calibmanager.GetDirection(0);
        }
    }

    void CheckGameOver()
    {
        if ((timer.GetComponent<GameTimer>().minutes >= 19 && timer.GetComponent<GameTimer>().seconds > 58)  ||
             Input.GetKeyDown(KeyCode.F12))
        {
            GameOverScreen.Score = Objmanager.GetScore;
            GameOverScreen.Level = Objmanager.GetLevel;
            Application.LoadLevel("GameOver");
            CleanUpObjects();
        }
    }

	void Update () {
        SetObjectivePosition();
		CollectObjects();
        PausePlayer();
        SendPlayerInput();
        CheckGameOver();
        if (!Application.isEditor && !pausegame)
        {
            Screen.showCursor = false;
        }
        else 
        {
            Screen.showCursor = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }


	}
}
