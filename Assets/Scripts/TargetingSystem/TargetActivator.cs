using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivator : MonoBehaviour{
    private int numberOfObservers = 0;
    public GameObject physicalTarget;

    public void add(TargetManager tm) {
        numberOfObservers++;
        if (numberOfObservers == 1) activate(); 
    }
    public void remove(TargetManager tm) {
        numberOfObservers--;
        if (numberOfObservers == 0) deactivate();
    }

    private void activate() {
        //display target.....
        physicalTarget.SetActive(true);
        physicalTarget.GetComponent<Target>().activate();
    }
    private void deactivate() {
        //dont display target
        physicalTarget.SetActive(false);
    }

    
}
