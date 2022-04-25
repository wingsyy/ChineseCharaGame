using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlatformEffector2D platform2D;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        platform2D = GetComponent<PlatformEffector2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("ArrowIn"))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            platform2D.enabled= true;
        }
    }
}
