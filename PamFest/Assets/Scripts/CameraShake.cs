using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject mainCamera;
    float currentTime = 0.1f;

    public IEnumerator shakeCamera(float duration, float intensity)
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        currentTime = 0;

        while (currentTime < duration)
        {
            float xShake = Random.Range(-1f, 1f) * intensity;
            float yShake = Random.Range(-1f, 1f) * intensity;

            mainCamera.transform.position = new Vector3(xShake + cameraPosition.x, yShake + cameraPosition.y, cameraPosition.z);

            currentTime += Time.deltaTime;

            //intensity *= 0.6f;

            yield return null;
        }

    mainCamera.transform.position = cameraPosition;
    }
}
