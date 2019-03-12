using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LifeStrage : MonoBehaviour {

    public TaggedModel[] models;
    public TaggedTexture[] textures;

    protected Dictionary<Life.ModelType, Texture> modelMap = new Dictionary<Life.ModelType, Texture>();
    protected Dictionary<Life.TextureType, Texture> textureMap = new Dictionary<Life.TextureType, Texture>();

    #region interface
    public static LifeStrage Instance { get; protected set; }

    public Texture this[Life.ModelType key] {
        get {
            return modelMap[key];
        }
    }
    public Texture this[Life.TextureType key] {
        get {
            return textureMap[key];
        }
    }
    #endregion

    #region unity
    protected void OnEnable() {
        Instance = this;

        modelMap.Clear();
        textureMap.Clear();

        foreach (var m in models)
            modelMap[m.key] = m.value;
        foreach (var t in textures)
            textureMap[t.key] = t.value;
    }
    #endregion

    #region classes
    [System.Serializable]
    public class TaggedModel {
        public Life.ModelType key;
        public Texture value;
    }
    [System.Serializable]
    public class TaggedTexture {
        public Life.TextureType key;
        public Texture value;
    }
    #endregion
}
