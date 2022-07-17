using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice Instance { get; private set; }

    public GameObject m_walls;
    public Camera m_diceCamera;

    const float diceThrowHeight = 5.0f;

    Rigidbody m_rigidbody;

    float m_diceThrowTimer = 0.0f;

    System.Action<int> m_onResult = null;

    void Awake()
    {
        Instance = this;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Camera
        {
            Vector3 to = transform.localPosition;
            to.y = 8.0f;
            m_diceCamera.transform.localPosition = Vector3.Lerp(m_diceCamera.transform.localPosition, to, 0.1f);
        }
        if (IsThrowingDice())
        {
            m_diceThrowTimer += Time.deltaTime;
            if (m_rigidbody.velocity.sqrMagnitude < 0.05f && m_rigidbody.angularVelocity.sqrMagnitude < 0.05f && m_walls.activeSelf && m_diceThrowTimer > 0.5f)
            {
                m_diceThrowTimer = 0.0f;
                m_walls.SetActive(false);
            }
            if (m_rigidbody.velocity.sqrMagnitude < 0.0005f && m_rigidbody.angularVelocity.sqrMagnitude < 0.0005f && m_diceThrowTimer > 0.5f)
            {
                m_rigidbody.velocity = Vector3.zero;
                m_rigidbody.angularVelocity = Vector3.zero;
                m_rigidbody.useGravity = false;

                Vector3 rayOrigin = transform.position + Vector3.up * diceThrowHeight;
                Vector3 rayDirection = transform.position - rayOrigin;
                int diceResult = 6;
                foreach (RaycastHit hit in Physics.RaycastAll(rayOrigin, rayDirection))
                {
                    if (hit.collider.gameObject.name.StartsWith("Side"))
                    {
                        int.TryParse(hit.collider.gameObject.name[4].ToString(), out diceResult);
                        break;
                    }
                }
                Debug.Assert(m_onResult != null);
                m_onResult(diceResult);
                m_onResult = null;
            }
        }
        /*else */if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowDice((diceResult) => Debug.Log("Dice result: " + diceResult));
        }
    }

    public bool IsThrowingDice()
    {
        return m_rigidbody.useGravity;
    }

    public void ThrowDice(System.Action<int> onResult)
    {
        Debug.Log("Throw dice!");
        Debug.Assert(!IsThrowingDice());
        Debug.Assert(m_onResult == null);
        m_onResult = onResult;
        m_walls.SetActive(true);
        m_diceThrowTimer = 0.0f;
        m_diceCamera.transform.localPosition = Vector3.up * 8;
        transform.localPosition = Vector3.up * diceThrowHeight;
        transform.rotation = Random.rotation;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = Vector3.zero;
        float dirx = Random.Range(40.0f, 150.0f);
        float diry = Random.Range(40.0f, 150.0f);
        float dirz = Random.Range(40.0f, 150.0f);
        m_rigidbody.useGravity = true;
        m_rigidbody.AddTorque(dirx, diry, dirz);
    }
}
