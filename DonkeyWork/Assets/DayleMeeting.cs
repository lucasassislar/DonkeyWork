using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
public class DayleMeeting : MonoBehaviour
{
    public PlayableDirector birdsTalkingTimeline;
    public PlayableDirector birdsReactTimeline;
    public PlayableDirector birdsLeaveTimeline;
    public bool day1;

    void Update()
    {

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }
        if (day1)
        {
            birdsLeaveTimeline.Play();
        }
        else
        {
           if (keyboard.xKey.isPressed ||
              keyboard.yKey.isPressed ||
              keyboard.zKey.isPressed)
            {
                birdsReactTimeline.Play();
            }
        }
       

    }
}
