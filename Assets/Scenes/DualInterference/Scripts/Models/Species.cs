using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Species {

    public readonly int model;
    public readonly int color;

    public Species(int model, int color) {
        this.model = model;
        this.color = color;
    }

    #region interface
    public override int GetHashCode() {
        return (model) + color * 71;
    }
    public override string ToString() {
        return string.Format("{0}<model={1},color={2}>", this.GetType().Name, model, color);
    }
    #endregion
}
