using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable] // this is required to inform unity that this is a serializable class
public class SavedGameData { 
    /* This class will contains all the game informations that need to be stored and that have to persist from one 
     * game to another. It shouldn't be used directly, and instead it should be accessed by the save and load methods from 
     * the @SaveLoadManager class.
     * This class is a Singleton.
     */
     [System.Serializable]
    public class LevelData { // this class will contains the informations pertaininig a single level
        public int levelID; // the id of this level's scene.
        public bool unlocked;
        //public bool completed;
        public float bestTime;
        public int stars;

        public LevelData(int id) {
            levelID = id - SavedGameData.minLevelIndex;
            unlocked = false;
            bestTime = float.MaxValue;
            stars = 0;
        }

        public override string ToString() {
            return "liv( " + levelID + " ) : " + new string('*', stars) + " unlocked: " + unlocked + " time: " + bestTime;
        }
    }

    public static SavedGameData gamedata { // this is the reference to the game data that need to be saved or loaded
        get {
            if (_sd == null) _sd = getSGDInstance();
            return _sd; }
        private set { }
    }

    private static SavedGameData _sd = null;
    private static int maxLevelIndex = SceneManager.sceneCountInBuildSettings;

    private static int minLevelIndex = 2;//SceneManager.GetSceneByName("Lv1").buildIndex; //todo: sostituire questo con qualcosa di meno error-prone;

    private Dictionary<int, LevelData> levels;

    private static SavedGameData getSGDInstance() { 
        /* This method is a constructor method.
         * It will try to load the game data and return the previously saved data. If it can't find a save file, 
         * it will create a new instance.
         */
        SavedGameData sgd = SaveLoadManager.load();
        if (sgd == null) sgd = new SavedGameData();
        Debug.Log(sgd);
        return sgd;
    }

    private SavedGameData() { 
        levels = new Dictionary<int, LevelData>(maxLevelIndex - minLevelIndex);
        for (int i = minLevelIndex; i < maxLevelIndex; i++) { // todo: modificare l'indice in modo che i corrisponda all indice della scena
            levels.Add(i, new LevelData(i)); // fill the dictionary with all the levels;
        }
        levels[minLevelIndex].unlocked = true; // the first level should always be unlocked;

        Debug.Log("Number of levels : " + (maxLevelIndex - minLevelIndex) + " ( " + maxLevelIndex + " - " + minLevelIndex + " )");
        
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

    public void unlockNewLevel(int id) {
        /* This method will unlock (set the unlocked variable a true) the level with ID id. 
         */
        if (!levels.ContainsKey(id)) {
            levels.Add(id, new LevelData(id));
        }
        levels[id].unlocked = true;
    }

    public float getLevelTime(int id) {
        /*  this method returns the best time the player has achieved for the current level, or a number less than zero if it hasn't 
         *  completed the level yet
         */
        return (levels.ContainsKey(id)) ? levels[id].bestTime : -1f;
    }

    public Dictionary<int, LevelData> getLevelInfos() {
        /* This method returns a shallow copy of the levels dictionary. 
         */
        return new Dictionary<int, LevelData>(levels);

    }

    public override string ToString() {
        string str = "-------------------------------------\n";
        foreach (KeyValuePair<int, LevelData> lv in levels) {
            str += "[" + lv.Key + "] >> " + lv.Value.ToString() + "\n";
        }
        return str + "-------------------------------------"; 
    }
}
