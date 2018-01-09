using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

// Tagging component for use with the LocalNavMeshBuilder
// Supports mesh-filter and terrain - can be extended to physics and/or primitives
[DefaultExecutionOrder(-200)]
public class NavMeshSourceTag : MonoBehaviour
{
    // Global containers for all active mesh/terrain tags
    public static List<MeshFilter> m_Meshes = new List<MeshFilter>();
    public static List<Terrain> m_Terrains = new List<Terrain>();

    void OnEnable()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        if (meshFilters.Length > 0)
        {
            for (int i = 0; i < meshFilters.Length; i++)
            {
                m_Meshes.Add(meshFilters[i]);
            }
        }

        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            m_Terrains.Add(terrain);
        }
    }

    void OnDisable()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            m_Meshes.Remove(meshFilter);
        }

        Terrain terrain = GetComponent<Terrain>();
        if (terrain != null)
        {
            m_Terrains.Remove(terrain);
        }
    }

    // Collect all the navmesh build sources for enabled objects tagged by this component
    public static void Collect(ref List<NavMeshBuildSource> sources)
    {
        sources.Clear();

        for (var i = 0; i < m_Meshes.Count; ++i)
        {
            MeshFilter meshFilter = m_Meshes[i];
            if (meshFilter == null) { continue; }

            Mesh mesh = meshFilter.sharedMesh;
            if (mesh == null) { continue; }

            NavMeshBuildSource navMeshBuildSource = new NavMeshBuildSource();
            navMeshBuildSource.shape = NavMeshBuildSourceShape.Mesh;
            navMeshBuildSource.sourceObject = mesh;
            navMeshBuildSource.transform = meshFilter.transform.localToWorldMatrix;
            navMeshBuildSource.area = 0;
            sources.Add(navMeshBuildSource);
        }

        for (var i = 0; i < m_Terrains.Count; ++i)
        {
            Terrain terrain = m_Terrains[i];
            if (terrain == null) continue;

            NavMeshBuildSource navMeshBuildSource = new NavMeshBuildSource();
            navMeshBuildSource.shape = NavMeshBuildSourceShape.Terrain;
            navMeshBuildSource.sourceObject = terrain.terrainData;
            // Terrain system only supports translation - so we pass translation only to back-end
            navMeshBuildSource.transform = Matrix4x4.TRS(terrain.transform.position, Quaternion.identity, Vector3.one);
            navMeshBuildSource.area = 0;
            sources.Add(navMeshBuildSource);
        }
    }
}
