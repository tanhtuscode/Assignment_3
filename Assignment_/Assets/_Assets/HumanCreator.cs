using System.Collections;
using UnityEngine;

public class HumanCreator : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        // Create the mesh
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Define the vertices
        Vector3[] vertices = new Vector3[]
        {
            // Head
            new Vector3(0f, 0.5f, 0f),
            // Body
            new Vector3(0f, 0f, 0f),
            // Left Arm
            new Vector3(-0.3f, 0f, 0f),
            // Right Arm
            new Vector3(0.3f, 0f, 0f),
            // Left Leg
            new Vector3(-0.15f, -0.5f, 0f),
            // Right Leg
            new Vector3(0.15f, -0.5f, 0f)
        };
        mesh.vertices = vertices;

        // Define the triangles
        int[] triangles = new int[]
        {
            // Head
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1,
            // Body
            1, 2, 4,
            2, 3, 4,
            // Left Leg
            1, 4, 5,
            // Right Leg
            1, 3, 5,
            // Left Arm
            1, 2, 5,
            // Right Arm
            1, 3, 5
        };
        mesh.triangles = triangles;

        // Define the vertex colors
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Random.ColorHSV();
        }
        mesh.colors = colors;

        // Set up materials and shaders
        Material material = new Material(Shader.Find("Standard"));
        meshRenderer.material = material;
    }
}