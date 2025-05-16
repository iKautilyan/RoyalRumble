using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 0f;

    public int damageByBullet = 0;

    public float bulletLifeTime = 0f;

    private bool bulletImpact = false;

    public void FireBullet(float speed, float range, int damage = 25)
    {
        damageByBullet = damage;
        bulletSpeed = speed;
        bulletLifeTime = range / speed;
        StartCoroutine(ProjectileMotion());
    }

    IEnumerator ProjectileMotion()
    {
        float timeElapsed = 0f;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed;
        while (timeElapsed < bulletLifeTime && !bulletImpact)
        {
            timeElapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hitBox"))
        {
            collision.gameObject.GetComponent<Character>().DamageToCharacter(damageByBullet);
        }
        bulletImpact = true;
    }
}
