using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    private Collider2D thisCollider;
    private Rigidbody2D rigidBody;

    public int maxChains = 3;
    private List<GameObject> chains = new List<GameObject>();

    public GameObject searchPrefab, chain;
    private Collider2D closest = null;
    private ColliderDistance2D closestD;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();


        //float dist = float.MaxValue;

        //List<Collider2D> colliders = new List<Collider2D>();
        //ContactFilter2D filter = new ContactFilter2D();
        //int cols = thisCollider.OverlapCollider(filter, colliders);
        //foreach (Collider2D collider in colliders)
        //{
        //    if (collider.tag == "Enemy" && collider.Distance(thisCollider).distance < dist)
        //    {
        //        closestD = collider.Distance(thisCollider);
        //        dist = closestD.distance;
        //        closest = collider;

        //    }
        //}
        GameObject searchRadius = (GameObject)Instantiate(searchPrefab, transform);
        SearchRadiusController search = searchRadius.GetComponent<SearchRadiusController>();
        List<Collider2D> list = new List<Collider2D>();
        list.Add(thisCollider);
        closest = search.findClosest(12f, "Enemy", rigidBody.rotation, 50f, list);
        closestD = search.closestD;
        dist = search.distance;
        Destroy(searchRadius);

        if (closest != null)
        {
            // Play Lightning sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Attacks/Lightening_Atk");

            //dist += 300;
            rigidBody.rotation = Mathf.Atan2(closestD.normal.x, closestD.normal.y) * Mathf.Rad2Deg * -1 - 90;
            transform.localScale = new Vector3(dist, .25f, 1);

            float newX = Mathf.Cos(rigidBody.rotation * Mathf.Deg2Rad);
            float newY = Mathf.Sin(rigidBody.rotation * Mathf.Deg2Rad);
            transform.position += new Vector3(dist * newX * .5f, dist * newY * .5f, 0 );

            list.Add(closest);
            gameObject.GetComponent<AttackType>().hit(closest);

            for (int i = 0; i < maxChains; i++)
            {
                GameObject chainSearchRadius = (GameObject)Instantiate(searchPrefab, closest.transform);
                SearchRadiusController chainSearch = chainSearchRadius.GetComponent<SearchRadiusController>();
                Collider2D chainClosest = chainSearch.findClosest(5f, "Enemy", list);

                if (chainClosest != null)
                {
                    GameObject newChain = (GameObject)Instantiate(chain, closest.transform.position, closest.transform.rotation);
                    Rigidbody2D chainRB = newChain.GetComponent<Rigidbody2D>();
                    chainRB.rotation = Mathf.Atan2(chainSearch.closestD.normal.x, chainSearch.closestD.normal.y) * Mathf.Rad2Deg * -1 - 90;
                    newChain.transform.localScale = new Vector3(chainSearch.distance, .25f, 1);

                    float chainNewX = Mathf.Cos(chainRB.rotation * Mathf.Deg2Rad);
                    float chainNewY = Mathf.Sin(chainRB.rotation * Mathf.Deg2Rad);
                    newChain.transform.position += new Vector3(chainSearch.distance * chainNewX * .5f, chainSearch.distance * chainNewY * .5f, 0);

                    chains.Add(newChain);
                    list.Add(chainClosest);
                    gameObject.GetComponent<AttackType>().hit(chainClosest);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject c in chains)
           Destroy(c, .5f);
        Destroy(gameObject, .5f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
