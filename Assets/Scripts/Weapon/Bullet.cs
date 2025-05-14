using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 0f;

    public float bulletLifeTime = 0f;

    private bool bulletImpact = false;

    public void FireBullet(float speed, float range)
    {
        bulletSpeed = speed;
        bulletLifeTime = range / speed;
        StartCoroutine(ProjectileMotion());
    }

    IEnumerator ProjectileMotion()
    {
        float timeElapsed = 0f;
        while(timeElapsed < bulletLifeTime || !bulletImpact)
        {
            transform.position += transform.forward.normalized * bulletSpeed * Time.deltaTime;
            timeElapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("hitBox"))
        {
            //other.transform.parent
        }
        bulletImpact = true;
    }
}
