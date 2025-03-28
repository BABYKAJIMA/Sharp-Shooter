using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animatior;
    [SerializeField] GameObject hitVFXPrefab;   
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int damgeAmount = 1;
    
    StarterAssetsInputs starterAssetsInputs;

    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    void Update()
    {
        HandleShoot();
    }

   void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;
        {
            muzzleFlash.Play();
            animatior.Play(SHOOT_STRING, 0, 0f);
            starterAssetsInputs.ShootInput(false);

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                Instantiate(hitVFXPrefab, hit.point, Quaternion.identity);

                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                enemyHealth?.TakeDamge(damgeAmount);
            }
        }
    }
}
