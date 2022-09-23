using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUpdater : MonoBehaviour
{
    [SerializeField] private bool autoUpdate =  false;

    private bool updatingGraph;

    private void Awake()
    {
        updatingGraph = false;
    }

    private void Start()
    {
        if (autoUpdate && !updatingGraph)
        {
            StartGraphUpdates();
        }
    }   

    public void StartGraphUpdates()
    {
        if (!updatingGraph)
        {
            updatingGraph = true;
            StartCoroutine("UpdateGraphRoutine");
        }
    }
    public void StopGraphUpdates()
    {
        if (updatingGraph)
        {
            updatingGraph = false;
            StopCoroutine("UpdateGraphRoutine");
        }
    }

    private IEnumerator UpdateGraphRoutine()
    {
        while (true)
        {
            AstarPath.active.Scan();
            yield return null; new WaitForSeconds(.5f);
        }
    }
}
