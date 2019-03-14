using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Species : IKeyTuple<ModelType, TextureType> {

    public static readonly ModelType[] MODEL_TYPE_LIST =
        ((ModelType[])System.Enum.GetValues(typeof(ModelType))).TakeWhile(v => v != ModelType.END).ToArray();
    public static readonly TextureType[] TEXTURE_TYPE_LIST =
        ((TextureType[])System.Enum.GetValues(typeof(TextureType))).TakeWhile(v => v != TextureType.END).ToArray();

    public readonly ModelType model;
    public readonly TextureType tex;

    public Species(ModelType model, TextureType tex) {
        this.model = model;
        this.tex = tex;
    }

    #region interface
    public override int GetHashCode() {
        return ((int)model) + ((int)tex * (int)TextureType.END);
    }

    #region IKeyTuple
    public ModelType A {
        get { return model; }
    }
    public TextureType B {
        get { return tex; }
    }
    #endregion
    #endregion
}
