using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirageXR;

public class UIOrigin : MonoBehaviour
{
    [SerializeField] private LayerMask floorLayer;

    [SerializeField] private Transform followTarget;

    bool isFollowing = true;
    float heightOffset = 1.75f;
    Vector3 currentPosition;

    public static UIOrigin Instance;

    public float CurrentFloorYPosition(){  return currentPosition.y; }


    void Awake()
    {
        Instance = this;

        // Attach UI origin objects as the ui origin object.
        //uiOriginObject = GameObject.Find("UiOriginObject");

    }

    // Use this for initialization
    void Start()
    {
        currentPosition = Vector3.zero;
        currentPosition.y = followTarget.position.y - heightOffset;
    }

    private void Update()
    {
        if (isFollowing)
        {
            currentPosition.x = followTarget.position.x;
            currentPosition.z = followTarget.position.z;

            Ray ray = new Ray(followTarget.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 2f, floorLayer))
            {
                currentPosition.y = hit.point.y;
            }

            transform.position = currentPosition;
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(followTarget.forward, Vector3.up).normalized, Vector3.up);
        }
    }
}
