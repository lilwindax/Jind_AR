using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    public class InteractibleManager : MonoBehaviour
    {
        public static InteractibleManager Instance { get; private set; }

        private List<GameObject> focusedObjects = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddFocusedObject(GameObject obj)
        {
            if (!focusedObjects.Contains(obj))
            {
                focusedObjects.Add(obj);
            }
        }

        public void RemoveFocusedObject(GameObject obj)
        {
            if (focusedObjects.Contains(obj))
            {
                focusedObjects.Remove(obj);
            }
        }

        public void ClearFocusedObjects()
        {
            focusedObjects.Clear();
        }

        public GameObject GetFocusedObject()
        {
            if (focusedObjects.Count > 0)
            {
                return focusedObjects[focusedObjects.Count - 1];
            }

            return null;
        }
    }
}