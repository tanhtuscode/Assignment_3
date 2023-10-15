using System.Collections;
using System.IO;
using UnityEngine;

public class VertexReader : MonoBehaviour
{
    public GameObject model; // Path to the 3D model file

    void Start()
    {
      
        // Load the model file

        // Extract the vertex locations
        Vector3[] vertices = ExtractVertices(model);

        // Save the vertex locations
        SaveVertices(vertices);

        // Convert vertex data to code
        string code = ConvertToCode(vertices);

        // Print the generated code
        Debug.Log(code);
    }



    Vector3[] ExtractVertices(GameObject model)
    {
        // Get the MeshFilter component of the model
        MeshFilter meshFilter = model.GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("Model does not have a MeshFilter component.");
            return null;
        }

        // Get the vertices of the mesh
        return meshFilter.mesh.vertices;
    }

    void SaveVertices(Vector3[] vertices)
    {
        // Create a new file to save the vertex data
        string savePath = Application.dataPath + "/vertexData.txt";

        using (StreamWriter writer = new StreamWriter(savePath))
        {
            // Write each vertex location to the file
            foreach (Vector3 vertex in vertices)
            {
                writer.WriteLine(vertex.x + "," + vertex.y + "," + vertex.z);
            }
        }

        Debug.Log("Vertex data saved to: " + savePath);
    }

    string ConvertToCode(Vector3[] vertices)
    {
        string code = "Vector3[] vertices = new Vector3[] {\n";

        // Generate code for each vertex location
        foreach (Vector3 vertex in vertices)
        {
            string vertexCode = "    new Vector3(" + vertex.x + "f, " + vertex.y + "f, " + vertex.z + "f),";
            code += vertexCode + "\n";
        }

        code += "};";

        return code;
    }
}