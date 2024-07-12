using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;

    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _spawnerWeapon;

    private NavMeshAgent _agent;
    private Vector3 _targetPosition;
    private LevelController _levelController;

    private int _currentPlatformIndex = 0;
    private Platform _currentPlatform;
    private List<Platform> _platformList = new List<Platform>();

    private GameController _gameController;
    private Weapon _currentWeapon;
    private Transform _transform;
    private Camera _camera;

    private State _currentState;

    private enum State
    {
        IDLE,
        RUN
    }

    [Inject]
    private void Construct(GameController gameController)
    {
        _gameController = gameController;   
        _transform = transform;
        _camera = Camera.main;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _playerConfig.SpeedMove;
        _agent.enabled = false;

        _currentWeapon = Instantiate(_playerConfig.CurrentWeapon, _spawnerWeapon.position, _spawnerWeapon.rotation, _spawnerWeapon);
        _currentWeapon.Init();

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

        this.UpdateAsObservable()
            .Where(_ => _currentPlatform != null && _gameController.IsGame)
            .Subscribe(_ => LookAtCenterPlatform(_currentPlatform))
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

        SetAnimation(State.IDLE);
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

            SetAnimation(State.RUN);
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
            _currentWeapon.Shot(hit.point);
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

    private void LookAtCenterPlatform(Platform platform)
    {
        Vector3 platrofmCenter = platform.transform.position;
        platrofmCenter.y = platform.transform.localScale.y / 2;

        Vector3 dirction = (platrofmCenter - _transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dirction);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * _playerConfig.SpeedRotate);
    }

    private void SetAnimation(State state)
    {
        if (_currentState == state)
            return;

        _currentState = state;

        switch (_currentState)
        {
            case State.RUN:
                _animator.SetTrigger(Constants.ANIM_RUN);
                break;

            default:
                _animator.SetTrigger(Constants.ANIM_IDLE);
                break;
        }
    }
}
