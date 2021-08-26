using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class GizmosController : MonoBehaviour
{
    [SerializeField] private Camera screenshotCamera;

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = screenshotCamera.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - screenshotCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = screenshotCamera.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else
        {
            var rotationSpeed = 2;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
                transform.Rotate(Vector3.down, XaxisRotation);
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
                transform.Rotate(Vector3.right, YaxisRotation);
            }
        }


    }

}