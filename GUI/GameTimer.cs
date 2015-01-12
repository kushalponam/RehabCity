using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    public GUIText timertext;
    private float totaltime;

    public string timetext=null;
    [HideInInspector]
    public int minutes=0;

    [HideInInspector]
    public float seconds=0;

    private int nextsetofsecond = 60;
    private int storesecondfortext=0;

    private string minutestring;
    private string secondstring;
    // Use this for initialization
    void Start()
    {
        timertext = GetComponent<GUIText>();
        DontDestroyOnLoad(this.gameObject);
#if UNITY_WEBPLAYER
        timertext.fontSize = 50;
#endif
#if UNITY_ANDROID
        timertext.fontSize = 50;
#endif
    }

    private static GameTimer s_Instance = null;
    public static GameTimer Instance
    {
        get
        {
            if(s_Instance==null)
            {
                s_Instance = FindObjectOfType(typeof(GameTimer)) as GameTimer;
                if(s_Instance==null)
                {
                    Debug.Log("Could not locate Game Timer");
                }
            }
            return s_Instance;
        }
    }

	// Update is called once per frame
	void Update () {
        // this is total seconds in the game 
        // only resets if the game is stopped
        seconds = Time.time ;
        // if the current time is greater that 60 seconds 
        // minute is increased and nextsetofsecond is set to 60*(minute+1) i.e 60*2 = 120
        if (seconds > nextsetofsecond)
        {
            minutes += 1;
            // it is minutes+1 because minutes starts with zero. So, when it's 1 minute nextsetofsecond is set to 120
            nextsetofsecond = 60 * (minutes + 1);
            // here we store the seconds to subract from totalseconds. For first minute it will be 60
            // and the text would be (current time)61- (storesecondfortext)60 = 1;
            storesecondfortext = 60 * minutes;
        }
        // if the second is greater that 60 seconds then 
        // the text must reset to 0
        seconds = Mathf.Abs(storesecondfortext - (int)seconds);

        minutestring = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        secondstring = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        timetext = Time.time.ToString();
        timertext.text = minutestring + " : " + secondstring;
	}
}
