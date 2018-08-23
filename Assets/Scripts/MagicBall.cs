using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    float xMax;
    float yMax;
    float xMin;
    float yMin;


    Vector3 dir;
    public float force;
    public Rigidbody2D rb;

    void Start()
    {
        xMax = 500;
        yMax = 500;
        xMin = -500;
        yMin = -500;
        Vector3 m = Input.mousePosition;
        m = new Vector3(m.x, m.y, transform.position.y);
        Vector3 p = Camera.main.ScreenToWorldPoint(m);
            //Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //dir = (Input.mousePosition - target).normalized;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(p * force);
        }

        // Update is called once per frame
        void Update()
        {
            
        if (transform.position.x >xMax|| transform.position.x<xMin|| transform.position.y <yMin || transform.position.y >yMax)
            {
                DestroyProjectile();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("GroundMonster"))
            {
                other.GetComponent<GroundMonsterAI>().TakeDamage();
                DestroyProjectile();
            }
            if (other.CompareTag("AirMonster"))
            {
                other.GetComponent<AirMonster>().TakeDamage();
                DestroyProjectile();
            }
        }

        void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }

