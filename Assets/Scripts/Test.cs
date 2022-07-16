using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        PlayerState playerState = GetComponent<PlayerState>();
        playerState.InitState(100, 10, 100, 100);
        IEvent pickedEvent = EventController.GetNewEvent(EVENT_TYPE.SEA_GOOD, 3);
        IEvent modifiedEvent = EventController.ApplyEventModifier(pickedEvent, 3);
        playerState.AddEvent(modifiedEvent);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
