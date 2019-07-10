﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(Collider))]

public class TimelineActivator : MonoBehaviour {


    public PlayableDirector playDirector;
    public string playerTAG;
    public Transform interactionLocation;
    public bool autoActivate = false;

    public bool interact { get; set; }

    [Header("Activation Zone Events")]
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    [Header("Timeline Events")]
    public UnityEvent OnTimeLineStart;
    public UnityEvent OnTimeLineEnd;

    private bool isPlaying;
    private bool playerInside;
    private Transform playerTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = true;
            playerTransform = other.transform;
            OnPlayerEnter.Invoke();
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = false;
            playerTransform = null;
            OnPlayerExit.Invoke();
        }
    }

    private void PlayTimeline()
    {
        if (playerTransform && interactionLocation)
        {
            playerTransform.SetPositionAndRotation(interactionLocation.position, interactionLocation.rotation);
        }

        if (autoActivate)
        {
            playerInside = false;
        }

        if (playDirector)
        {
            playDirector.Play();
        }

        isPlaying = true;
        interact = false;

        StartCoroutine(waitForTimeLineToEnd());

    }

    private IEnumerator waitForTimeLineToEnd()
    {
        OnTimeLineStart.Invoke();

        float timeLineDuration = (float)playDirector.duration;

        while (timeLineDuration > 0)
        {
            timeLineDuration -= Time.deltaTime;
            yield return null;
        }

        isPlaying = false;
        OnTimeLineEnd.Invoke();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInside && !isPlaying)
        {
            if (interact || autoActivate)
            {
                PlayTimeline();
            }
        }
	}
}
