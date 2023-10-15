using UnityEngine;
using System.IO;

public class PawnMaker : MonoBehaviour
{
    public string vertexDataPath = "Assets/vertexData.txt";

    void Start()
    {
        // Read vertexData.txt file
        string[] lines = File.ReadAllLines(vertexDataPath);

        // Initialize array to store vertices
        Vector3[] vertices = new Vector3[lines.Length];

        // Loop through each line and parse vertex values
        for (int i = 0; i < lines.Length; i++)
        {
            string[] vertexValues = lines[i].Split(',');
            float x = float.Parse(vertexValues[0]);
            float y = float.Parse(vertexValues[1]);
            float z = float.Parse(vertexValues[2]);

            vertices[i] = new Vector3(x, y, z);
        }

        // Build profile from vertices
        Vector3[] profile = vertices;

        // Build path from profile
        Matrix4x4[] path = new Matrix4x4[1];
        path[0] = Matrix4x4.identity;

        // Generate mesh using Sweep method
        Mesh mesh = MeshUtilities.Sweep(profile, path, true);

        // Create and configure MeshRenderer component
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        // Apply mesh to MeshFilter component
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh = mesh;
        }
        else
        {
            Debug.LogError("MeshFilter component not found!");
        }
    }
}