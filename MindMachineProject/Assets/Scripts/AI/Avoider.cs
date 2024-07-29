using System.Collections;
using System.Collections.Generic;
using MindMachine;
using Unity.VisualScripting;
using UnityEngine;

public class Avoider : DinoAI
{

    private MindMachineBehavior<Avoider> _mindMachine;

    void Start()
    {
        _mindMachine = new MindMachineBehavior<Avoider>();
        _mindMachine.Run(this);
    }

    void OnDestroy()
    {
        _mindMachine?.Release();
    }
}
