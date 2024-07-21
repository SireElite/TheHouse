using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxWaitingTime = 15f;
    [SerializeField] private float _minWaitingTime = 5f;

    public Transform SpawnPoint => _spawnPoint;

    private int _randomWaypointIndex;
    private float _waitingTime;

    private void OnValidate()
    {
        if(_maxWaitingTime <= _minWaitingTime)
            _maxWaitingTime = _minWaitingTime + 1f;
    }

    private void OnEnable()
    {
        _waitingTime = Random.Range(_minWaitingTime, _maxWaitingTime);
        FindRandomWaypoint();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, _wayPoints[_randomWaypointIndex].position);

        if (distanceToWaypoint <= 0.2f)
        {
            if (_waitingTime <= 0f)
            {
                FindRandomWaypoint();
                _waitingTime = Random.Range(_minWaitingTime, _maxWaitingTime);
            }
            else
            {
                _waitingTime -= Time.deltaTime;
            }
        }
        else
        {
            Vector3 moveDirection = _wayPoints[_randomWaypointIndex].position - transform.position;
            transform.rotation = Quaternion.LookRotation(moveDirection.normalized);
            transform.Translate(moveDirection.normalized * _speed * Time.deltaTime, Space.World);
        }
    }

    private void FindRandomWaypoint()
    {
        _randomWaypointIndex = Random.Range(0, _wayPoints.Length);
    }
}
