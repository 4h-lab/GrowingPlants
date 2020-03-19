using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenPopulator : MonoBehaviour{

    private static T getComponentInChildrenWithTag<T>(GameObject originator, string tag) where T : Component {
        Transform t = originator.transform;
        foreach (Transform tr in t) {
            if (tr.tag == tag) return tr.GetComponent<T>();
        }
        return null;
    }
}
