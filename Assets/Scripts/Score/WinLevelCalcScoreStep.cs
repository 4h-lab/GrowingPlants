using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelCalcScoreStep : BaseCalcScoreStep{

    public override int step(List<string> s) {
        Debug.Log("calc step!");
        return s.Contains("level.finished") ? 1 : 0;
    }


}
