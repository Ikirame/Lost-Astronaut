using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset;

    public Transform LookAt;
   
    public float RotateSpeed = 5.0f;

    public float MinVerticalAngle = 0.0f;
    public float MaxVerticalAngle = 50.0f;

    public bool InvertYAxis = false;

    public Transform pivot;


    // Variables for zoom
    public float MinOrthographicSize;
    public float MaxOrthographicSize;
    public float SmoothSpeed;
    public float Increment;
    
    private Vector2 _currentDir;
    

    public bool CanMove = true;

    private void Start()
    {
        pivot.transform.position = LookAt.position;
        pivot.transform.parent = null;
    }

    private void LateUpdate()
    {
        if (!CanMove)
            return;

        float horizontal = Input.GetAxis("Mouse X") * RotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * RotateSpeed;

        _currentDir.x += horizontal;
        if (InvertYAxis)
            _currentDir.y += vertical;
        else
            _currentDir.y += -vertical;

        pivot.Rotate(0, horizontal, 0);

        _currentDir.y = Mathf.Clamp(_currentDir.y, MinVerticalAngle, MaxVerticalAngle);
        
        var rotation = Quaternion.Euler(_currentDir.y, _currentDir.x, 0);

        transform.position = LookAt.position + rotation * Offset;
        transform.LookAt(LookAt.position);
    }

    public void InvvertYAxis()
    {    
        InvertYAxis = !InvertYAxis;
    }
}