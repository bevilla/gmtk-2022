using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Camera m_camera = null;
    public float m_maxTurnRate = 4.0f;
    public float m_turnRateStep = 1.0f;

    PlayerState m_playerState;

    float m_speed = 0.0f;
    float m_turnRate = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Assert(m_camera != null);
        m_playerState = GetComponent<PlayerState>();
        m_playerState.InitState(100, 10, 100, 100);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime * GameTime.GameplayTimeScale;

        float maxSpeed = m_playerState.GetSpeed();
        if (m_speed > maxSpeed)
        {
            m_speed = Mathf.Max(maxSpeed, m_speed - Mathf.Abs(maxSpeed) / 2.0f * Time.deltaTime);
        }
        else
        {
            m_speed = Mathf.Min(maxSpeed, m_speed + Mathf.Abs(maxSpeed) / 2.0f * Time.deltaTime);
        }

        if (Input.GetButtonDown("Left"))
        {
            m_turnRate = Mathf.Max(m_turnRate - m_turnRateStep, -m_maxTurnRate);
        }
        if (Input.GetButtonDown("Right"))
        {
            m_turnRate = Mathf.Min(m_turnRate + m_turnRateStep, m_maxTurnRate);
        }
        transform.Rotate(Vector3.up, m_turnRate * deltaTime);
        transform.position += transform.forward * m_speed * deltaTime;
        m_camera.transform.position = transform.position + new Vector3(0, 40, -32);

        UserInterface.Instance.m_turnRateSlider.value = m_turnRate / m_maxTurnRate;
        UserInterface.Instance.m_text.text = string.Format("HP: {0}\nMoral: {1}\nFood: {2}\nSpeed: {3}",
            m_playerState.GetPv(),
            m_playerState.GetTreasure(),
            m_playerState.GetFood(),
            m_playerState.GetSpeed()
        );
    }

    public void OnGameplayEvent(EVENT_TYPE[] eventTypes)
    {
        int eventTypeIndex = Random.Range(0, eventTypes.Length);
        int diceValue = Random.Range(1, 7);
        EVENT_TYPE pickedEventType = eventTypes[eventTypeIndex];

        UserInterface.Instance.ShowDialog(pickedEventType, (e) => m_playerState.AddEvent(e));
    }
}