using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManipulator : MonoBehaviour
{
    private GestureRecognizer gestureRecognizer;

    void Start()
    {
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

        gestureRecognizer.Tapped += OnTapped;

        gestureRecognizer.StartCapturingGestures();
    }

    void OnDestroy()
    {
        gestureRecognizer.StopCapturingGestures();
        gestureRecognizer.Tapped -= OnTapped;
    }

    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
        {
            if (hitInfo.collider.gameObject == gameObject)
            {
                // The user is looking at this object
                // You can add visual feedback here
            }
        }
    }

    private void OnTapped(TappedEventArgs args)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
        {
            if (hitInfo.collider.gameObject == gameObject)
            {
                // The user tapped on this object
                // You can add code here to move the object
                transform.position += Vector3.up * 0.1f;
            }
        }
    }
}
