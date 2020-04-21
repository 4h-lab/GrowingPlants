using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinStarsCalcScoreStep : BaseCalcScoreStep{
    public int minAwardedStars = 0;

    public override int step(List<string> s) {
        return minAwardedStars;
    }

}
