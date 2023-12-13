using System;
using UnityEngine;

public class S_SculptRotate : MonoBehaviour
{
    public float rotateSensitivity = 5;

    [Range(0f, 0.99999f)]
    public float friction = 0.8f;

    public float smoothMoveTime = 1;

    private Vector3 rotSpeed = Vector3.zero;

    public bool isZoomed = false;

    private Vector3 zoomedPos, unzoomedPos;
    private Quaternion baseRot;

    [Range(0,1)]
    public float zoomRatio = 0.2f;

    private Vector3 v;
    private Quaternion q;


    private void Start()
    {
       
        unzoomedPos = transform.position;
        zoomedPos = Vector3.Lerp(unzoomedPos,Camera.main.transform.position,zoomRatio);
        baseRot = transform.rotation;
    }

    public void StatueZoomToggle()
    {
        isZoomed = !isZoomed;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).deltaPosition.magnitude < 0.5)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray.origin, ray.direction * 10, out hitInfo, Mathf.Infinity))
            {
               if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.GetComponent<S_SculptRotate>() != null)
                    {
                        isZoomed = !isZoomed;
                    }
                }
            }

        }*/
            if (isZoomed)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                    Vector3 oldRot = transform.rotation.eulerAngles;
                    transform.RotateAround(transform.position, Vector3.up, -t.deltaPosition.x * rotateSensitivity * Time.deltaTime);
                    transform.RotateAround(transform.position, Vector3.left, -t.deltaPosition.y * rotateSensitivity * Time.deltaTime);
                    Vector3 newRot = transform.rotation.eulerAngles;

                    rotSpeed = newRot - oldRot;
                    transform.rotation = Quaternion.Euler(oldRot);

                    //transform.Rotate(Rotation * Time.deltaTime);

                    //transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, 0));
                }
            }
            else
            {

                rotSpeed *= friction;
            }
            transform.Rotate(rotSpeed);
            transform.position = Vector3.SmoothDamp(transform.position, zoomedPos, ref v, smoothMoveTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, unzoomedPos, ref v, smoothMoveTime);
            transform.rotation = SmoothDamp(transform.rotation, baseRot, ref q, smoothMoveTime);
        }
    }

    //Smooth damp adapted for quaternion
    private Quaternion SmoothDamp(Quaternion rot, Quaternion target, ref Quaternion deriv, float time)
    {
        if (Time.deltaTime < Mathf.Epsilon) return rot;
        // account for double-cover
        var Dot = Quaternion.Dot(rot, target);
        var Multi = Dot > 0f ? 1f : -1f;
        target.x *= Multi;
        target.y *= Multi;
        target.z *= Multi;
        target.w *= Multi;
        // smooth damp (nlerp approx)
        var Result = new Vector4(
            Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
            Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
            Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
            Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
        ).normalized;

        // ensure deriv is tangent
        var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), Result);
        deriv.x -= derivError.x;
        deriv.y -= derivError.y;
        deriv.z -= derivError.z;
        deriv.w -= derivError.w;

        return new Quaternion(Result.x, Result.y, Result.z, Result.w);
    }

}
