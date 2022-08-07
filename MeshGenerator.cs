using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    float mth;
    float mmth;
    public GameObject grass;


    public int xSize = 40;
    public int zSize = 40;
    public float grasx = 0;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }





    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z =0; z<= zSize; z++)
        {
            for (int x = 0; x<=xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);

                if (y > mmth)
                {
                    mmth = y;
                }
                if (y < mth)
                {
                    mth = y;
                }
                

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];

        for (int I = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++ )
            {
                uvs[I] = new Vector2((float)x / xSize,(float)z/ zSize);
                I++;
            }
        }

        for (float I = 0f, z = 0f; z <= zSize; z+= 25.5f)
        {
            grasx += 25.5f;
            for(float x = 0f; x <= xSize; x+= 25.5f )
            {
                Instantiate(grass, new Vector3(x, 7, grasx), Quaternion.identity); 
                I+= 25.5f;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        
        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        
    }


}