using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAmbEvent : MonoBehaviour
{
    // Declare vars
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject emitter;
    PolygonCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assign emitter position to closest point of collider area
        emitter.transform.position = collider.ClosestPoint(player.transform.position);
    }
}
