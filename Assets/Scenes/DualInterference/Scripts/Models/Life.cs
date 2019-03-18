using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Life : MonoBehaviour {
    public const string PROP_MAIN_TEX = "_MainTex";
    public const string PROP_ALPHA_TEX = "_AlphaTex";

    protected Species spec;

    protected bool validated = false;
    protected MaterialPropertyBlock block;

    #region interface
    public Species CurrentKey {
        get { return spec; }
        set {
            if (!spec.Equals(value)) {
                validated = false;
                spec = value;
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

        var model = LifeStrage.Instance.GetModel(spec.model);
        block.SetTexture(PROP_ALPHA_TEX, model.shape);
        block.SetTexture(PROP_MAIN_TEX, model.colors[spec.color]);

        rend.SetPropertyBlock(block);

        validated = true;
    }
    #endregion
}
