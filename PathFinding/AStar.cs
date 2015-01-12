using UnityEngine;
using System.Collections;
/*
 * AStar pathfinding algorithm
*/
public class AStar {

    public static PriorityQueue closedList,openList;
    private static ArrayList list;
	private static float HeuristicEstimateCost(Node curNode,Node goalNode)
	{
		Vector3 vecCost = curNode.position - goalNode.position;
		return vecCost.magnitude;
	}
	public static  ArrayList FindPath(Node Start,Node Goal)
	{
		openList = new PriorityQueue();
        openList.Clear();
		openList.Push(Start);
		Start.nodeTotalCost = 0;
		Start.estimatedCost = HeuristicEstimateCost(Start,Goal);

		closedList = new PriorityQueue();
        closedList.Clear();
		Node node = null;
		
		while(openList.Length!=0)
		{
			node = openList.First();
			if(node.position == Goal.position)
			{
				return CalculatePath(node);
			}
			ArrayList neighbours = new ArrayList();
			NodeManager.Instance.GetNeighbours(node,neighbours);
			for(int i=0;i<neighbours.Count;i++)
			{
				Node neighbourNode =(Node)neighbours[i];
				if(!closedList.Contains(neighbourNode))
				{
					float cost = HeuristicEstimateCost(node,neighbourNode);
					float totalcost = node.nodeTotalCost+cost;
					float neighbournodeEstCost = HeuristicEstimateCost(neighbourNode,Goal);
					
					neighbourNode.nodeTotalCost = totalcost;
					neighbourNode.parent = node;
					neighbourNode.estimatedCost = totalcost+neighbournodeEstCost;
					
					if(!openList.Contains(neighbourNode))
					{
						openList.Push(neighbourNode);
					}
				}
			}
			closedList.Push(node);
			openList.Remove(node);	
		}
        if (node.position != Goal.position)
        {
            Debug.Log("Goal Not Found");
            return null;
        }
		return CalculatePath(node);
	}
   
	private static ArrayList CalculatePath(Node node)
	{
        if (list == null) list = new ArrayList();
       
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
		list.Reverse();
		return list;
	}
    public static  void ClearPath()
    {
        if (openList != null)
        {
            foreach (Node n in openList.nodes)
            {
                n.parent = null;
            }
        }
        if (closedList != null)
        {
            foreach (Node n in closedList.nodes)
            {
                n.parent = null;
            }
        }
        if (list != null)
        {
            foreach (Node n in list)
            {
                n.parent = null;
            }
            list.Clear();
        }
        
    }


}

