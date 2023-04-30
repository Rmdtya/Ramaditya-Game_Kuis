using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]

public class PlayerProgress : ScriptableObject
{
    [System.Serializable]

    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }


    [SerializeField]
    private string _fileName = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progresData = new MainData();

   
    public void SimpanProgres()
    {
        // Sampel Data
        /*progresData.koin = 200;
        if (progresData.progresLevel == null) 
            progresData.progresLevel = new();

            progresData.progresLevel.Add("Level Pack 1", 3);
            progresData.progresLevel.Add("Level Pack 3", 5);*/

        //Simpan Starting Data Saat object dictionary tidak ada saat dimuat
        if(progresData.progresLevel == null)
        {
            progresData.progresLevel = new();
            progresData.koin = 0;
            progresData.progresLevel.Add(_startingLevelPackName, 1);
        }


        
        ////Informasi Alamat dan Penyimpanan Data
 #if UNITY_EDITOR 
        var directory = Application.dataPath + "/temporary/";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif
        var path = directory + _fileName;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory Has Been Created" + directory);
        }

        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File Created : " + path);
        }


        //var konten = $"{progresData.koin}\n";
        var fileStream = File.Open(path, FileMode.Open);
        //var formatters = new BinaryFormatter();

        fileStream.Flush();
        //formatters.Serialize(fileStream, progresData);

        //Menyimpan Data ke dalam file menggunakan binary writer
        var writer = new BinaryWriter(fileStream);

        writer.Write(progresData.koin);
        foreach (var i in progresData.progresLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        writer.Dispose();

        //putuskan aliran memori dengan file
        fileStream.Dispose();

        /*foreach(var i in progresData.progresLevel)
        {
            konten += $"{i.Key} {i.Value}\n";
        }
        File.WriteAllText(path, konten);*/

        Debug.Log($"{_fileName} Berhasil Disimpan");

    }

    public bool MuatProgres()
    {
        //Informasi Alamat dan Penyimpanan Data
        var directory = Application.dataPath + "/temporary";
        var path = directory + "/" + _fileName;

        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);
            try
            {
                progresData.koin = reader.ReadInt32();
                if (progresData.progresLevel == null)
                    progresData.progresLevel = new();

                while(reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack} - {levelKe}");
                }

                reader.Dispose();

            }
            catch (System.Exception e)
            {
                Debug.Log($"Error : Terjadi kesalahan saat memuat binary\n{e.Message}");

                //putuskan aliran memori dengan file
                fileStream.Dispose();
                reader.Dispose();
                return false;
            }

            /*var formatters = new BinaryFormatter();

            progresData = (MainData)formatters.Deserialize(fileStream); //Mengembalikan format binary menjadi data semula
        */
            //putuskan aliran memori dengan file
            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;

        } catch(System.Exception e)
        {
            Debug.Log($"Error : Terjadi kesalahan saat mmemuat progress\n{e.Message}");

            //putuskan aliran memori dengan file
            fileStream.Dispose();

            return false;
        }
    }
}
