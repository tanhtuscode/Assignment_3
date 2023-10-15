using System.Collections;
using UnityEngine;

public class MeshUtilities
	{
		public static Mesh Cube(float size)
		{
			Mesh mesh = new Mesh();

			Vector3[] vertices = new Vector3[4 * 6]
			{
				//front
				new Vector3(-size, -size, -size),
				new Vector3(size, -size, -size),
				new Vector3(size, size, -size),
				new Vector3(-size, size, -size),

				// back
				new Vector3(-size, -size, size),
				new Vector3(size, -size, size),
				new Vector3(size, size, size),
				new Vector3(-size, size, size),

				// left
				new Vector3(-size, -size, -size),
				new Vector3(-size, size, -size),
				new Vector3(-size, size, size),
				new Vector3(-size, -size, size),

				// right
				new Vector3(size, -size, -size),
				new Vector3(size, size, -size),
				new Vector3(size, size, size),
				new Vector3(size, -size, size),

				// bottom
				new Vector3(-size, -size, -size),
				new Vector3(-size, -size, size),
				new Vector3(size, -size, size),
				new Vector3(size, -size, -size),

				// top
				new Vector3(-size, size, -size),
				new Vector3(-size, size, size),
				new Vector3(size, size, size),
				new Vector3(size, size, -size)
			};
			mesh.vertices = vertices;

			int[] tris = new int[6 * 2 * 3]
			{
				//front
				3, 2, 1,
				3, 1, 0,

				// back
				4, 5, 6,
				4, 6, 7,

				// left
				11, 10, 9,
				11, 9, 8,

				// right
				12, 13, 14,
				12, 14, 15,

				// bottom
				19, 18, 17,
				19, 17, 16,

				// top
				20, 21, 22,
				20, 22, 23
			};
			mesh.triangles = tris;
			mesh.RecalculateNormals();
			return mesh;
		}

		public static Mesh Cylinder(int d, float r, float h)
		{
			Mesh mesh = new Mesh();

			Vector3[] vertices = new Vector3[2 * d]; // need top and bottom vertices
			float dTheta = Mathf.PI * 2.0f / d;
			for (int i = 0; i < d; i++)
			{
				float theta = i * dTheta;
				float x = r * Mathf.Cos(theta);
				float z = r * Mathf.Sin(theta);
				// top vertex
				vertices[i] = new Vector4(x, h, z);
				// bottom vertex
				vertices[i + d] = new Vector4(x, -h, z);
			}

			mesh.vertices = vertices;

			int[] tris = new int[d * 6]; // two tris for each side
			for (int i = 0; i < d; i++)
			{
				tris[i * 6] = i; // current top vertex
				tris[i * 6 + 1] = (i + 1) % d; // next top vertex (wrapping)
				tris[i * 6 + 2] = d + (i + 1) % d; // next bottom vertex (wrapping)

				tris[i * 6 + 3] = i; // current top vertex
				tris[i * 6 + 4] = d + (i + 1) % d; // next bottom vertex (wrapping)
				tris[i * 6 + 5] = d + i; // current bottom vertex
			}

			mesh.triangles = tris;

			mesh.RecalculateNormals();

			return mesh;
		}
		
		
		public static Mesh Sweep(Vector3[] profile, Matrix4x4[] path, bool closed)
		{
			Mesh mesh = new Mesh();

			int numVerts = path.Length * profile.Length;
			int numTris;
			if (closed)
				numTris = 2 * path.Length * profile.Length;
			else
				numTris = 2 * (path.Length - 1) * profile.Length;

			Vector3[] vertices = new Vector3[numVerts];
			int[] tris = new int[numTris * 3];

			for (int i = 0; i < path.Length; i++)
			{
				for (int j = 0; j < profile.Length; j++)
				{
					Vector3 v = path[i].MultiplyPoint(profile[j]);
					vertices[i * profile.Length + j] = v;

					if (closed || i < path.Length - 1)
					{
						tris[6 * (i * profile.Length + j)] = (j + i * profile.Length);
						tris[6 * (i * profile.Length + j) + 1] = ((j + 1) % profile.Length + i * profile.Length);
						tris[6 * (i * profile.Length + j) + 2] = ((j + 1) % profile.Length + ((i + 1) % path.Length) * profile.Length);
						tris[6 * (i * profile.Length + j) + 3] = (j + i * profile.Length);
						tris[6 * (i * profile.Length + j) + 4] = ((j + 1) % profile.Length + ((i + 1) % path.Length) * profile.Length);
						tris[6 * (i * profile.Length + j) + 5] = (j + ((i + 1) % path.Length) * profile.Length);
					}
				}
			}
			
			mesh.vertices = vertices;

			mesh.triangles = tris;

			mesh.RecalculateNormals();

			return mesh;
		}

		public static Matrix4x4[] MakeCirclePath(float radius, int density)
		{
			Matrix4x4[] path = new Matrix4x4[density];
			for (int i = 0; i < density; i++)
			{
				float angle = (360.0f * i) / density;
				path[i] = Matrix4x4.Rotate(Quaternion.Euler(0, -angle, 0)) * Matrix4x4.Translate(new Vector3(radius, 0, 0));
			}
			return path;
		}

		public static Vector3[] MakeCircleProfile(float radius, int density)
		{
			Vector3[] profile = new Vector3[density];
			for (int i = 0; i < density; i++)
			{
				float angle = (2.0f * Mathf.PI * i) / density;
				profile[i] = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
			}
			return profile;
		}

	}
