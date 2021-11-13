using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHit : MonoBehaviour
{
    [SerializeField] float coolDown = 0.15f;
    float lastFireTime = 0;
    [SerializeField] int maxAmmo = 120;
    [SerializeField] int magSize = 30;
    [SerializeField] float range = 10f;
    [SerializeField] int damage = 20;
    int currentAmmo, currentMagAmmo;
    public Transform cam;
    public GameObject bloodPrefab;
    public GameObject bulletHole;
    public ParticleSystem muzzleFlash;
    public GameObject messageText;
    public GameObject playerObject;
    public GameObject magObject;
    public GameObject lightt;
    public AudioSource ak47AudioSource;
    public AudioClip ak47ShootAudioClip, ak47ReloadAudioClip;
    PlayerHealth playerHealth;
    public Text currentMagAmmoText, currentAmmoText, playerHealthText;
    private void Start()
    {
        currentAmmo = maxAmmo - magSize;
        currentMagAmmo = magSize;
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Shoot();
            }
        }
        BilgiTextleri();
    }

    private void Reload()
    {
        ak47AudioSource.PlayOneShot(ak47ReloadAudioClip, 0.3f);
        if(currentAmmo == 0 || currentMagAmmo == magSize)
        {
            return;
        }
        if(currentAmmo < magSize)
        {
            currentMagAmmo = currentAmmo + currentMagAmmo;
            currentAmmo = 0;
        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            currentMagAmmo = magSize;
        }
        currentMagAmmoText.text = currentMagAmmo.ToString();
        currentAmmoText.text = currentAmmo.ToString();
        GameObject newMagObject = Instantiate(magObject, magObject.transform.position, magObject.transform.rotation);
        newMagObject.AddComponent<Rigidbody>();
        newMagObject.GetComponent<Rigidbody>().mass = 0.1f;
        Destroy(newMagObject, 5);
    }

    private bool CanFire()
    {
        if (currentMagAmmo > 0 && lastFireTime + coolDown < Time.time)
        {
            lastFireTime = Time.time + coolDown;
            return true;
        }        
        return false;        
    }

    private void BilgiTextleri()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 2))
        {
            if (hit.collider.gameObject.CompareTag("Heal"))
            {
                messageText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (playerHealth.GetHeal() < 100)
                    {
                        playerHealth.AddHealth(10);
                        Destroy(hit.transform.gameObject);
                    }
                }                
            }
            else
            {
                messageText.SetActive(false);
            }
            if (hit.collider.gameObject.CompareTag("Ammo"))
            {
                messageText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if ((currentAmmo + currentMagAmmo) < 120)
                    {
                        currentAmmo += 30;
                        if (currentAmmo + currentMagAmmo > 120)
                        {
                            currentAmmo = 120 - currentMagAmmo;
                        }
                        currentAmmoText.text = currentAmmo.ToString();
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            messageText.SetActive(false);
        }
    }

    private void Shoot()
    {
        ak47AudioSource.PlayOneShot(ak47ShootAudioClip, 0.3f);
        currentMagAmmo -= 1;
        Debug.Log("Sarjordeki mermi sayisi " + currentMagAmmo + " -----Toplam mermi sayisi: " + currentAmmo);
        currentMagAmmoText.text = currentMagAmmo.ToString();
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.transform.tag == "Zombie")
            {
                hit.transform.GetComponent<ZombiHealth>().Hit(damage);
                GenerateBloodEffect(hit);
            }
            else
            {
                GenerateBulletHoleEffect(hit);
            }
        }
        transform.localEulerAngles = new Vector3(Random.Range(-2, 3), Random.Range(-5, 6), Random.Range(-2, 3));
        muzzleFlash.Play(true);
        StartCoroutine("LightFonks");
    }
    IEnumerator LightFonks()
    {
        lightt.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        lightt.SetActive(false);
    }
    private void GenerateBulletHoleEffect(RaycastHit hit)
    {
        GameObject holeObject = Instantiate(bulletHole, hit.point, Quaternion.identity);
        holeObject.transform.rotation = Quaternion.FromToRotation(-bulletHole.transform.forward, hit.normal);//mermi deliği rostasyonunu normal ile tersi yönde döndürdük. yoksa mermi deliği duvarın içine bakıyor.
    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
        Debug.Log("Kan Efekti oluşturuldu.");
    }
}
