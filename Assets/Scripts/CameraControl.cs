using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float smoothing = 0.15f;
    public float minX = -20;
    public float minY = -20;
    public float maxX = 20;
    public float maxY = 20;
    public Color cameraBoundsColor = Color.cyan;
    public Color cameraMoveBoundsColor = Color.red;

    private Vector3 targetPosition2D;

    void OnEnable()
    {
        // Initial no-lerp transition to centre on player, avoid jank
        targetPosition2D = new Vector3(target.position.x, target.position.y, transform.position.z);
        ClampCameraToLevelBounds(false);
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            targetPosition2D = new Vector3(target.position.x, target.position.y, transform.position.z);
            ClampCameraToLevelBounds(true);
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition2D, smoothing * Time.deltaTime);
    }

    private void ClampCameraToLevelBounds(bool smoothPan)
    {
        float camVertExtent = Camera.main.orthographicSize;
        float camHorzExtent = Camera.main.aspect * camVertExtent;

        float leftBound = minX + camHorzExtent;
        float rightBound = maxX - camHorzExtent;
        float bottomBound = minY + camVertExtent;
        float topBound = maxY - camVertExtent;

        Debug.DrawLine(new Vector3(leftBound, bottomBound), new Vector3(rightBound, bottomBound), cameraMoveBoundsColor);
        Debug.DrawLine(new Vector3(rightBound, bottomBound), new Vector3(rightBound, topBound), cameraMoveBoundsColor);
        Debug.DrawLine(new Vector3(rightBound, topBound), new Vector3(leftBound, topBound), cameraMoveBoundsColor);
        Debug.DrawLine(new Vector3(leftBound, topBound), new Vector3(leftBound, bottomBound), cameraMoveBoundsColor);

        targetPosition2D.x = Mathf.Clamp(targetPosition2D.x, leftBound, rightBound);
        targetPosition2D.y = Mathf.Clamp(targetPosition2D.y, bottomBound, topBound);

        if (smoothPan)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition2D, smoothing);
        }
        else
        {
            transform.position = targetPosition2D;
        }

    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(new Vector3(minX, minY), new Vector3(maxX, minY), cameraBoundsColor);
        Debug.DrawLine(new Vector3(maxX, minY), new Vector3(maxX, maxY), cameraBoundsColor);
        Debug.DrawLine(new Vector3(maxX, maxY), new Vector3(minX, maxY), cameraBoundsColor);
        Debug.DrawLine(new Vector3(minX, maxY), new Vector3(minX, minY), cameraBoundsColor);
    }
}
