using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollection : MonoBehaviour
{
    [SerializeField] int maximumAmmo;

    [Space(10)]
    Queue<GameObject> ammoQueue = new Queue<GameObject>();

    public void AddAmmo(GameObject newAmmo) {
        newAmmo.transform.parent = this.transform;
        ammoQueue.Enqueue(newAmmo);

        if (ammoQueue.Count > maximumAmmo) {
            Destroy(ammoQueue.Dequeue());
        }
    }

    public void DestroyAllAmmo()
    {
        // Note: Queue count decreases as elements are dequeued, so must store Queue.Count ahead of time :P
        int queueLength = ammoQueue.Count;
        for (int i = 0; i < queueLength; ++i) {
            Destroy(ammoQueue.Dequeue());
        }
    }
}
