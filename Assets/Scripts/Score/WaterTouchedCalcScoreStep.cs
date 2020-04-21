using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTouchedCalcScoreStep : BaseCalcScoreStep{
    public override int step(List<string> s) {
        Debug.Log("calc step!!");
        return s.Contains("water.touchedby") ? 0 : 1;
    }

}
