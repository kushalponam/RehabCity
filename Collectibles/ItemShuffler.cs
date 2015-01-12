using UnityEngine;
using System.Collections;

public class ItemShuffler : MonoBehaviour {

    public GameObject[] items;

	void Start () {
        items = GameObject.FindGameObjectsWithTag("items");
        KnuthShuffle();
	}

    /// <summary>
    /// Each item has a random number generated.
    /// we are sorting the array using insertion sort.
    /// sorting is done for positions.
    /// </summary>
    private int ShuffleCounter = 0;
    void Shuffle()
    {
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = i; j > 0 && less(items[j].GetComponentInChildren<CollectibleItem>().randomnumber, items[j - 1].GetComponentInChildren<CollectibleItem>().randomnumber); j--)
            {
                exchange(items, j, j - 1);
                ShuffleCounter++;
               // Debug.Log("Shuffled" +j.ToString()+ " and " + (j-1).ToString()+" for "+ShuffleCounter.ToString()+" time ");
            }
            //Debug.Log(i);
        }
        Debug.Log("Shuffled items and it took " + ShuffleCounter + " Cycles");
    }
    /// <summary>
    /// efficient shuffle algorithm
    /// </summary>
    void KnuthShuffle()
    {
        for (int i = 1; i < items.Length; i++)
        {
            int randomnumber = Random.Range(0, i);
            exchange(items, i, randomnumber);
            ShuffleCounter++;
        }
        Debug.Log("Shuffled items and it took " + ShuffleCounter + " Cycles" );
    }
    bool less(int a, int b)
    {
        if (a <= b) return true;
        return false;
    }

    /// <summary>
    /// exchange both positions and 
    /// also the array items.
    /// </summary>
    void exchange(GameObject[] itemarray, int a, int b)
    {
        if (a == b) return;
        Vector3 temp = itemarray[b].transform.position;
        GameObject tempg = itemarray[b];
        itemarray[b].transform.position = itemarray[a].transform.position;
        itemarray[b] = itemarray[a];
        itemarray[a].transform.position = temp;
        itemarray[a] = tempg;
    }
}
