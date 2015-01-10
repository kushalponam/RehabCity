using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Attach this class to any gameobject
 * This class manages the addition of objectives
 * Check Objectives clas to know what it does
 * New Objectives are added to a queue and removed when completed 
 * Coder : Kushal
*/
public class ObjectiveManager :MonoBehaviour{
	
	// Variables for different types of objectives
	private LocationObjective LObj;
	private ActionObjective AObj;
	private CollectionObjective CObj;

    private GameManager gamemanager;
    private InventoryManager inventorymanager;
	// Varialbe for main objective class which stores the current objective spawned	
	private  Objectives Obj;
	
	// queue to store the currentobjective 
	// used by drawinventory to display text
	private Queue<Objectives> ObjQueue;
	
	// readonly property that gives the entire objectivequeue as an array
	public Objectives[] GetObjQueue
	{
		get
		{
			return ObjQueue.ToArray();
		}
	}
	
	// readolny property that gives the current objective in the queue
	public Objectives GetCurrentObjective
	{
		get
		{
			if(ObjQueue.Count>0)
			{
			return ObjQueue.ToArray()[ObjQueue.Count-1];
			}else
			{
				return null;
			}
		}
	}

    private List<Objectives> LevelList;
	
	// number of objectives completed
	private int CompletedObjectives=0;
	
	//elapsed time is used to set delay for showing a tickmark after an objective is completed
	private float elapsedTime=0;
	
	// used to know when the objective is in transition to other.
	[HideInInspector]
	public bool transition=false;

    private DrawObjectiveList drawobjective;

    private int score=0;

    public int GetScore
    {
        get
        {
            return score;
        }
    }

   

    /// <summary>
    /// stores the level completed
    /// </summary>
    public int PreviousLevel = 0;
    /// <summary>
    /// stores the current level
    /// </summary>
    public int Level = 1;
    /// <summary>
    /// bool to know if a level has been completed
    /// </summary>
    public bool LevelComplete = false;
    public int GetLevel
    {
        get { return Level; }
    }


