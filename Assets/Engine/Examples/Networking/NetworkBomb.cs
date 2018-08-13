using UnityEngine;
using UnityEngine.Networking;

public class NetworkBomb : NetworkBehaviour {

    [SerializeField]
    private float lifeTime = 1f;
    private float timePassed = 0f;


    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifeTime)
        {
            if (GetComponent<SpriteRenderer>().enabled)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play();
                ExplodeBomb();
            }
            else if(!GetComponent<AudioSource>().isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }



    private void ExplodeBomb()
    {
       

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if ((player.transform.position - gameObject.transform.position).magnitude < 2f)
            {
                var health = player.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(10);
                }
            }
        }
    }

}
