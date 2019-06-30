using UnityEngine;

public class InfiniteStarField : MonoBehaviour
{
    // Particle system
    public ParticleSystem ParticleSystem;

    // Number of stars maximum
    public int StarsMax = 100;

    // Size of a star
    public float StarSize = 1;

    // Distance between each star
    public float StarDistance = 10;

    public float StarClipDistance = 1;

    // Object's transform
    private Transform _transform;

    // Array of particles
    private ParticleSystem.Particle[] _points;

    // Distance between each ray multiply by him
    private float _starDistanceSqr;

    private float _starClipDistanceSqr;

    private void Start()
    {
        // Get object's transform
        _transform = transform;

        // Calculate the distance between each ray multiply by him
        _starDistanceSqr = StarDistance * StarDistance;

        _starClipDistanceSqr = StarClipDistance * StarClipDistance;
    }

    private void CreateStars()
    {
        // Allocate array of particules
        _points = new ParticleSystem.Particle[StarsMax];

        for (var i = 0; i < StarsMax; i++)
        {
            // Sets the position, the color and the size of each star
            _points[i].position = Random.insideUnitSphere * StarDistance + _transform.position;
            _points[i].startColor = new Color(1, 1, 1, 1);
            _points[i].startSize = StarSize;
        }
    }

    private void Update()
    {
        // Checks if the array of particules is null
        if (_points == null)
            CreateStars();

        for (var i = 0; i < StarsMax; i++)
        {
            // Updating position of each star
            if ((_points[i].position - _transform.position).sqrMagnitude > _starDistanceSqr)
                _points[i].position = Random.insideUnitSphere.normalized * StarDistance + _transform.position;

            if (!((_points[i].position - _transform.position).sqrMagnitude <= _starClipDistanceSqr))
                continue;

            // Updating color and size of each star
            var percent = (_points[i].position - _transform.position).sqrMagnitude / _starClipDistanceSqr;

            _points[i].startColor = new Color(1, 1, 1, percent);
            _points[i].startSize = percent * StarSize;
        }

        // Sets the particles
        ParticleSystem.SetParticles(_points, _points.Length);
    }
}