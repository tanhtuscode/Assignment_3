using System.Collections;
using System.IO;
using UnityEngine;

public class MeshReader : MonoBehaviour
{
    public MeshFilter meshFilter; // Reference to the MeshFilter component of the user-provided mesh

    void Start()
    {
        // Extract the vertex locations
        Vector3[] vertices = ExtractVertices();

        // Save the vertex data and profile
        SaveVertices(vertices);
    }

    Vector3[] ExtractVertices()
    {
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("MeshFilter or sharedMesh is missing. Please assign a valid MeshFilter component.");
            return null;
        }

        // Get the vertices of the mesh
        return meshFilter.sharedMesh.vertices;
    }

    void SaveVertices(Vector3[] vertices)
    {
        // Create a new file to save the vertex data
        string vertexDataPath = Application.dataPath + "/vertexData.txt";

        using (StreamWriter writer = new StreamWriter(vertexDataPath))
        {
            // Write each vertex location to the file
            foreach (Vector3 vertex in vertices)
            {
                writer.WriteLine(vertex.x + "," + vertex.y + "," + vertex.z);
            }
        }

        Debug.Log("Vertex data saved to: " + vertexDataPath);

        // Create a new file to save the vertex profile
        string vertexProfilePath = Application.dataPath + "/VertexProfile.txt";

        using (StreamWriter writer = new StreamWriter(vertexProfilePath))
        {
            // Write each vertex profile to the file
            foreach (Vector3 vertex in vertices)
            {
                string vertexProfile = GetVertexProfile(vertex); // Replace this with your own method to get the vertex profile
                writer.WriteLine(vertexProfile);
            }
        }

        Debug.Log("Vertex profile saved to: " + vertexProfilePath);
    }

    string GetVertexProfile(Vector3 vertex)
    {
        // Replace this method with your own logic to generate the vertex profile
        // You can use the vertex position or any other relevant information to create the profile
        string profile = "Vertex Profile for (" + vertex.x + "," + vertex.y + "," + vertex.z + ")";
        return profile;
    }
}