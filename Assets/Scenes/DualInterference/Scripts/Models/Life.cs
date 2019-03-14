using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Life : MonoBehaviour {
    public const string PROP_MAIN_TEX = "_MainTex";
    public const string PROP_ALPHA_TEX = "_AlphaTex";

    protected Species key;

    protected bool validated = false;
    protected MaterialPropertyBlock block;

    #region interface
    public Species CurrentKey {
        get { return key; }
        set {
            if (!key.Equals(value)) {
                validated = false;
                key = value;
            }
        }
    }
    #endregion

    #region unity
    private void OnEnable() {
        Validate();
        LifeStat.Instance.Add(this);
    }
    private void OnDisable() {
        LifeStat.Instance.Remove(this);
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

        block.SetTexture(PROP_ALPHA_TEX, LifeStrage.Instance[key.model]);
        block.SetTexture(PROP_MAIN_TEX, LifeStrage.Instance[key.tex]);

        rend.SetPropertyBlock(block);

        validated = true;
    }
    #endregion
}
