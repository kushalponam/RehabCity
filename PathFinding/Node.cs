using UnityEngine;
using System.Collections;
using System;

public class Node : MonoBehaviour,IComparable {
	
	public float nodeTotalCost;
	public float estimatedCost;

	public Node parent;
	public Vector3 position;
	
	public bool IsStartNode=false;
	public bool IsGoalNode=false;
	
	public int index;
	
    [HideInInspector]
	public float DistanceFromPlayer;
    [HideInInspector]
    public float DistanceFromGoal;

	private GameObject player;
    private ObjectiveManager objmanager;

	public Node()
	{
		this.estimatedCost = 0.0f;
		this.nodeTotalCost = 1.0f;
		
		this.parent = null;
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		float radius = 2.0f;
	    Gizmos.DrawSphere(transform.position,radius);
	}
	void Start()
	{
		this.position = transform.position;
		player = GameObject.FindGameObjectWithTag("Player");
        objmanager = ObjectiveManager.Instance;
	}

   public void GetDistanceFromPlayer()
    {
        if (player != null) DistanceFromPlayer = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));
    }
   public void GetDistanceFromGoal()
   {
       if (objmanager.GetCurrentObjective != null) DistanceFromGoal = Mathf.Abs(Vector3.Distance(transform.position, objmanager.GetCurrentObjective.Location));
   }

	public int CompareTo(System.Object obj)
	{
		Node node = (Node)obj;
		if(this.estimatedCost<node.estimatedCost)return -1;
		if(this.estimatedCost>node.estimatedCost)return 1;
		return 0;
	}
}
