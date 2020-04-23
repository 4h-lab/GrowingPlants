using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static  class SaveLoadManager {
    /*This class is used to load and save the game data; note that it just contins the methods to write and 
     * read from the disk and does not contains the save file format.
     * the savefile format (with the data that need to be serialized) is specified in the @SavedGameData class.
     */

    private static string savefilename = "/save.sv";

    public static void save(SavedGameData sgd) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + savefilename); // application.persistentdatapath ritorna la directory dove unity può scrivere dati

        Debug.Log("Something something");
        bf.Serialize(fs, sgd);
        fs.Close();

        Debug.Log(sgd);
    }

    public static SavedGameData load() {
        /* This method returns the save file, or null if the file doesn't exists
         */

        SavedGameData sgd = null;
        if (File.Exists(Application.persistentDataPath + savefilename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + savefilename, FileMode.Open); // application.persistentdatapath ritorna la directory dove unity può scrivere dati
            sgd = (SavedGameData)bf.Deserialize(fs);
            fs.Close();

            Debug.Log(sgd);
        }
        return sgd;
    }

}
