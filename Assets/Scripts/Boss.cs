using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public int AmmountExplosion;
    //death bolean
    public bool Death;
    
    //anim contains boss explosion
    public GameObject BossExplosion;
    public float maxHealth;
    public float currentHealth;
    public UnityEngine.UI.Image bossHealthBar;

    [Space]
    public GameObject projectilePrefab;
    public int currentProjectileDamage = 1;
    public float projectileShootDelay;

    [Space]
    public Transform leftPSL;  //left projectile spawn location
    public Transform rightPSL; //right projectile spawn location

    [Space]
    public int RotationZOffset; // Offset for RotationZ, -90 works fine
    SinglePlayer singleplayer; // ref to player script
    public Transform singleplayerTransform;// singleplayer gameobject in hierarchy
    Transform RedUnit;

    [Space]
    public ObstacleManager ADManager;//Area Denial Manager
    public float ADDelay;

    [Space]
    public Vector3 standLoc = new Vector2(0f, 5.9f);
    public GameObject CamZ2;
    public GameObject CamZ;

    private int lastAttack; //last used attack 1:Projectile, 2:Area Denial

    private Animator anim;

    private void Awake()
    {
        singleplayer = singleplayerTransform.GetComponent<SinglePlayer>();
        RedUnit = singleplayer.atkChar.GetComponent<Transform>(); //pivot is not always the red, use atkChar for the red character
        //Aim at Red Unit
        TargetRed();

        anim = GetComponent<Animator>();
        Death = false;

        currentHealth = maxHealth;
        bossHealthBar.fillAmount = currentHealth / maxHealth;
    }

    void Start()
    {
        StartCoroutine(Attack());
        StartCoroutine(CamWork());
    }

    IEnumerator Attack()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        while (Vector2.Distance(transform.position,standLoc)>0.1)  //before transform.position != standLoc
        {
            transform.position -= new Vector3(0, (10.81f - 5.9f)/6*Time.deltaTime, 0f);
            yield return null;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < 4; i++)
            {
                StartCoroutine("ProjectileShoot", leftPSL);
                yield return new WaitForSeconds(0.8f);
                StartCoroutine("ProjectileShoot", rightPSL);
                yield return new WaitForSeconds(0.8f);

            }
            yield return new WaitForSeconds(7f);
            StartCoroutine(AreaDenial());

            yield return null;
        }
        gameObject.SetActive(false);
        //Win
    }

    IEnumerator CamWork()
    {
        yield return new WaitForSeconds(0.1f);
        CamZ2.SetActive(false);
        yield return new WaitForSeconds(5.2f);
        CamZ.SetActive(false);
    }

    void Update()
    {
        if(currentHealth > 0)
        {
            return;

        }
        if (currentHealth <= 0)
        {
            Death = true;
            gameObject.SetActive(false);
            //set boss death or something 

            while (AmmountExplosion > 0)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2),0);
                var instance = Instantiate(BossExplosion, pos, Quaternion.identity);
                Destroy(instance, 3f);
                
                AmmountExplosion--;

            }

            if (Death)
            {

                GameManager.instance.BossDie();
            }


        }
    }

    public void Damage(float amt)
    {
        currentHealth-= amt;
        bossHealthBar.fillAmount = currentHealth / maxHealth;
    }

    public IEnumerator AreaDenial()
    {
        GetComponent<AudioSource>().PlayDelayed(projectileShootDelay / 10f - 0.5f);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(ADDelay);
        ADManager.Spawn();
    }

    public void ProjectileShoot(Transform spawnPoint)
    {
        TargetRed();
        Projectile p = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<Projectile>();
        p.damage = currentProjectileDamage;
        p.TestShoot();

    }
    
    public IEnumerator TestProjectileShoot()
    {
        GameObject projectile;
        while (true)
        {
            TargetRed();

            int r = Random.Range(0, 1);
            if (r == 1)
            {
                projectile = Instantiate(projectilePrefab, leftPSL.position, leftPSL.rotation);
            }
            else
            {
                projectile = Instantiate(projectilePrefab, rightPSL.position, rightPSL.rotation);
            }

            projectile.GetComponent<Projectile>().damage = currentProjectileDamage;
            projectile.GetComponent<Projectile>().TestShoot();

            yield return new WaitForSeconds(3f);
        }
    }

    ////////////////// Target The Red by Farhan
    void TargetRed()
    {
        Vector3 RedPos = RedUnit.position;
        // direction calculation
        Vector3 differenceForLeftPSL = RedPos - leftPSL.position;
        Vector3 differenceForRightPSL = RedPos - rightPSL.position; 
        //anglee of Z axis Calculation
        float LeftPSLRotationZ = Mathf.Atan2(differenceForLeftPSL.y, differenceForLeftPSL.x) * Mathf.Rad2Deg;
        float RightPSLRotationZ = Mathf.Atan2(differenceForRightPSL.y, differenceForRightPSL.x) * Mathf.Rad2Deg;
        leftPSL.transform.rotation = Quaternion.Euler(0,0, LeftPSLRotationZ + RotationZOffset + Random.Range(-3f, 3f));
        rightPSL.transform.rotation = Quaternion.Euler(0, 0, RightPSLRotationZ + RotationZOffset + Random.Range(-3f, 3f));

    }
    void DeathEffects()
    {



    }
}
