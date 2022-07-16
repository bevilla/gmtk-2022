using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera m_camera = null;
    public Camera m_cameraMinimap = null;
    public float m_speed = 2.0f;
    public float m_turnRate = 4.0f;

    public Image m_image = null;
    public Text m_text = null;

    RenderTexture m_renderTexture = null;
    Texture2D m_texture = null;
    Rect m_rect;

    void Start()
    {
        Debug.Assert(m_camera != null);
        m_renderTexture = new RenderTexture(1024, 1024, 24);
        m_texture = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
        m_rect = new Rect(0, 0, 1024, 1024);
        m_cameraMinimap.targetTexture = m_renderTexture;
    }

    void Update()
    {
        if (Input.GetButton("Left"))
        {
            transform.Rotate(Vector3.up, -m_turnRate * Time.deltaTime);
        }
        if (Input.GetButton("Right"))
        {
            transform.Rotate(Vector3.up, m_turnRate * Time.deltaTime);
        }
        transform.position += transform.forward * m_speed * Time.deltaTime;
        m_camera.transform.position = transform.position + new Vector3(0, 40, -32);

        transform.localScale = Vector3.one * 6.0f;
        m_cameraMinimap.Render();
        transform.localScale = Vector3.one;
        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = m_renderTexture;
        m_texture.ReadPixels(m_rect, 0, 0);
        m_texture.Apply();
        RenderTexture.active = currentRenderTexture;
        Sprite sprite = Sprite.Create(m_texture, m_rect, Vector2.zero);
        m_image.sprite = sprite;

        m_text.text = string.Format("HP: {0}\nMoral: {1}\nFood: {2}\nSpeed: {3}", 1, 2, 3, 4);
    }

    public void OnGameplayEvent(EVENT_TYPE[] eventTypes)
    {
        int eventTypeIndex = Random.Range(0, eventTypes.Length);
        int diceValue = Random.Range(1, 7);
        EVENT_TYPE pickedEventType = eventTypes[eventTypeIndex];

        IEvent e = EventController.GetNewEvent(pickedEventType, diceValue);

        Debug.Log(e.title);
        Debug.Log(e.description);
        e = EventController.ApplyEventModifier(e, 3);
        Debug.Log("speed: " + e.speed.ToString() + " food: " + e.food.ToString() + " moral: " + e.moral.ToString() + " hp: " + e.pv.ToString());
    }
}
