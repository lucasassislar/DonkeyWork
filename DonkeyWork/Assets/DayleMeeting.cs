using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
        Debug.Log("startmeeting");
        if (DeterminismManager.Instance.rulesAsset.nCurrentDay == 1)
        {
            Debug.Log("startmeeting__________dia 1");
            birdsTalkingTimeline.Play();
        }
        else
        {
            Debug.Log("startmeeting__________outrosdias");
            birdsTalkingOtherDaysTimeline.Play();
        }
      
    }
    void Update()
    {

        //Keyboard keyboard = Keyboard.current;
        //if (keyboard == null)
        //{
        //    return;
        //}

        //if (waitForPlayerAnswer && (keyboard.xKey.wasPressedThisFrame || keyboard.yKey.wasPressedThisFrame || keyboard.zKey.wasPressedThisFrame))
        //{
        //    Debug.Log("playerpressed_______________");
        //    birdsReactTimeline.Play();
        //}
        //if ((keyboard.escapeKey.isPressed ) && !BirdsLeaving)
        //{
        //    birdsTalkingTimeline.Stop();
        //    birdsTalkingOtherDaysTimeline.Stop();

        //    BirdsLeave();
           
        //}
    }
    public void BirdsLeave()
    {
        BirdsLeaving = true;
        birdsLeaveTimeline.Play();
    }
    public void PlayerAnswer()
    {
        Debug.Log("waiting for answer true_______________");
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
