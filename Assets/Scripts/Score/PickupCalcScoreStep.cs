using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCalcScoreStep : BaseCalcScoreStep {
    public override int step(List<string> s) {
        Debug.Log("calc step");
        return s.FindAll(e => e == "star.pickup").Count;
    }

}
