using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void Save(string dataName, int[] intData = null, string[] stringData = null) {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + dataName + ".silverfish";

        FileStream stream = new FileStream(path, FileMode.Create);

        if (intData != null) {

            formatter.Serialize(stream, intData);

        } else if (stringData != null) {

            formatter.Serialize(stream, stringData);

        }

        stream.Close();

    }

    public static int[] LoadInt(string dataName) {

        string path = Application.persistentDataPath + "/" + dataName + ".silverfish";

        if (File.Exists(path)) {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            int[] data = formatter.Deserialize(stream) as int[];

            stream.Close();

            return data;

        } else {

            return null;

        }

    }

    public static string[] LoadStr(string dataName) {

        string path = Application.persistentDataPath + "/" + dataName + ".silverfish";

        if (File.Exists(path)) {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            string[] data = formatter.Deserialize(stream) as string[];

            return data;

        } else {

            return null;

        }

    }

}
