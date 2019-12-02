using UnityEngine;

public class SurfaceGenerator : MonoBehaviour
{
    public bool generateWaves = false;
    public bool generateCollider = false;
    [Range(0.1f,50.0f)]
    public float yScaling = 5.0f;
    [Range(0.1f,20.0f)]
    public float detailScaling = 1.0f;
    [HideInInspector]
    public Vector3[] vertices;

    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    void Update()
    {
        GenerateSurface();
    }

    void GenerateSurface()
    {
        vertices = mesh.vertices;
        int counter = 0;
        int yLevel = 0;

        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                MeshCalculate(counter, yLevel);
                counter++;
            }
            yLevel++;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        if (generateCollider)
        {
            Destroy(GetComponent<MeshCollider>());
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = null;
            collider.sharedMesh = mesh;
        }
    }

    void MeshCalculate(int vertexIndex, int yOffset)
    {
        if (generateWaves)
        {            
            vertices[vertexIndex].z = Mathf.PerlinNoise
                (Time.time + (vertices[vertexIndex].x + transform.position.x) / detailScaling,
                    Time.time + (vertices[vertexIndex].y + transform.position.y)) * yScaling;
            vertices[vertexIndex].z -= yOffset;
        }
        else
        {
            vertices[vertexIndex].z = Mathf.PerlinNoise
                ((vertices[vertexIndex].x + transform.position.x) / detailScaling,
                (vertices[vertexIndex].y + transform.position.y)) * yScaling;
            vertices[vertexIndex].z -= yOffset;
        }
    }
}