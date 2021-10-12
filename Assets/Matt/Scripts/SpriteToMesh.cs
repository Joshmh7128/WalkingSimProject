using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteToMesh : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Sprite toConvert;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toConvert != null)
        {
            Mesh mesh = new Mesh();
            mesh.vertices = Array.ConvertAll(toConvert.vertices, i => (Vector3)i);
            mesh.uv = toConvert.uv;
            mesh.triangles = Array.ConvertAll(toConvert.triangles, i => (int)i);
            meshFilter.mesh = mesh;

            toConvert = null;
        }
    }
}
