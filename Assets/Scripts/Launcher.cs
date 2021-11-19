using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] float launchSpeed;
    [SerializeField] float autoFireBetweenDelay;
    [SerializeField] Vector3 positionOffset;

    [SerializeField] bool manualFire;

    [Space(10)]
    [SerializeField] GameObject launchOrigin;

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance; 
    }

    // Update is called once per frame
    void Update()
    {
        if (manualFire) {
            Launch();
            manualFire = false;
        }
    }

    public void ToggleAutomaticFire(bool activate)
    {
        if (activate)
        {
            StartCoroutine("AutoLaunch");
        } else
        {
            StopCoroutine("AutoLaunch");
        }
    }

    void Launch()
    {
        Vector3 originForward = launchOrigin.transform.forward;
        Vector3 launchPosition = launchOrigin.transform.position + originForward + positionOffset;

        GameObject ammo = objectPooler.SpawnFromPool("Ammo", launchPosition, Quaternion.identity);

        ammo.GetComponent<Rigidbody>().velocity = originForward * launchSpeed;
    }

    IEnumerator AutoLaunch()
    {
        // Initial Delay
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            Launch();
            yield return new WaitForSeconds(autoFireBetweenDelay);
        }

    }
}
