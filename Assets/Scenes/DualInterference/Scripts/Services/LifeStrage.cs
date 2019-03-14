using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LifeStrage : MonoBehaviour {

    public TaggedModel[] models;
    public TaggedTexture[] textures;

    protected Dictionary<ModelType, Texture> modelMap = new Dictionary<ModelType, Texture>();
    protected Dictionary<TextureType, Texture> textureMap = new Dictionary<TextureType, Texture>();

    #region interface
    public static LifeStrage Instance { get; protected set; }

    public Texture this[ModelType key] {
        get {
            return modelMap[key];
        }
    }
    public Texture this[TextureType key] {
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
        public ModelType key;
        public Texture value;
    }
    [System.Serializable]
    public class TaggedTexture {
        public TextureType key;
        public Texture value;
    }
    #endregion
}
