using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsUsedCalcScoreStep : BaseCalcScoreStep {
    public int maxAllowedNumberOfPlants = 5;

    public override int step(List<string> s) {
        Debug.Log("calc step");
        return s.FindAll(e => e == "plant.planted").Count <= maxAllowedNumberOfPlants ? 1 : 0;

    }


}
