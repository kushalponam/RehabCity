using UnityEngine;
using System.Collections;
/* This script is attached to MiniMapCamera.
 * Camera is placed at certain Height from the player.
 * Camera rotates based on players Horizontal rotation.
 * Coder : Kushal
*/


public class FollowPlayer : MonoBehaviour {
	
	public Transform player;
	public float Height = 30;

    private DrawObjectiveList drawobjectivelist;
    private ObjectiveManager objmanager;

    public int MaxMapLength = 508;
    public int MaxCameraRange = 250;
	void Start()
	{
		if(player==null){
			Debug.LogWarning("Attach the player transform to minimap camera");
		}
        drawobjectivelist = DrawObjectiveList.Instance;
        objmanager = ObjectiveManager.Instance;
	}
	

	void Update () 
	{
        //if minimap camera is maximized then show the entire path from player to destination.
        if (objmanager.GetCurrentObjective != null && !drawobjectivelist.MinimizeObjective)
        {
            float dist = (player.transform.position - objmanager.GetCurrentObjective.Location).magnitude;
            int range = (MaxCameraRange * (int)dist) / MaxMapLength;
            // we are disabling boxcollider to stop the objective from floating on the minimap borders
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<Camera>().orthographicSize = range>20?range:20;
            // change position to midpoint (b/w player and objective)
            transform.position = (player.transform.position + objmanager.GetCurrentObjective.Location) / 2 + new Vector3(0,Height,0);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, player.localEulerAngles.y, 0);
        }
        else
        {
            // set it back to true 
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<Camera>().orthographicSize = 20;
            // Camera is placed at a height from the player.
            transform.position = player.transform.position + new Vector3(0, Height, 0);
            // Camera rotates based on players horizontal rotation
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, player.localEulerAngles.y, 0);
        }
	}
}
