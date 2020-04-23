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

    public static void save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + savefilename); // application.persistentdatapath ritorna la directory dove unity può scrivere dati
        bf.Serialize(fs, xxxx); // todo: sostituire xxxx con il savegame
        fs.Close();

    }

    public static void load() {
        if (File.Exists(Application.persistentDataPath + savefilename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + savefilename); // application.persistentdatapath ritorna la directory dove unity può scrivere dati

            //LOAD THE DATA
            fs.Close();

        }
    }

}
