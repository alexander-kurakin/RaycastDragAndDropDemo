using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTester : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _unityPlane;

    private Vector3 _gizmosPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_gizmosPosition,0.1f);
    }

    private void Update()
    {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 normal = _unityPlane.transform.up;
        Vector3 position = _unityPlane.transform.position;

            Plane groundPlane = new Plane(normal, position);

            float distance;
            if (groundPlane.Raycast(mouseRay, out distance))
            { 
                Vector3 worldPositionOnPlane = mouseRay.GetPoint(distance);
                _gizmosPosition = worldPositionOnPlane;
            }

        if (Input.GetMouseButton(0))
        {
            _cube.GetComponent<Rigidbody>().MovePosition(_gizmosPosition);
        }
    }
}
