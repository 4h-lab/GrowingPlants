using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivator : MonoBehaviour{
    private int numberOfObservers = 0;
    public GameObject physicalTarget;

    public void add(TargetManager tm) {
        Debug.Log("add");
        numberOfObservers++;
        if (numberOfObservers == 1) activate(); 
    }
    public void remove(TargetManager tm) {
        Debug.Log("remove");
        if(numberOfObservers > 0) numberOfObservers--;
        if (numberOfObservers == 0) deactivate();
    }

    private void activate() {
        //display target.....
        Debug.Log("activate");
        physicalTarget.SetActive(true);
        physicalTarget.GetComponent<Target>().activate();
    }
    private void deactivate() {
        //dont display target
        Debug.Log("deactivate");
        physicalTarget.SetActive(false);
    }

    public int GetNumberOfObservers() {
        return numberOfObservers;
    }
}
