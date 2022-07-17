using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    public Camera m_camera = null;
    public MeshRenderer m_vignette = null;
    public float m_maxTurnRate = 4.0f;
    public float m_turnRateStep = 1.0f;

    public Transform m_turnArrow = null;

    PlayerState m_playerState;

    float m_speed = 0.0f;
    float m_turnRate = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameTime.GameplayTimeScale = 1.0f;
        Debug.Assert(m_camera != null);
        m_playerState = GetComponent<PlayerState>();
        m_playerState.InitState(200, 10, 0, 100);
        m_vignette.material.SetFloat("_Visibility", 0.0f);
        UserInterface.Instance.m_hudParent.localPosition = Vector3.up * 250;
    }

    void Update()
    {
        if (!LevelGenerator.Instance.m_isReady)
            return;

        {
            float gameVisibility = m_vignette.material.GetFloat("_Visibility");
            gameVisibility += (1 / 2.0f) * Time.deltaTime;
            gameVisibility = Mathf.Min(gameVisibility, 1.0f);
            m_vignette.material.SetFloat("_Visibility", gameVisibility);

            if (gameVisibility >= 0.7f)
            {
                UserInterface.Instance.m_hudParent.localPosition = Vector3.Lerp(UserInterface.Instance.m_hudParent.localPosition, Vector3.zero, 0.05f);
            }
        }

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

        m_turnArrow.localRotation = Quaternion.Euler(0.0f, 90 + (m_turnRate / m_maxTurnRate) * 45.0f, 0.0f);

        UserInterface.Instance.m_textHP.text = m_playerState.GetPv().ToString();
        UserInterface.Instance.m_textFood.text = m_playerState.GetFood().ToString();
        UserInterface.Instance.m_textTreasure.text = m_playerState.GetTreasure().ToString();
        UserInterface.Instance.m_textSpeed.text = m_playerState.GetSpeed().ToString();
        UserInterface.Instance.m_dialogGameOverTreasure.text = "Collected treasures: " + m_playerState.GetTreasure().ToString();
    }

    public void OnGameplayEvent(EVENT_TYPE[] eventTypes)
    {
        int eventTypeIndex = Random.Range(0, eventTypes.Length);
        int diceValue = Random.Range(1, 7);
        EVENT_TYPE pickedEventType = eventTypes[eventTypeIndex];

        UserInterface.Instance.ShowDialog(pickedEventType, (e) => m_playerState.AddEvent(e));
    }
}
