using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleController : MonoBehaviour
{
    public Sprite Blood1, Blood2, Blood3;
    private int randomSprite;
    void Start()
    {
        randomSprite = Random.Range(1,4);
        if (randomSprite == 1) 
        {
            GetComponent<SpriteRenderer>().sprite = Blood1;
        }
        if (randomSprite == 2)
        {
            GetComponent<SpriteRenderer>().sprite = Blood2;
        }
        if (randomSprite == 3)
        {
            GetComponent<SpriteRenderer>().sprite = Blood3;
        }
        float randomRotation = Random.Range(1f,360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,randomRotation));
        Destroy(gameObject, 3f);
    }
}
