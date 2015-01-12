using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {

    private GUIText scoretext;
    public int Score=0;
    private string text;
    private static DisplayScore s_Instance = null;
    public static DisplayScore Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(DisplayScore)) as DisplayScore;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate Score");
                }
            }
            return s_Instance;
        }
    }
    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        scoretext = GetComponent<GUIText>();
        Invoke("SetText", 0.5f);
#if UNITY_WEBPLAYER
        scoretext.fontSize = 40;
#endif
#if UNITY_ANDROID
        scoretext.fontSize = 40;
#endif
	}
    void SetText()
    {
        text = LanguageManager.Instance.ScoreText();
        scoretext.text = text + Score.ToString();
    }

    public void UpdateScoreDisplay(int score)
    {
        Score = score;
        scoretext.text = text+Score.ToString();
    }

}
