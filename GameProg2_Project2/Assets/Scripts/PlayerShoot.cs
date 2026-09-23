using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerShoot : MonoBehaviour
{
    TS_Inputs inputs;

    [Header("Spawn Setup")]
    public Transform bulletSpawnPoint;

    [Header("Bullet Types")]
    public Rigidbody baseBullet;
    public float shotForce = 40f;

    [Header("Shoot Timing")]
    public float rateOfFire = 0.2f;
    public bool canShoot;

    [Header("Bullet Lifetime")]
    public float bulletLifetime = 3.0f;

    private void Awake()
    {
        inputs = new TS_Inputs();
        canShoot = true;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        if (inputs.Player.Shoot.IsPressed() && canShoot)
        {
            StartCoroutine(PlayerShot());
        }
    }

    IEnumerator PlayerShot()
    {
        canShoot = false;
        if (RumbleManager.instance != null)
        {
            RumbleManager.instance.RumblePulse(0.2f, 0.2f, 0.1f);
        }
        Rigidbody shot = Instantiate(baseBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        shot.AddForce( bulletSpawnPoint.forward * shotForce, ForceMode.Impulse);

        Destroy(shot.gameObject, bulletLifetime);

        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }
}