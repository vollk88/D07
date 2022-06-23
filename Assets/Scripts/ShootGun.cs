using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletGun;
    [SerializeField] private GameObject placeOfSpawn;
    [SerializeField] private GameObject explotion;
    [SerializeField] private GameObject impactPart;

    [SerializeField] private AudioSource aExplotion;
    [SerializeField] private AudioSource aGunShot;
    [SerializeField] private AudioSource ABG;


    

    [SerializeField] private int ammo = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && ammo != 0)
        {
            ammo--;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 50f))
            {
                var t = Instantiate(bullet, placeOfSpawn.transform.position, placeOfSpawn.transform.rotation);
                t.SetActive(true);
                StartCoroutine(MoveBullet(t.transform.position, hit.point, t, explotion, aExplotion));
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 90f))
            {
                var t = Instantiate(bulletGun, placeOfSpawn.transform.position, placeOfSpawn.transform.rotation);
                t.SetActive(true);
                StartCoroutine(MoveBullet(t.transform.position, hit.point, t, impactPart, null));
                
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            aGunShot.Stop();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            aGunShot.Play();
    }

    private IEnumerator MoveBullet(Vector3 start, Vector3 end, GameObject bullet, GameObject particle, AudioSource a)
    {
        float i = 0;
        while (i <= 1)
        {
            var res = Vector3.Lerp(start, end, i);
            i += 0.1f;
            yield return new WaitForFixedUpdate();
            if (bullet.gameObject != null)
                bullet.transform.position = res;
        }
        if (a != null)
            a.Play();
        var part = Instantiate(particle, end, Quaternion.identity);
        Destroy(part, 2);
    }
}
