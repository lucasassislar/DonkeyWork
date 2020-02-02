using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using DonkeyWork;

public class DayleMeeting : MonoBehaviour
{
   
    public PlayableDirector birdsTalkingTimeline;
    public PlayableDirector birdsTalkingOtherDaysTimeline;

    public PlayableDirector birdsReactTimeline;
    public PlayableDirector birdsLeaveTimeline;

    private bool waitForPlayerAnswer;
    private bool BirdsLeaving;
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            StartMeeting();
        }
    }
    void StartMeeting()
    {
        if (DeterminismManager.Instance.rulesAsset.nCurrentDay == 1)
        {
            birdsTalkingTimeline.Play();
        }
        else
        {
            birdsTalkingOtherDaysTimeline.Play();
        }
      
    }
    void Update()
    {

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (waitForPlayerAnswer && (keyboard.xKey.isPressed || keyboard.yKey.isPressed || keyboard.zKey.isPressed))
        {
            birdsReactTimeline.Play();
        }

    }
    public void BirdsLeave()
    {
        birdsLeaveTimeline.Play();
    }
    public void PlayerAnswer()
    {
        waitForPlayerAnswer = true;
    }
    
}
