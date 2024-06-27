using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek nesne (karakter)
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuşaklığı
    public Vector3 offset; // Kameranın hedefe göre ofseti

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
