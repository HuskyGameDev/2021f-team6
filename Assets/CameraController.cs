using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    private Vector3 offset;
    public float MIN_X;
    public float MAX_X;
    public float MIN_Y;
    public float MAX_Y;
    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    //private void Update()
    //{
    //    transform.position = new Vector3(
    //    Mathf.Clamp(Player.transform.position.x, MIN_X, MAX_X),
    //    Mathf.Clamp(Player.transform.position.y, MIN_Y, MAX_Y),
    //    transform.position.z);
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Player.transform.position + offset;
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, MIN_X, MAX_X),
        Mathf.Clamp(transform.position.y, MIN_Y, MAX_Y),
        transform.position.z);
    }
}
