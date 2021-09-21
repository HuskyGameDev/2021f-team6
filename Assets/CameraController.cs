using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
    }
}
