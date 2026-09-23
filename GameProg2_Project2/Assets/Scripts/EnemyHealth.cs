using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Material Types")]
    [SerializeField] MeshRenderer[] _mRender;
    public Material baseMat;
    public Material hitFlashMat;

    [Header("Enemy Health Count")]
    public int enemyHealth = 5;

    [Header("Explosion")]
    public GameObject explosionEffect;

    private void Start()
    {
        foreach(MeshRenderer m in _mRender)
        {
            m.material = baseMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        enemyHealth--;
        if(enemyHealth <= 0)
        {
            Instantiate(explosionEffect, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            foreach(MeshRenderer m in _mRender)
            {
                m.material = hitFlashMat;
            }
            yield return new WaitForSeconds(0.1f);
            foreach (MeshRenderer m in _mRender) ;
        }
    }
}
