using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviourPunCallbacks
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = 15f; // minimum angle in degrees
    public float yMaxLimit = 80f; // maximum angle in degrees
    public float zoomSpeed = 2.0f; // speed of zooming
    public float zoomMinLimit = 1.0f; // minimum distance between camera and target
    public float zoomMaxLimit = 7.0f; // maximum distance between camera and target

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;



    }
    
    void LateUpdate()
    {

        if (target != null)
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                // clamp the vertical angle between min and max limits
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
            }

            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, zoomMinLimit, zoomMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
        else
        {
            FindPlayer();

        }
    }

    private void FindPlayer()
    {
        foreach (var p in GameObject.FindObjectsOfType<PhotonView>())
        {

            if (p.IsMine)
            {
                target = p.transform;

                break;
            }
        }
    }
}









