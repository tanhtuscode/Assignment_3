using System;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    public float radius;
    public int density;

    private float curentradius;
    private int curentdensity;
    private Mesh mesh;
    private GameObject circle;
    
    private void Start()
    {
        curentradius = radius;
        curentdensity = density;
        
        Matrix4x4[] circlePath = MeshUtilities.MakeCirclePath(radius,density);
        Vector3[] circleProfile = MeshUtilities.MakeCircleProfile(radius, density);
         mesh = MeshUtilities.Sweep(circleProfile, circlePath, true);
        circle = new GameObject("cicle");
        circle.AddComponent<MeshFilter>().mesh = mesh;
        circle.AddComponent<MeshRenderer>().material =  new Material(Shader.Find("Standard"));
      
    }
    private void Update()
    {
        changeValue();
    }
    private void changeValue()
    {
        if (curentdensity != density || curentradius != radius)
        {
            curentradius = radius;
            curentdensity = density;
            
            Matrix4x4[] circlePath = MeshUtilities.MakeCirclePath(radius,density);
            Vector3[] circleProfile = MeshUtilities.MakeCircleProfile(radius, density);
            mesh.Clear();
            mesh = MeshUtilities.Sweep(circleProfile, circlePath, true);
            circle.GetComponent<MeshFilter>().mesh = mesh;
            mesh.RecalculateNormals();
        }
    }
}