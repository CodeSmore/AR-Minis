using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionHandler : MonoBehaviour
{
    [SerializeField] GameObject particleSystemPrefab;
    [SerializeField] string tagToDestroyOn;

    ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToDestroyOn) {
            GameObject p_s_Instance = objectPooler.SpawnFromPool("ExplosionParticleSystem", transform.position, Quaternion.identity);
            p_s_Instance.GetComponent<ParticleSystemRenderer>().material = this.GetComponent<Renderer>().material;
            gameObject.SetActive(false);
        }
    }
}
