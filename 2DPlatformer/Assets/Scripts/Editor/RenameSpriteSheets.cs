using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
 
public class NameSpritesAutomatically : MonoBehaviour
{

    [SerializeField]
    static public Dictionary<string, int> frames;
 
    [SerializeField]
    static public int xSize;

    [SerializeField]
    static public int ySize;

    [SerializeField]
    static public string file;

    [MenuItem("Sprites/Rename Sprites")]
    static void SetSpriteNames()
    {
 
        Texture2D myTexture = (Texture2D)AssetDatabase.LoadAssetAtPath<Texture2D>(file);
 
        string path = AssetDatabase.GetAssetPath(myTexture);
        TextureImporter texImporter = AssetImporter.GetAtPath(path) as TextureImporter;
 
        texImporter.isReadable = true;

        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        int y = 0;
        foreach (KeyValuePair<string, int> frame in frames)
        {
            for (int x = 0; x < frame.Value; x++)
            {
                SpriteMetaData smd = new SpriteMetaData();
                smd.pivot = new Vector2(0.5f, 0.5f);
                smd.name = frame.Key + "_" + (x+1);
                smd.rect = new Rect(x * xSize, myTexture.height - y * ySize - ySize, xSize, ySize);

                newData.Add(smd);
            }
            //value to know which Y value row we are in
            y++;
        }

 
        texImporter.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}