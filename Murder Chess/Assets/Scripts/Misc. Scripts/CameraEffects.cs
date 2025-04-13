using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public float shakeMagnitude;
    public float shakeFrequency;
    Vector3 originalPos;
    Vector3 shakeDirection;
    public float timeTemp;
    float shakeTimeTemp;
    public bool startAnim;
    public float timeToMove;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (startAnim)
        {
            startAnim = false;
            timeTemp = timeToMove;
        }
        else if (timeTemp > 0)
        {
            timeTemp -= Time.deltaTime;
            if (shakeTimeTemp <= 0)
            {
                shakeTimeTemp = shakeFrequency;
                shakeDirection = new Vector3(0, 0, 0);
                shakeDirection.x = Random.Range(-100, 100);
                shakeDirection.y = Random.Range(-100, 100);
                shakeDirection.Normalize();
                shakeDirection *= shakeMagnitude;
                shakeDirection += originalPos;
            }
            if (shakeTimeTemp > 0)
            {
                shakeTimeTemp -= Time.deltaTime;
                if (shakeTimeTemp > shakeFrequency / 2)
                {
                    transform.localPosition = Vector3.Lerp(shakeDirection, originalPos, (shakeTimeTemp - (shakeFrequency / 2)) / (shakeFrequency / 2));
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(originalPos, shakeDirection, (shakeTimeTemp) / (shakeFrequency / 2));
                }
            }
        }
        else if (timeTemp > -1)
        {
            timeTemp = -2;
            transform.localPosition = originalPos;
        }
    }

    public static void ScreenShake(float duration = 0.25f, float shakeMagnitude = 0.1f, float shakeFrequency = 0.1f)
    {
        CameraEffects cameraEffects = FindFirstObjectByType<CameraEffects>();
        cameraEffects.shakeMagnitude = shakeMagnitude;
        cameraEffects.shakeFrequency = shakeFrequency;
        cameraEffects.timeToMove = duration;
        cameraEffects.startAnim = true;
    }
}
