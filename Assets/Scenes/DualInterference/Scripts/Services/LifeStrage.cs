using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LifeStrage : MonoBehaviour {

    public Model[] models;

    #region interface
    public static LifeStrage Instance { get; protected set; }

    public Model GetModel(int index) {
        return models[index % models.Length];
    }
    public Texture GetShape(int index) {
        return GetModel(index).shape;
    }
    public Texture[] GetColors(int index) {
        return GetModel(index).colors;
    }
    #endregion

    #region unity
    protected void OnEnable() {
        if (Instance != null && Instance != this) {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
    }
    protected void OnDisable() {
        if (Instance == this)
            Instance = null;
    }
    #endregion

    #region classes
    [System.Serializable]
    public class Model {
        public Texture shape;
        public Texture[] colors;
    }
    #endregion
}
