using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BulletScript : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    EnemyAI enemyAI;
    CameraFollow cameraF;
    FireController fireController;
    MermiController mermiController;
    [SerializeField] PostProcessingDeneme postProcessing;
    float damage;
    float enemyDamage = 15;
    float minDamage;
    float maxDamage;
    public bool vignetteWork;

    public bool isEnemy;
    private void Awake()
    {
        cameraF = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        fireController = GameObject.Find("CanvasUI").GetComponent<FireController>();
        mermiController = GameObject.Find("Player").GetComponent<MermiController>();
    }
    private void Start()
    {
        minDamage = fireController.weapons[fireController.currentIndex].minDamage;
        maxDamage = fireController.weapons[fireController.currentIndex].maxDamage;
        damage = Random.Range(minDamage, maxDamage);
        Destroy(this.gameObject, 2);
        if (mermiController.weaponController._weapons[fireController.currentIndex].mermiPrefab.name == "ShotGunMermi")
        {
            GameObject bullet = mermiController.weaponController._weapons[fireController.currentIndex].mermiPrefab;
            GetComponent<Rigidbody>().AddForce(fireController.ammoSpawn.forward * mermiController.speed);
        }
        if (isEnemy)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * mermiController.speed);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(fireController.ammoSpawn.forward * mermiController.speed);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !isEnemy)
        {
            enemyAI = other.GetComponent<EnemyAI>();
            TMP_Text var = Instantiate(textMesh, other.transform.Find("EnemiesCanvas (2)"));
            var.text = Mathf.RoundToInt(damage).ToString();
            var.color = Color.Lerp(Color.yellow, Color.red, damage / 40);
            other.transform.GetComponent<HealthEnemy>().health -= damage;
            enemyAI.ChasePlayer();
            var.rectTransform.anchoredPosition = new Vector2(Random.Range(1, 5), var.rectTransform.anchoredPosition.y);
            StartCoroutine(WaitText(var));
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            TMP_Text var = Instantiate(textMesh, other.transform.Find("Canvas"));
            var.text = Mathf.RoundToInt(enemyDamage).ToString();
            var.color = Color.Lerp(Color.yellow, Color.red, enemyDamage / 40);
            other.transform.GetComponent<HealthPlayer>().TakeDamage(enemyDamage);
            vignetteWork = true;
        }
        if (other.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator WaitText(TMP_Text var)
    {
        yield return new WaitForSeconds(.3f);
        Destroy(var);
    }
}
