using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEmitter : MonoBehaviour{
    public delegate void EventCallback(Object[] parameters);

    [SerializeField]
    private Dictionary<string, List<EventCallback>> events;

    private void Awake(){
        events = new Dictionary<string, List<EventCallback>>();
    }

    public void on(string eventname, EventCallback eventcallback) {
        if (!events.ContainsKey(eventname)) events.Add(eventname, new List<EventCallback>());
        events[eventname].Add(eventcallback);
    }

    public void unsubscribe(string eventname, EventCallback eventCallback) {
        if (events.ContainsKey(eventname)) {
            events[eventname].Remove(eventCallback);
        }
    }

    public void invoke(string eventname, Object[] parameters) {
        Debug.Log("EVENTO INVOCATO >>> " + eventname);
        if (!events.ContainsKey(eventname)) return; 
        foreach (EventCallback e in events[eventname]) {
            if(e != null)e(parameters);
        }
    }

}
