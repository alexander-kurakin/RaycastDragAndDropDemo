using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float YOffset = 1f;
    private const float GizmosSphereRadius = 0.1f;
    private const int LeftMouseButtonCode = 0;
    private const int RightMouseButtonCode = 1;
    private const float CameraSwitchInterval = 5f;
    private const float ExplosionRadius = 5f;
    private const float ExplosionForce = 750f;

    [SerializeField] private GameObject _groundPlane;
    [SerializeField] private DragAndDropService _dragAndDropService;
    [SerializeField] private EffectsSpawner _effectsSpawner;

    private Shooter _shooter;
    private CameraSwitcher _cameraSwitcher;
    private IMovable _currentItem;
    private Vector3 _mouseOnThePlanePosition;
    private Vector3 _mouseRayOrigin;
    private Vector3 _mouseRayDirection;

    private float _cameraSwitchTimer;

    private void Awake()
    {
        _cameraSwitcher = GetComponent<CameraSwitcher>();
        _cameraSwitchTimer = CameraSwitchInterval;

        _shooter = new Shooter(new RigidBodyBasedExplosion(ExplosionRadius, ExplosionForce));
    }

    private void Update()
    {
        SwitchCameras();

        ShootMousePointRay();

        if (Input.GetMouseButtonDown(LeftMouseButtonCode))
            _currentItem = _dragAndDropService.TryPickUpItem(_mouseRayOrigin, _mouseRayDirection);

        if (_currentItem != null && Input.GetMouseButton(LeftMouseButtonCode))
            _dragAndDropService.TryDragItem(_mouseOnThePlanePosition);

        if (Input.GetMouseButtonUp(LeftMouseButtonCode))
        {
            _dragAndDropService.TryDropItem();
            _currentItem = null;
        }

        if (Input.GetMouseButtonDown(RightMouseButtonCode))
        {
            _effectsSpawner.SpawnExplosionEffect(_mouseOnThePlanePosition);
            _shooter.Shoot(_mouseOnThePlanePosition);
        }
    }

    private void SwitchCameras()
    {
        _cameraSwitchTimer -= Time.deltaTime;
        
        if (_cameraSwitchTimer <= 0f)
        {
            _cameraSwitcher.RandomSwitchCamera();
            _cameraSwitchTimer = CameraSwitchInterval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_mouseOnThePlanePosition, GizmosSphereRadius);
    }

    private void ShootMousePointRay()
    {
        Ray mousePointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        _mouseRayOrigin = mousePointRay.origin;
        _mouseRayDirection = mousePointRay.direction;

        Vector3 normal = _groundPlane.transform.up;
        Vector3 position = _groundPlane.transform.position + new Vector3(0, YOffset, 0);

        Plane groundPlane = new Plane(normal, position);

        float distance;

        if (groundPlane.Raycast(mousePointRay, out distance))
            _mouseOnThePlanePosition = mousePointRay.GetPoint(distance);
    }
}