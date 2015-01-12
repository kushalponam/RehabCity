using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	
	public int ObstacleFor;
	public int ObstacleIndex;

	public int CannotConnecttoRow;
	public int CannotConnecttoCol;
	// Use this for initialization
	void Start () {
	
	    CannotConnecttoRow = NodeManager.Instance.GetRow(ObstacleIndex);
		CannotConnecttoCol = NodeManager.Instance.GetCol(ObstacleIndex);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(transform.position,4.0f);
	}
}
