using UnityEngine;

public class ItemMover
{
    private IGrabbable _itemToMove;
    private Vector3 _movePoint;

    public ItemMover(IGrabbable itemToMove, Vector3 movePoint)
    {
        _itemToMove = itemToMove;
        _movePoint = movePoint;
    }

    public void Move()
    {
        _itemToMove.Rigidbody().MovePosition(_movePoint);
    }
}
