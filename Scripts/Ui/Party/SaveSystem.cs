using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void Save(string dataName, int[] intData = null) {

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/" + dataName + ".silverfish";

        FileStream stream = new FileStream(path, FileMode.Create);

        if (intData != null) {

            formatter.Serialize(stream, intData);

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

            FileStream stream = new FileStream(path, FileMode.Create);

            stream.Close();

            return null;

        }

    }

}
