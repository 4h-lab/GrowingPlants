using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable] // this is required to inform unity that this is a serializable class
public class SavedGameData { 
    /* This class will contains all the game informations that need to be stored and that have to persist from one 
     * game to another. It shouldn't be used directly, and instead it should be accessed by the save and load methods from 
     * the @SaveLoadManager class. */
    private class LevelData { // this class will contains the informations pertaininig a single level
        public int levelID; // the id of this level's scene.
        public bool unlocked;
        //public bool completed;
        public float bestTime;
        public int stars;

        public LevelData(int id) {
            levelID = id;
            unlocked = false;
            bestTime = float.MaxValue;
            stars = 0;
        }
    }

    public static SavedGameData gamedata {
        get {
            if (_sd == null) _sd = new SavedGameData();
            return _sd; }
        private set { }
    }

    private static SavedGameData _sd = null;

    private Dictionary<int, LevelData> levels;

    private SavedGameData() {
        levels = new Dictionary<int, LevelData>();
    }

    public void addOrModifyCompletedLevel(int id, float time, int stars) {
        /* this method add the specified level if it doesn't exist in levels (ehich is, if the player hasn't completed it yet)
         * and then update its values if they are better than the ones the playerhas already achieved
         */
        if (!levels.ContainsKey(id)) {
            levels.Add(id, new LevelData(id));
        }
        levels[id].unlocked = true;
        levels[id].bestTime = Mathf.Min(time, levels[id].bestTime);
        levels[id].stars = Mathf.Max(stars, levels[id].stars);
    }

    public float getLevelTime(int id) {
        /*  this method returns the best time the player has achieved for the current level, or a number less than zero if it hasn't 
         *  completed the level yet
         */

    }
}
