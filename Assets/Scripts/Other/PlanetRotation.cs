using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Rotation angle
    public Vector3 Rotation;

    // Rotation speed
    public float RotationSpeed;

    // Rotation Smoothness
    public float RotationSmoothness;

    // Rotation axis
    private Vector3 _rotationAxis;

    private void Update()
    {
        // Updating rotation axis
        _rotationAxis += Rotation * Time.deltaTime * RotationSpeed;

        // Applying the rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_rotationAxis),
            Time.deltaTime * RotationSmoothness);
    }
}