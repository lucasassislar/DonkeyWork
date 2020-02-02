using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using DonkeyWork;

public class DayleMeeting : MonoBehaviour
{
    public PlayableDirector gameStartTimeline;
    public PlayableDirector birdsTalkingTimeline;
    public PlayableDirector birdsTalkingOtherDaysTimeline;

    public PlayableDirector birdsReactTimeline;
    public PlayableDirector birdsLeaveTimeline;

    private bool waitForPlayerAnswer;
    private bool BirdsLeaving;
 
    public void StartMeeting()
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
        if ((keyboard.escapeKey.isPressed ) && !BirdsLeaving)
        {
            birdsTalkingTimeline.Stop();
            birdsTalkingOtherDaysTimeline.Stop();

            BirdsLeave();
           
        }
    }
    public void BirdsLeave()
    {
        BirdsLeaving = true;
        birdsLeaveTimeline.Play();
    }
    public void PlayerAnswer()
    {
        waitForPlayerAnswer = true;
    }
    public void Reset()
    {

        BirdsLeaving = false;
        waitForPlayerAnswer = false;
    }
    public void GaranteJogoComeca()
    {
        gameStartTimeline.Play();
    }
}
