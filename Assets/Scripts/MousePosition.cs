using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    private static MousePosition instance;
    [SerializeField] private LayerMask mousePlaneLayerMask;
    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }
    public static Vector3 getPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return  raycastHit.point;
    }
}
