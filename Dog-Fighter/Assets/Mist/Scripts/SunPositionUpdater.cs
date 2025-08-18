using UnityEngine;

public class SunManager : MonoBehaviour
{
    public Transform sunTransform;            
    public Material[] toonMaterials;         

    void Update()
    {
        Vector3 sunPos = sunTransform.position;

        foreach (Material mat in toonMaterials)
        {
            mat.SetVector("_SunPosition", sunPos);
        }
    }
}
