using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;
    bool isDragging = false;
    Vector3 manipulationOffset;

    void Awake()
    {
        Instance = this;
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.ManipulationTranslate);
        recognizer.Tapped += (args) =>
        {
            if (FocusedObject != null)
            {
                // Move the object to the gaze position.
                Vector3 gazePosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                FocusedObject.transform.position = gazePosition;
            }
        };
        recognizer.ManipulationStarted += (args) =>
        {
            if (FocusedObject != null)
            {
                // Start dragging the object.
                isDragging = true;

                // Calculate the offset between the object's position and the gaze position.
                Vector3 gazePosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                Vector3 objectPosition = FocusedObject.transform.position;
                manipulationOffset = objectPosition - gazePosition;
            }
        };

        recognizer.ManipulationUpdated += (args) =>
        {
            if (FocusedObject != null)
            {
                // Move the object with the gesture delta.
                Vector3 gazePosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                FocusedObject.transform.position = gazePosition + manipulationOffset + args.cumulativeDelta;
            }
        };

        recognizer.ManipulationCompleted += (args) =>
        {
            // Stop dragging the object.
            isDragging = false;
        };
        recognizer.ManipulationCanceled += (args) =>
        {
            // Stop dragging the object.
            isDragging = false;
        };
        recognizer.StartCapturingGestures();
    }


    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }

        // Move the object to the gaze position if it is not being dragged.
        if (!isDragging && FocusedObject != null)
        {
            // Only move the object when the user taps the air
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the cursor is pointing at the object
                bool hit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit newHitInfo);
                if (hit && hitInfo.collider.gameObject == FocusedObject)
                {
                    // Move the object to the cursor position
                    Vector3 gazePosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;
                    FocusedObject.transform.position = gazePosition;
                }
            }
        }

    }

}
