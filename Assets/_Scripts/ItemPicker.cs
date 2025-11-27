using UnityEngine;

public class ItemPicker
{
    private IGrabbable _grabbedItem;

    public void PickupItem(Vector3 origin, Vector3 direction, out IGrabbable grabbedItem)
    {
        grabbedItem = null;

        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _grabbedItem = hit.collider.GetComponent<IGrabbable>();

            if (_grabbedItem != null)
            {
                _grabbedItem.Grab();
                grabbedItem = _grabbedItem;
            }
        }
    }

    public void DropItem()
    {
        if (_grabbedItem != null)
        {
            _grabbedItem.Drop();
            _grabbedItem = null;
        }
    }   
}
