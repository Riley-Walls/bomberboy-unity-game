using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplosionBehavior
{
    bool checkIfExplosionPassesThrough();
    void Explode();
}
