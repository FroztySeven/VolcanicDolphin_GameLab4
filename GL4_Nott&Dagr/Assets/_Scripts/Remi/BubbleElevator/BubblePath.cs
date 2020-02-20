using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePath : MonoBehaviour
{
    private Color rayColor = Color.yellow;
    private List<Transform> pathObj = new List<Transform>();
    private Transform[] childArray;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        childArray = GetComponentsInChildren<Transform>();
        pathObj.Clear();

        foreach (Transform path_obj in childArray)
        {
            if (path_obj != this.transform)
            {
                pathObj.Add(path_obj);
            }
        }

        for (int i = 0; i < pathObj.Count;i++)
        {
            Vector3 position = pathObj[i].position;
            
            if (i > 0)
            {
                Vector3 previous = pathObj[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }
}
