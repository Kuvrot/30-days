using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] Inventory;
    public int currentWeapon = 0;

    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private float rotationLimit = 45f; // Limits the rotation 

    //WeaponController
    bool canShoot = true; //The weapon the player is using canShoot?
    bool aiming = false;

    //Components
    Animator animator;
    WeaponStats weaponStats;

    float mouseY, mouseX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        WeaponSetup();
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        yRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -rotationLimit, rotationLimit);
        yRotation = Mathf.Clamp(yRotation, -rotationLimit, rotationLimit);

        Camera.main.transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Inventory[currentWeapon].transform.localPosition = Vector3.Lerp(Inventory[currentWeapon].transform.localPosition, weaponStats.AimPosition, weaponStats.AimSpeed * Time.deltaTime);
            aiming = true;
        }
        else
        {
            Inventory[currentWeapon].transform.localPosition = Vector3.Lerp(Inventory[currentWeapon].transform.localPosition, weaponStats.NormalPosition, weaponStats.AimSpeed * Time.deltaTime);
            aiming = false;
        }

        if (weaponStats.currAmmo <= 0 && canShoot)
        {
            canShoot = false;
            StartCoroutine(WaitReload());
        }

    }

    private void FixedUpdate()
    {
        if (canShoot && weaponStats.currAmmo > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Fire");
                weaponStats.muzzleFlash.Play();
                Camera.main.GetComponent<Animator>().SetTrigger("Fire");
                yRotation -= 2.5f;
                Shoot();
                canShoot = false;
                StartCoroutine(WaitForWeapon());       
            }
        }
    }

    void WeaponSetup()
    {
        animator = Inventory[currentWeapon].GetComponent<Animator>();
        weaponStats = Inventory[currentWeapon].GetComponent<WeaponStats>();
    }

    void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward , out hit, 100))
        {
            if (hit.transform.CompareTag("Body"))
            {
                hit.transform.GetComponentInParent<EnemyController>().GetDamage(weaponStats.Damage);
                
            }

            if (hit.transform.CompareTag("Head"))
            {
                hit.transform.GetComponentInParent<EnemyController>().GetDamage(weaponStats.Damage * 2);

            }
            Debug.Log(hit.transform.name);
        }
        weaponStats.currAmmo--;
    }

    IEnumerator WaitForWeapon ()
    {
        yield return new WaitForSeconds(weaponStats.FireRate);
        canShoot = true;
        StopCoroutine(WaitForWeapon());
    }

    IEnumerator WaitReload()
    {
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(weaponStats.ReloadingTime);
        canShoot = true;
        weaponStats.currAmmo = weaponStats.maxAmmo;
        StopCoroutine(WaitReload());
    }
}
