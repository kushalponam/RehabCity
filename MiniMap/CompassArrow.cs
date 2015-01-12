using UnityEngine;
using System.Collections;

/* Attach this class to player's arrow object
 * This class gets the PathArray and sets the next target position to look at
*/

public class CompassArrow : MonoBehaviour {
    public Vector3 TargetPosition;
    private ArrayList pathnodes;

    /// <summary>
    /// removing the y value so that arrow does not point towards 3D point.
    /// </summary>
    private Vector3 lookatposition;
    private static CompassArrow s_Instance = null;
    public static CompassArrow Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(CompassArrow)) as CompassArrow;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate CompassArrow");
                }
            }
            return s_Instance;
        }
    }
    //Set from PathFindingController script once it finds a path
    public void SetPathNodes(ArrayList list)
    {
        pathnodes = list;
    }

    void GetTargetPosition()
    {   
        // for each node in the pathlist
        for (int i=0;i<pathnodes.Count;i++)
        {
            // get each node
            Node node = (Node)pathnodes[i];
            // check the distance between player and each node
            float Distance = Mathf.Abs(Vector3.Distance(transform.position, node.position));
            // if player is close to one node
            if (Distance < 15)
            {
                //  and if next node is not near the final destination then, choose to point towards next node
                if (i < pathnodes.Count-1)
                {
                    Node nextnode = (Node)pathnodes[i + 1];
                    TargetPosition = nextnode.position;
                }
                // else if next node is final destination then get the postion from objective manager
                else
                {
                    TargetPosition = ObjectiveManager.Instance.GetCurrentObjective.Location;
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        pathnodes = new ArrayList();
        TargetPosition = Vector3.zero;
       
	}

    /// <summary>
    /// set from Game Manager which gets data about to show arrow or not from start scene settings
    /// </summary>
    public void ToggleArrowDisplay(bool show)
    {
        transform.FindChild("Group1").FindChild("Mesh1").renderer.enabled = show;
    }
	
	// Update is called once per frame
	void Update () {
        GetTargetPosition();
        lookatposition.x = TargetPosition.x;
        lookatposition.y = 2;
        lookatposition.z = TargetPosition.z;
        transform.LookAt(lookatposition);
	}
}
