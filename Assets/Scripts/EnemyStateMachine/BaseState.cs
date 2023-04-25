using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public abstract void EnterState(EnemyCtrl enemy);
    public abstract void Update(EnemyCtrl enemy);
}
