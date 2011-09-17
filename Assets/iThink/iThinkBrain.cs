/*! \file iThinkBrain.cs
    \brief The agent's implementation

    The iThink brain will contain all the planning-related systems and information.
    It's primary function is to produce a plan for the parent GameObject
    according to its local knowledge (world state), available actions and goal state.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \class iThinkBrain
*/
public class iThinkBrain
{
    public List<GameObject> gameParts;   //! A list of all the GameObjects known to the agent
    public List<iThinkFact> factList;    //! A list of all the Facts known to the agent

    public iThinkState startState;       //! The first state before planning begins
    public iThinkState curState;         //! The current state
    public iThinkState goalState;        //! The goal state

    public iThinkActionSet actionSet;    //! The list of available actions
    public iThinkSensorySystem sensorySystem = new iThinkSensorySystem(); //! The system which updates the list of known \a gameParts 
    public iThinkPlanner planner;

    public float lastUpdate;

    public List<GameObject> getKnownObjects() { return gameParts; }
    public List<iThinkFact> getKnownFacts() { return factList; }

    public iThinkBrain()
    {
        lastUpdate = Time.time;

        planner = new iThinkPlanner();
        gameParts = new List<GameObject>();
    }


}
