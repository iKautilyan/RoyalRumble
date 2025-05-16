using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletInstantiatePoint;

    public float range, speed;

    public int damageByWeapon = 25;

    public float fireRate = 0f;

    public bool loaded = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            OnTriggerPulled();
    }

    public void PullTrigger()
    {
        OnTriggerPulled();
    }

    private void OnTriggerPulled()
    {
        if(loaded)
        {
            StartCoroutine(FireWeapon());
        }
    }

    IEnumerator FireWeapon()
    {
        loaded = false;
        Bullet bullet = Instantiate(bulletPrefab, bulletInstantiatePoint.position, bulletInstantiatePoint.rotation).GetComponent<Bullet>();
        bullet.FireBullet(speed, range, damageByWeapon);
        yield return new WaitForSeconds(fireRate);
        loaded = true;
    }
}
