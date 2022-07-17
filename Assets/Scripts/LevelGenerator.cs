using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }

    public GameObject m_destination;
    
    public GameObject[] m_islandPrefabs;

    public GameplayEvent m_gameplayEventGreen;
    public GameplayEvent m_gameplayEventYellow;
    public GameplayEvent m_gameplayEventRed;

    [System.NonSerialized]
    public List<Island> m_islands = new ();

    [System.NonSerialized]
    public bool m_isReady = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_islands.Clear();

        // Spawn islands
        float[] islandWeights = new float[m_islandPrefabs.Length];
        for (int i = 0; i < m_islandPrefabs.Length; i++)
        {
            islandWeights[i] = (1 / (float)m_islandPrefabs.Length) * (i + 1);
        }
        StartCoroutine(SpawnCoroutine(4, 20, 120, "OceanWall", m_islandPrefabs, islandWeights,
            (island) => {
                island.GetComponentInChildren<TerrainCollider>().enabled = false;
                m_islands.Add(island.GetComponent<Island>());
            },
            () => {
                // Pick target
                int targetIndex = Random.Range(0, m_islands.Count);
                m_islands[targetIndex].m_isTarget = true;
                Instantiate(m_destination, m_islands[targetIndex].transform);

                // Pick start
                int startIndex = targetIndex;
                float maxDistSqr = 0.0f;
                for (int i = 0; i < m_islands.Count; ++i)
                {
                    float distSqr = (m_islands[i].transform.position - m_islands[targetIndex].transform.position).sqrMagnitude;

                    if (distSqr > maxDistSqr)
                    {
                        startIndex = i;
                        maxDistSqr = distSqr;
                    }
                }
                m_islands[startIndex].m_isStart = true;

                // Move player
                Vector2 dir2 = Random.insideUnitCircle;
                Vector3 dir = new Vector3(dir2.x, 0.0f, dir2.y);
                dir = (m_islands[targetIndex].transform.position - m_islands[startIndex].transform.position).normalized;
                Vector3 pos = m_islands[startIndex].transform.position + dir * 20.0f;
                PlayerController.Instance.transform.position = pos;
                PlayerController.Instance.transform.LookAt(pos + dir * 20.0f);

                // Spawn events
                GameObject[] events = { m_gameplayEventRed.gameObject, m_gameplayEventGreen.gameObject, m_gameplayEventYellow.gameObject };
                float[] weights = { 0.1f, 0.4f, 1.0f };
                StartCoroutine(SpawnCoroutine(10, 10, 35, "", events, weights, (_) => { }, () => { m_isReady = true; }));
            }
        ));
    }

    IEnumerator SpawnCoroutine(int density, int retries, int overlapSphere, string blacklistedTag, GameObject[] gameObjects, float[] weights, System.Action<GameObject> onSpawn, System.Action onCompleted)
    {
        const float xmin = -220.0f;
        const float xmax = 220.0f;
        const float ymin = -220.0f;
        const float ymax = 220.0f;

        for (int xarea = 0; xarea < density; xarea++)
        {
            for (int yarea = 0; yarea < density; yarea++)
            {
                for (int retry = 0; retry < retries; ++retry)
                {
                    bool ok = true;
                    float warea = (xmax - xmin) / (float)density;
                    float harea = (ymax - ymin) / (float)density;
                    float xoffset = warea * (float)xarea;
                    float yoffset = harea * (float)yarea;
                    float x = Random.Range(xmin + xoffset, xmin + xoffset + warea);
                    float y = Random.Range(ymin + yoffset, ymin + yoffset + harea);
                    Vector3 pos = new Vector3(x, 0.0f, y);

                    foreach (Collider c in Physics.OverlapSphere(pos, overlapSphere))
                    {
                        if (blacklistedTag == "" || c.tag != blacklistedTag)
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        float r = Random.Range(0.0f, 1.0f);
                        Debug.Assert(gameObjects.Length == weights.Length);
                        bool spawned = false;
                        for (int i = 0; i < gameObjects.Length; ++i)
                        {
                            if (r <= weights[i])
                            {
                                GameObject o = Instantiate(gameObjects[i]).gameObject;
                                o.transform.position = pos;
                                o.transform.localScale = Vector3.one * 1.0f;
                                yield return new WaitForEndOfFrame();
                                spawned = true;
                                onSpawn(o);
                                break;
                            }
                        }
                        Debug.Assert(spawned);
                        break;
                    }
                }
            }
        }
        onCompleted();
    }
}
