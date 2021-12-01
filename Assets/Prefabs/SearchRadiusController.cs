using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRadiusController : MonoBehaviour
{
    private Collider2D thisCollider;
    private CircleCollider2D circleCollider2D;

    public Collider2D closest = null;
    public int numCols;
    public ColliderDistance2D closestD;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Collider2D findClosest(float radius, string tag, List<Collider2D> ignore)
    {
        closest = null;
        thisCollider = GetComponent<Collider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        circleCollider2D.radius = radius;

        float dist = float.MaxValue;
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        numCols = thisCollider.OverlapCollider(filter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (!ignore.Contains(collider) && collider.tag == tag && collider.Distance(thisCollider).distance < dist)
            {
                closestD = collider.Distance(thisCollider);
                dist = closestD.distance;
                closest = collider;

            }
        }
        distance = dist + radius;
        return closest;
    }

    public Collider2D findClosest(float radius, string tag, float angle, float range, List<Collider2D> ignore)
    {
        closest = findClosest(radius, tag, ignore);
        Vector3 dir = closest.transform.position - thisCollider.transform.position;
        //float closestAngle = Vector3.SignedAngle(closest.transform.position - thisCollider.transform.position, Vector3.right, Vector3.zero);
        float closestAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log("Closest Angle: " + closestAngle);
        while (closest != null && Mathf.Abs(closestAngle - angle) % 360 > range)
        {
            ignore.Add(closest);
            if(findClosest(radius, tag, ignore) == null) return null;
            dir = closest.transform.position - thisCollider.transform.position;
            closestAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
        return closest;
    }
}

