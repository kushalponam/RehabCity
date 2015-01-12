using UnityEngine;
using System.Collections;

public class PathFindingController : MonoBehaviour {

    public Node StartNode;
    public Node GoalNode;
    public ArrayList PathArray;
   
    private NodeManager nodemanager;
    private LineRenderer line;
    private CompassArrow arrow;

    private DrawObjectiveList drawobjectivelist;
    public static bool showGreenPath = true;
    public Color PathColor;
    private static PathFindingController s_Instance = null;
    public static PathFindingController Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(PathFindingController)) as PathFindingController;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate Pathfinding");
                }
            }
            return s_Instance;
        }
    }
	// Use this for initialization
	void Start () {
        nodemanager = NodeManager.Instance;
        PathArray = new ArrayList();
        line = GetComponent<LineRenderer>();
        arrow = CompassArrow.Instance;
        drawobjectivelist = DrawObjectiveList.Instance;
	}
    public void FindPath()
    {
        AStar.ClearPath();
        StartNode = nodemanager.GetStartNode();
        GoalNode = nodemanager.GetGoalNode();
        PathArray = AStar.FindPath(StartNode, GoalNode);
        arrow.SetPathNodes(PathArray);
        line.SetVertexCount(PathArray.Count);
    }
	// Update is called once per frame
	void Update () {
        RedesignPath();
        DrawPathLine();
	}
    void DrawPathLine()
    {
        if (PathArray == null)
        {
            return;
        }
        PathColor.a = (drawobjectivelist.MinimizeObjective && !showGreenPath) ? 0 : 1;
        line.gameObject.renderer.material.color = PathColor;
        if (PathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in PathArray)
            {
                if (index < PathArray.Count)
                {
                    Node nextNode = (Node)PathArray[index];
                    line.SetPosition(index-1, node.position);
                    line.SetPosition(index, nextNode.position);
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            }
        }
    }
    void RedesignPath()
    {
        if (StartNode == null || GoalNode == null || nodemanager.GetNearestNode() == null) return;
        if (!PathArray.Contains(nodemanager.GetNearestNode()))
        {
            FindPath();
        }
    }

}
