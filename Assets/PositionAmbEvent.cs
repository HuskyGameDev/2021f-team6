using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAmbEvent : MonoBehaviour
{
    // Declare vars
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject emitter;
    private PolygonCollider2D colliderArea;


    // Start is called before the first frame update
    void Start()
    {
        colliderArea = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assign emitter position to closest point of collider area
        emitter.transform.position = colliderArea.ClosestPoint(player.transform.position);
    }
}
