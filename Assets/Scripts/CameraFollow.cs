using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //Target to follow

    public float smoothSpeed=0.125f; //How fast the camera will lock on onto the target

    public Vector3 offset; //Distance from player

    
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition,smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
