using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsUsedCalcScoreStep : BaseCalcScoreStep {
    [Tooltip("The player is awarded 1 star if he used fewer or equal this number of plants ")]
    public int maxAllowedNumberOfPlants = 5;

    public override int step(List<string> s) {
        Debug.Log("calc step");
        return s.FindAll(e => e == "plant.planted").Count <= maxAllowedNumberOfPlants ? 1 : 0;

    }


}
