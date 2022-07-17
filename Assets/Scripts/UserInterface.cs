using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Instance { get; private set; }

    public Camera m_cameraMinimap = null;
    public Camera m_cameraDice = null;

    public Text m_textHP = null;
    public Text m_textFood = null;
    public Text m_textTreasure = null;
    public Text m_textSpeed = null;

    public Image m_imageMinimap = null;
    public Image m_imageDice = null;
    public Slider m_turnRateSlider = null;
    public Canvas m_dialogCanvas = null;
    public Text m_eventTitle = null;
    public Text m_eventDescription = null;
    public Button m_eventCloseButton = null;

    RenderTexture m_renderTextureMinimap = null;
    Texture2D m_textureMinimap = null;
    Rect m_rectMinimap;

    RenderTexture m_renderTextureDice = null;
    Texture2D m_textureDice = null;
    Rect m_rectDice;

    void Awake()
    {
        Instance = this;
        m_dialogCanvas.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        m_renderTextureMinimap = new RenderTexture(512, 512, 24);
        m_textureMinimap = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        m_rectMinimap = new Rect(0, 0, 512, 512);
        m_cameraMinimap.targetTexture = m_renderTextureMinimap;

        m_renderTextureDice = new RenderTexture(256, 256, 24);
        m_textureDice = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        m_rectDice = new Rect(0, 0, 256, 256);
        m_cameraDice.targetTexture = m_renderTextureDice;
    }

    void Update()
    {
        {
            m_cameraMinimap.Render();
            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = m_renderTextureMinimap;
            m_textureMinimap.ReadPixels(m_rectMinimap, 0, 0);
            m_textureMinimap.Apply();
            RenderTexture.active = currentRenderTexture;
            Sprite sprite = Sprite.Create(m_textureMinimap, m_rectMinimap, Vector2.zero);
            m_imageMinimap.sprite = sprite;
        }

        {
            m_cameraDice.Render();
            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = m_renderTextureDice;
            m_textureDice.ReadPixels(m_rectDice, 0, 0);
            m_textureDice.Apply();
            RenderTexture.active = currentRenderTexture;
            Sprite sprite = Sprite.Create(m_textureDice, m_rectDice, Vector2.zero);
            m_imageDice.sprite = sprite;
        }
    }

    public void ShowDialog(EVENT_TYPE eventType, System.Action<IEvent> onEvent)
    {
        StartCoroutine(ShowDialogCoroutine(eventType, onEvent));
    }

    IEnumerator ShowDialogCoroutine(EVENT_TYPE eventType, System.Action<IEvent> onEvent)
    {
        GameTime.GameplayTimeScale = 0.0f;

        m_eventTitle.gameObject.SetActive(false);
        m_eventDescription.gameObject.SetActive(false);
        m_imageDice.gameObject.SetActive(false);
        m_eventCloseButton.gameObject.SetActive(false);
        m_eventCloseButton.onClick.RemoveAllListeners();

        while (m_dialogCanvas.transform.localScale.x < 1)
        {
            m_dialogCanvas.transform.localScale += Vector3.one * 3.0f * Time.deltaTime;
            yield return null;
        }
        m_dialogCanvas.transform.localScale = Vector3.one;

        int diceValue = -1;
        Dice.Instance.ThrowDice((result) => { diceValue = result; });
        m_imageDice.gameObject.SetActive(true);

        Dice.Instance.m_sideIcon1.material.mainTexture = TextureManager.Instance.pirate;
        Dice.Instance.m_sideIcon2.material.mainTexture = TextureManager.Instance.pirate;
        Dice.Instance.m_sideIcon3.material.mainTexture = TextureManager.Instance.pirate;
        Dice.Instance.m_sideIcon4.material.mainTexture = TextureManager.Instance.pirate;
        Dice.Instance.m_sideIcon5.material.mainTexture = TextureManager.Instance.pirate;
        Dice.Instance.m_sideIcon6.material.mainTexture = TextureManager.Instance.pirate;

        while (diceValue < 0)
        {
            yield return null;
        }

        IEvent e = EventController.GetNewEvent(eventType, diceValue);

        m_eventTitle.gameObject.SetActive(true);
        m_eventDescription.gameObject.SetActive(true);
        m_eventTitle.text = e.title;
        m_eventDescription.text = e.description;

        SoundManager.Instance.PlayAudioClip(e.sound);

        yield return new WaitForSeconds(1.0f);

        diceValue = -1;
        Dice.Instance.ThrowDice((result) => { diceValue = result; });
        m_imageDice.gameObject.SetActive(true);

        Dice.Instance.m_sideIcon1.material.mainTexture = TextureManager.Instance.p10;
        Dice.Instance.m_sideIcon2.material.mainTexture = TextureManager.Instance.p20;
        Dice.Instance.m_sideIcon3.material.mainTexture = TextureManager.Instance.p30;
        Dice.Instance.m_sideIcon4.material.mainTexture = TextureManager.Instance.p40;
        Dice.Instance.m_sideIcon5.material.mainTexture = TextureManager.Instance.p50;
        Dice.Instance.m_sideIcon6.material.mainTexture = TextureManager.Instance.p60;

        while (diceValue < 0)
        {
            yield return null;
        }

        IEvent modifiedEvent = EventController.ApplyEventModifier(e, diceValue);

        m_eventDescription.text += "\n";
        m_eventDescription.text += "\n";
        if (e.pv != 0) m_eventDescription.text += string.Format("HP: {0:+#;-#;0} ({1:+#;-#;+0}%) = {2:+#;-#;0}\n", e.pv, diceValue * 10, modifiedEvent.pv);
        if (e.speed != 0) m_eventDescription.text += string.Format("Speed: {0:+#;-#;0} ({1:+#;-#;+0}%) = {2:+#;-#;0}\n", e.speed, diceValue * 10, modifiedEvent.speed);
        if (e.food != 0) m_eventDescription.text += string.Format("Food: {0:+#;-#;0} ({1:+#;-#;+0}%) = {2:+#;-#;0}\n", e.food, diceValue * 10, modifiedEvent.food);
        if (e.treasure != 0) m_eventDescription.text += string.Format("Treasure: {0:+#;-#;0} ({1:+#;-#;+0}%) = {2:+#;-#;0}\n", e.treasure, diceValue * 10, modifiedEvent.treasure);

        m_eventCloseButton.gameObject.SetActive(true);
        m_eventCloseButton.enabled = true;
        m_eventCloseButton.onClick.AddListener(() =>
        {
            m_dialogCanvas.transform.localScale = Vector3.zero;
            GameTime.GameplayTimeScale = 1.0f;
            onEvent(modifiedEvent);
        });
    }
}
