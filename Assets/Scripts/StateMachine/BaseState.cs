using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseState<T>
{
    void EnterState(T character);
    void Update(T character);
}
