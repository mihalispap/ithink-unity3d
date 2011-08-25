///
/// <summary>
///
/// iThink GOAP library v0.0.8a
///     iThinkSensorySystem.cs
///
/// Description of file:
///     - Implements a basic system for declaring sensors usable by the agents.
///     - Currently, it only provides an active sensor which returns all objects of specified tag(s).
///     - The user can extend the class with more kinds of sensors.
///
/// </summary>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iThinkSensorySystem
{

    public void ProximityUpdate( GameObject agent, List<GameObject> gameParts )
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer( "GamePart" );
        Collider[] interactiveObjs = Physics.OverlapSphere( agent.transform.position, 20, layerMask.value );

        foreach ( Collider col in interactiveObjs )
        {
            gameParts.Add( col.gameObject );
        }

    }

    public void OmniscientUpdate( GameObject agent, List<GameObject> gameParts, List<String> Tags )
    {
        GameObject[] objects;

        foreach ( String tag in Tags )
        {
            objects = GameObject.FindGameObjectsWithTag( tag );

            foreach ( GameObject obj in objects )
                gameParts.Add( obj );
        }
    }
}
