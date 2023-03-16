using UnityEngine;

public class GazeReceiver : MonoBehaviour
{
    private Material defaultMaterial;
    private Material selectedMaterial;

    void Start()
    {
        // Get the default material of the object
        defaultMaterial = GetComponent<Renderer>().material;

        // Create a new material for when the object is selected
        selectedMaterial = new Material(defaultMaterial);
        selectedMaterial.color = Color.red;
    }

    void OnSelect()
    {
        // Change the object's material to the selected material
        GetComponent<Renderer>().material = selectedMaterial;
    }

    void OnReset()
    {
        // Change the object's material back to the default material
        GetComponent<Renderer>().material = defaultMaterial;
    }
}
