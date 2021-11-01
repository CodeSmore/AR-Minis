using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnMovement : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float delayCoefficient;

    float distance;

    bool movementEnabled, movementComplete;
    float riseTimer, shakeTimer;
    float riseStartValue, riseEndValue;

    private void Awake()
    {
        distance = transform.parent.transform.localScale.x;
        movementEnabled = false;
        movementComplete = false;
        riseStartValue = transform.position.y;
        riseEndValue = transform.position.y + distance;
        riseTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // rise and shake tile until it is fully spawned
        if (!movementComplete && movementEnabled) {
            TileRise();
            TileShake();
        } else if (movementComplete)
        {
            this.enabled = false;
        }

    }

    void TileRise()
    {
        float newYPos;

        newYPos = Mathf.Lerp(riseStartValue, riseEndValue, riseTimer / duration);
        riseTimer += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);

        movementComplete = newYPos >= riseEndValue ? true : false;
    }

    void TileShake()
    {

    }

    public void StartMovement(int tileIndex)
    {
        StartCoroutine(HandleMovement(tileIndex));
    }

    IEnumerator HandleMovement(int tileIndex)
    {
        yield return new WaitForSeconds(tileIndex * delayCoefficient);
        movementEnabled = true;
    }
}
