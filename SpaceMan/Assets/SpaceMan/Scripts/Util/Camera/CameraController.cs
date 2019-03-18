using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);
    public float dampingTime = -2.5f;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    private void LateUpdate()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    private void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampingTime);
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
