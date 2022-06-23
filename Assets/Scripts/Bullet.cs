using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Vector3.forward * Time.deltaTime * 70f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanDestroy")
            Destroy(collision.gameObject);
        if (collision.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