    private DisplayScore ScoreDisplay;
    private static ObjectiveManager s_Instance = null;
    public static ObjectiveManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(ObjectiveManager)) as ObjectiveManager;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate ObjectiveManager");
                }
            }
            return s_Instance;
        }
    }
	void Start()
	{
		Obj = new Objectives();
		ObjQueue = new Queue<Objectives>();
        drawobjective = GetComponent<DrawObjectiveList>();
        inventorymanager = GetComponent<InventoryManager>();
        gamemanager = GetComponent<GameManager>();
        //Level = 1;
        LevelList = new List<Objectives>();
        inventorymanager.AddCategory(InventoryManager.CategoryTypes.PostOffice);
        inventorymanager.AddCategory(InventoryManager.CategoryTypes.ShoppingMall);
        inventorymanager.AddCategory(InventoryManager.CategoryTypes.Pharmacy);
        ScoreDisplay = DisplayScore.Instance;
	}
    /// <summary>
    /// set from StartSceneSettings
    /// </summary>
    /// <param name="_level"></param>
    public void GetLevelAndAddObjective(int _level)
    {
        Level = _level;
        Debug.Log("Level Set to " + Level.ToString());
        GetNewObjectiveList();
        AddObjective(CompletedObjectives);
    }
    void GetNewObjectiveList()
    {
        LevelListManager.AddLevelsToList(Level);
        int randomnumber = Random.Range(0, LevelListManager.LevelList.Count);
        LevelList = LevelListManager.LevelList[randomnumber];
        Debug.Log("Random List choosen is " + randomnumber);
    }

	// main function to add objectives 
	// follow the sequence in which the objectives have to be spawned
	public void AddObjective(int ObjectiveNumber)
	{
        Obj = LevelList[ObjectiveNumber];
        Debug.Log("Added " + Obj + " as the objective");
       /* Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
        if (levelloaded == Obj.RequiredSceneToSpawn) Obj.CanbePushed = true;
        Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);*/
		/*switch(ObjectiveNumber)
		{
		case 0:
            
			LObj = new GotoShopping();
			// always store the spawned objective
			Obj = LObj;
            Debug.Log("Before Pushing "+Obj+" state is "+Obj.CanbePushed);
            if (levelloaded == 0) Obj.CanbePushed = true;
            Debug.Log("After Pushing "+Obj+" state is " + Obj.CanbePushed);
			break;
		case 1:
            CObj = new CollectMilk();
            Obj = CObj;
            inventorymanager.AddCategory(InventoryManager.CategoryTypes.ShoppingMall);
            Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
            if (levelloaded == 1) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
			break;
		case 2:
            LObj = new GotoPostoffice();
			Obj = LObj;
            Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
            if (levelloaded == 0) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
           // CompletedObjectives = 0;
			break;
        case 3:
            CObj = new CollectPackage();
            Obj = CObj;
            inventorymanager.AddCategory(InventoryManager.CategoryTypes.PostOffice);
            Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
            if (levelloaded == 2) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
            break;
        case 4:
            LObj = new GotoPharmacy();
            Obj = LObj;
            Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
            if (levelloaded == 0) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
            break;
        case 5:
            CObj = new CollectCream();
            Obj = CObj;
            inventorymanager.AddCategory(InventoryManager.CategoryTypes.Pharmacy);
            Debug.Log("Before Pushing "+Obj+" state is "+Obj.CanbePushed);
            if (levelloaded == 3) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
            break;
        case 6:
            LObj = new GotoBank();
            Obj = LObj;
            Debug.Log("Before Pushing " + Obj + " state is " + Obj.CanbePushed);
            if (levelloaded == 0) Obj.CanbePushed = true;
            Debug.Log("After Pushing " + Obj + " state is " + Obj.CanbePushed);
            break;
		default:
			break;
		}*/
	}

    public void AddScore(int _score)
    {
        score += _score;
        ScoreDisplay.UpdateScoreDisplay(score);
    }

    // set from startscene settings
    public void SetObjectiveLanguage(bool English, bool Portuguese)
    {
        Obj.SetLanguage(English, Portuguese);
    }
	/* checks for completion of objectives
	 * if completed adds an other objective to the queue
	*/
	void UpdateObjectives()
	{
        if (gamemanager.GameCompleted) return;
	   // check for completion on the spawned objective
	    Obj.CheckForCompletion();
		if(Obj.Completed)
		{
            // sets the objective window back to full screen.
            drawobjective.MinimizeObjective = false;
            // Example : If you are in supermarket and objective is to collect 3 milk. After that ojective is completed
            // the game has to wait until you exit that scene to give an other objective.
            if (Obj.CanAddNextObjective)
            {
                CompletedObjectives++;
                AddScore(10);
                Debug.Log("Score" + score + "list count "+LevelList.Count);
                // if the completedobjective is at the end of the list then 
                // increase the level and get another list
                if (CompletedObjectives > LevelList.Count - 1)
                {
                    Debug.Log("Level Complete " + Level);
                    PreviousLevel = Level;
                    LevelComplete = true;
                    Level++;
                    if (Level > gamemanager.MaxLevel)
                    {
                        Level = gamemanager.MaxLevel;
                      //gamemanager.GameCompleted = true;
                      //return;
                    }
                    CompletedObjectives = 0;
                    GetNewObjectiveList();
                }
                AddObjective(CompletedObjectives);
            }
			elapsedTime=0;
		}

		// Push the current objective into stack after some delay
		// delay is used to show a tick mark on the completed objective
	   elapsedTime+=Time.deltaTime;
	   transition = (ObjQueue.Count!=0 && elapsedTime<3.0f)?true:false;
	   if(elapsedTime>=3.0f)
		{
			if(ObjQueue.Count==0 || (GetCurrentObjective!=Obj))
			{
			    ObjQueue.Enqueue(Obj);
                LevelComplete = false;
				Debug.Log("Pushed "+Obj+" to queue");
			}
            
            if (ObjQueue.Count > 4)
            {
                ObjQueue.Dequeue();
                Debug.Log("Clearing Objective Queue as it crossed 4");
            }
		}
	}
	void Update()
	{
        UpdateObjectives();
       // Debug.Log((int)System.DateTime.Now.Ticks);
	}
}
