/**
 *
 *
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 2022.3
 */

using UnityEngine;

public class SlowPersonMove : MonoBehaviour
{

#region Parameters
    [SerializeField] private float personalSpace = 2;   // m
    [SerializeField] private float speed = 2; // m/s
#endregion 

#region Components
#endregion

#region State
    private Transform[] others;
#endregion

#region Init & Destroy
    void Awake()
    {
        GetOtherPeopleInScene();
    }
#endregion Init

#region Update
    void Update()
    {
        MoveRandomly();
        Transform nearest = NearestNeighbour();
        MoveAway(nearest);
    }

    private void MoveRandomly()
    {
        Vector2 v = Random.insideUnitCircle;
        transform.Translate(v * speed * Time.deltaTime);
    }

    private void GetOtherPeopleInScene()
    {
        SlowPersonMove[] people = FindObjectsOfType<SlowPersonMove>();
        others = new Transform[people.Length];
        for (int i = 0; i < people.Length; i++) 
        {
            others[i] = people[i].transform;
        }

    }

    private Transform NearestNeighbour() 
    {
        Transform nearest = null;
        float minDistance = personalSpace;

        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] == transform) 
            {
                // ignore ourself
                continue;
            }

            float distance = Vector3.Distance(others[i].position, transform.position);

            if (distance < minDistance)
            {
                nearest = others[i];
                minDistance = distance;
            }
        }

        return nearest;
    }    

    private void MoveAway(Transform nearest) 
    {
        if (nearest != null)
        {
            Vector3 dir = transform.position - nearest.position;
            dir = dir.normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
#endregion Update

#region Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, personalSpace);
    }
#endregion Gizmos
}
