using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    private Dictionary<string, Sprite> AtlasDictionary = new Dictionary<string, Sprite>();
    private Dictionary<string, Material> MaterialDictionary = new Dictionary<string, Material>();
    private Dictionary<string, Texture> textureDictionary = new Dictionary<string, Texture>();

    public override void Initialize()
    {
        InitSprite();
        InitMaterial();
        InitTexture();
    }
    private void InitSprite()
    {
        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("SpriteAtlas");
        Sprite[] sprites = new Sprite[atlas.spriteCount];
        string removeText = "(Clone)";
        if (atlas.GetSprites(sprites) > 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                string name = sprites[i].name.Replace(removeText, "");
                if (AtlasDictionary.ContainsKey(name) == false)
                    AtlasDictionary.Add(name, sprites[i]);
            }
        }

        sprites = null;
    }
    private void InitMaterial()
    {
        var materials = Resources.LoadAll<Material>("Materials");
        foreach(Material material in materials)
            if (MaterialDictionary.ContainsKey(material.name) == false)
                MaterialDictionary.Add(material.name,new Material(material));
    }
    private void InitTexture()
    {
        var textures = Resources.LoadAll<Texture>("Textures");
        foreach(Texture texture in textures)
            if (textureDictionary.ContainsKey(texture.name) == false)
                textureDictionary.Add(texture.name,texture);
    }
    

    public static Sprite GetSprite(string name)
    {
        Sprite sprite = null;
        if (Instance.AtlasDictionary.TryGetValue(name, out sprite))
            return sprite;

        return sprite;
    }
    
    
    public static Material GetMaterial(string name)
    {
        Material material = null;
        if (Instance.MaterialDictionary.TryGetValue(name, out material))
            return material;

        return material;
    }
    
    public static Texture GetTexture(string name)
    {
        Texture texture = null;
        if (Instance.textureDictionary.TryGetValue(name, out texture))
            return texture;

        return texture;
    }

}
