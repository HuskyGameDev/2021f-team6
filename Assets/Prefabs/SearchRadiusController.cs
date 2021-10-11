using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRadiusController : MonoBehaviour
{
    private Collider2D collider2D;
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
        collider2D = GetComponent<Collider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        circleCollider2D.radius = radius;

        float dist = float.MaxValue;
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        numCols = collider2D.OverlapCollider(filter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (!ignore.Contains(collider) && collider.tag == tag && collider.Distance(collider2D).distance < dist)
            {
                closestD = collider.Distance(collider2D);
                dist = closestD.distance;
                closest = collider;

            }
        }
        distance = dist + radius;
        return closest;
    }
}
