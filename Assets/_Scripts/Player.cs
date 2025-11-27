using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _groundPlane;

    private ItemPicker _itemPicker;
    private ItemMover _itemMover;

    private IGrabbable _currentItem;
    private Vector3 _mouseOnThePlanePosition;

    private void Awake()
    {
        _itemPicker = new ItemPicker();
    }

    private void Update()
    {
        Ray mousePointRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 normal = _groundPlane.transform.up;
        Vector3 position = _groundPlane.transform.position;

        Plane groundPlane = new Plane(normal, position);

        float distance;

        if (groundPlane.Raycast(mousePointRay, out distance))
            _mouseOnThePlanePosition = mousePointRay.GetPoint(distance);

        if (Input.GetMouseButtonDown(0))
            _itemPicker.PickupItem(mousePointRay.origin, mousePointRay.direction, out _currentItem);

        if (Input.GetMouseButtonUp(0))
        {
            _itemPicker.DropItem();
            _currentItem = null;
        }
    }

    private void FixedUpdate()
    {
        if (_currentItem != null)
        {
            _itemMover = new ItemMover(_currentItem, _mouseOnThePlanePosition);
            _itemMover.Move();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_mouseOnThePlanePosition, 0.1f);
    }
}