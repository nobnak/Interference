using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Life : MonoBehaviour {

    public enum ModelType { Quad, Circle, Triangle, Cross }
    public enum TextureType { Cyan, Magenta, Yellow }

    public const string PROP_MAIN_TEX = "_MainTex";
    public const string PROP_ALPHA_TEX = "_AlphaTex";

    [SerializeField]
    protected ModelType model;
    [SerializeField]
    protected TextureType tex;

    protected bool validated = false;
    protected MaterialPropertyBlock block;

    #region interface
    public ModelType CurrentModel {
        get { return model; }
        set {
            if (model != value) {
                validated = false;
                model = value;
            }
        }
    }
    public TextureType CurrentTexture {
        get { return tex; }
        set {
            if (tex != value) {
                validated = false;
                tex = value;
            }
        }
    }
    #endregion

    #region unity
    private void OnEnable() {
        Validate();
    }
    private void OnValidate() {
        validated = false;
    }
    private void Update() {
        Validate();
    }
    #endregion

    #region member
    private void Validate() {
        if (validated)
            return;

        if (LifeStrage.Instance == null)
            return;
        if (block == null)
            block = new MaterialPropertyBlock();

        var rend = GetComponent<Renderer>();
        rend.GetPropertyBlock(block);

        block.SetTexture(PROP_ALPHA_TEX, LifeStrage.Instance[model]);
        block.SetTexture(PROP_MAIN_TEX, LifeStrage.Instance[tex]);

        rend.SetPropertyBlock(block);

        validated = true;
    }
    #endregion
}
