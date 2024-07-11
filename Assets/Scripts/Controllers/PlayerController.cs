using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private LevelController _levelController;

    private int _currentPlatformIndex = 0;
    private Platform _currentPlatform;
    private List<Platform> _platformList = new List<Platform>();

    private GameController _gameController;
    private Camera _camera;

    [Inject]
    private void Construct(GameController gameController)
    {
        _gameController = gameController;
        _camera = Camera.main;

        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;

        _weapon.Init();
        SetupSubscriptions();
    }

    private void SetupSubscriptions()
    {
        _gameController.ObserveEveryValueChanged(g => g.IsGame)
            .Where(isGame => isGame)
            .Subscribe(_ => Move())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => _gameController.IsGame && _agent.isStopped && Input.GetMouseButtonDown(0))
            .Subscribe(_ => Attack())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => !_agent.pathPending && _agent.remainingDistance <= 0.1f && !_agent.isStopped)
            .Subscribe(_ => Stop())
            .AddTo(this);
    }

    public void GetLevel(LevelController levelController)
    {
        _levelController = levelController;
        SetPosition(_levelController);
    }

    public void SetPosition(LevelController levelController)
    {
        _currentPlatformIndex = 0;
        _platformList = levelController.Waypoints.PlatformList;
        _currentPlatform = _platformList[0];
        _targetPosition = GetTargetPosition(_currentPlatform);

        Transform trFirstPlatform = _platformList[0].transform;   
        transform.position = trFirstPlatform.position + Vector3.up * trFirstPlatform.localScale.y * 0.5f;
        transform.rotation = Quaternion.identity;

        _agent.enabled = true;
        _agent.Warp(transform.position);
    }

    private void Stop()
    {
        _agent.isStopped = true;
        _currentPlatform.OnAllEnemiesDied += Move;
    }

    private void Move()
    {
        _currentPlatform.OnAllEnemiesDied -= Move;
        _currentPlatformIndex++;

        if (_currentPlatformIndex < _platformList.Count)
        {
            _currentPlatform = _platformList[_currentPlatformIndex];
            _targetPosition = GetTargetPosition(_currentPlatform);
            _agent.SetDestination(_targetPosition);
            _agent.isStopped = false;
        }
        else
        {
            _gameController.Win();
        }
    }

    private void Attack()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _weapon.Shot(hit.point);
        }
    }

    private Vector3 GetTargetPosition(Platform platform)
    {
        Vector3 platformCenter = platform.transform.position;
        float platformRadius = platform.transform.localScale.z * 0.5f;
        Vector3 dirction = (platformCenter - transform.position).normalized;
        Vector3 targetPosition = platformCenter - dirction * platformRadius;
        targetPosition.y = platform.transform.position.y + platform.transform.localScale.y * 0.5f;

        return targetPosition;
    }
}
