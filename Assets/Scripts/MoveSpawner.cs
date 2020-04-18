using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveSpawner : MonoBehaviour
{
    private bool rightPosition;
    private Rigidbody2D virusRb;

    [Range(.1f, 2f)]
    public float extraForce;
    public float MovingTime, minSpawnTime, maxSpawnTime;
    public float virusForce;
    public Vector3 offset;
    public Sprite[] virusesSprites;
    public GameObject virus;
    public GameObject[] targets;

    public void AddForce()
    {
        virusForce += extraForce;
    }
    public void Moving()
    {
        if (!rightPosition)
        {
            transform.DOMoveX(8f, MovingTime);
            if (transform.position.x > 7.6f)
            {
                rightPosition = true;
            }
        }


        if (rightPosition)
        {
            transform.DOMoveX(-8f, MovingTime);
            if (transform.position.x < -7.6f)
            {
                rightPosition = false;
            }
        }
    }

    public void Spawning()
    {
        virus = Instantiate(virus, transform.position, Quaternion.identity);
        virus.GetComponent<SpriteRenderer>().sprite = virusesSprites[Random.Range(0, virusesSprites.Length)];
        virusRb = virus.GetComponent<Rigidbody2D>();

        VirusShoot();
    }

    public void VirusShoot()
    {
        offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
        Vector2 dir = (targets[Random.Range(0, 2)].transform.position + offset - this.transform.position).normalized;
        dir *= virusForce;
        virusRb.velocity = dir;
        print(virusRb.velocity);
    }

    void Start()
    {
        InvokeRepeating(nameof(Spawning),0.1f, Random.Range(minSpawnTime, maxSpawnTime));
        InvokeRepeating(nameof(AddForce), 3f, 5f);
    }

   
    void Update()
    {
        Moving();
    }
}
