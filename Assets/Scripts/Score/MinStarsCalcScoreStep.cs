using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinStarsCalcScoreStep : BaseCalcScoreStep{
    [Tooltip("This number of stars is always awarded to the player")]
    public int minAwardedStars = 0;

    public override int step(List<string> s) {
        return minAwardedStars;
    }

}
