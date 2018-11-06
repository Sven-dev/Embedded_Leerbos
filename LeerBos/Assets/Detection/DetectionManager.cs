using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Translates the Kinect-data to Unity data, and interacts with any objects thrown or clicked
public class DetectionManager : MonoBehaviour
{
    public LayerMask layer;
    public Camera cam;
    public CollisionType collisionType;
    [Tooltip("Enable to detect mouse-clicks")]
    public bool DeveloperMode;

    private void Start()
    {
        SceneManager.sceneLoaded += AttachCamera;
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void AttachCamera(Scene scene, LoadSceneMode mode)
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Blob b in BlobTracking.Blobs)
        {
            if (OkayCheck(b))
            {
                GetCollisionType(TranslateToWorld(b));
            }
        }

        if (DeveloperMode && Input.GetMouseButtonDown(0))
        {
            print("Mouse click");
            GetCollisionType(GetMousePosition());
        }
    }

    //Checks if the detection needs to detect 2D- or 3D-colliders, or both
    void GetCollisionType(Vector3 position)
    {
        print("CollisionType");
        switch (collisionType)
        {
            case CollisionType._2D:
                //2D
                Click2D(Raycast2D(layer, position));
                break;
            case CollisionType._3D:
                //3D
                Click3D(Raycast3D(layer, position));
                break;
            case CollisionType.Both:
                //Both
                Click2D(Raycast2D(layer, position));
                Click3D(Raycast3D(layer, position));
                break;
        }
    }

    #region Data Conversion
    //Checks if the blob falls within the boundaries of the screen
    private bool OkayCheck(Blob _blob)
    {
        return (_blob.XPosition >= 0 &&
            _blob.XPosition <= 1 &&
            _blob.YPosition >= 0 &&
            _blob.YPosition <= 1 &&
            _blob.Height > 0 &&
            _blob.Width > 0);
    }

    //Translates the kinect data to Unity coordinates.
    private Vector3 TranslateToWorld(Blob b)
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        return new Vector3(((cam.transform.position.x) + (b.XPosition * width) - (width / 2)),
            ((cam.transform.position.y) + (b.YPosition * height) - (height / 2)), -3f);
    }

    //Translates the mouseclick to Unity coordinates.
    private Vector3 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion

    #region 2D
    //Raycasts at the given position, returns the hit object.
    private RaycastHit2D Raycast2D(LayerMask layerMask, Vector2 position)
    {
        print("Raycast2D");
        Ray ray = new Ray(position, Vector3.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        RaycastHit2D hit = Physics2D.Raycast(position, Vector3.forward, Mathf.Infinity, layerMask);

        return hit;
    }

    //Clicks at the given position, checks for an interactable object, and interacts with it.
    private void Click2D(RaycastHit2D hit)
    {
        print("Click2D");
        if (hit.transform != null)
        {
            print("Object hit");
            Interactable obj = hit.transform.GetComponent<Interactable>();
            if (obj != null)
            {
                print("Interacting");
                obj.Interact(hit.point);
            }
        }
    }
    #endregion

    #region 3D
    //Raycasts at the given position, returns the hit object.
    private RaycastHit Raycast3D(LayerMask layerMask, Vector3 position)
    {
        RaycastHit hit;
        Ray ray = new Ray(position, Vector3.forward);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        return hit;
    }

    //Clicks at the given position, checks for an interactable object, and interacts with it.
    private void Click3D(RaycastHit hit)
    {
        if (hit.transform != null)
        {
            Interactable obj = hit.transform.GetComponent<Interactable>();
            if (obj != null)
            {
                obj.Interact(hit.point);
            }
        }
    }
    #endregion
}

public enum CollisionType
{
    _2D,
    _3D,
    Both
}