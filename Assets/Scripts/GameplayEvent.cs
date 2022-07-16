using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvent : MonoBehaviour
{
    public bool m_SeaGood = true;
    public bool m_SeaBad = true;
    public bool m_SeaGreed = true;
    public bool m_Island = true;

    public GameObject m_ripple;
    public GameObject m_icon;

    bool m_isProcessed = false;

    private void Start()
    {
        m_isProcessed = false;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!m_isProcessed)
            {
                List<EVENT_TYPE> eventTypes = new();

                if (m_SeaGood)
                {
                    eventTypes.Add(EVENT_TYPE.SEA_GOOD);
                }
                if (m_SeaBad)
                {
                    eventTypes.Add(EVENT_TYPE.SEA_BAD);
                }
                if (m_SeaGreed)
                {
                    eventTypes.Add(EVENT_TYPE.SEA_GREED);
                }
                if (m_Island)
                {
                    eventTypes.Add(EVENT_TYPE.ISLAND);
                }
                Debug.Assert(eventTypes.Count > 0);

                other.GetComponent<PlayerController>().OnGameplayEvent(eventTypes.ToArray());
                m_isProcessed = true;
                if (m_ripple && m_icon)
                {
                    StartCoroutine(HideEventCoroutine());
                }
            }
        }
    }

    IEnumerator HideEventCoroutine()
    {
        const float rippleDisappearDuration = 4.0f;
        const float textMeshDisappearDuration = 1.0f;
        float rippleVisiblity = 1.0f;
        float textMeshVisiblity = 1.0f;
        Material material = m_ripple.GetComponent<MeshRenderer>().material;
        TextMesh textMesh = m_icon.GetComponent<TextMesh>();
        Color textMeshColor = textMesh.color;

        while (rippleVisiblity > 0.0f || textMeshVisiblity > 0.0f)
        {
            rippleVisiblity -= (1 / rippleDisappearDuration) * Time.deltaTime;
            textMeshVisiblity -= (1 / textMeshDisappearDuration) * Time.deltaTime;
            rippleVisiblity = Mathf.Max(rippleVisiblity, 0.0f);
            textMeshVisiblity = Mathf.Max(textMeshVisiblity, 0.0f);
            material.SetFloat("_RadialVisibility", rippleVisiblity);
            textMeshColor.a = textMeshVisiblity;
            textMesh.color = textMeshColor;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject, 2.0f);
        yield return null;
    }
}
