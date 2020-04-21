using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCalcScoreStep : MonoBehaviour {
    public abstract int step(List<string> s);
}
