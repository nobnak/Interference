using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MDDB : IMDDB<ModelType, TextureType, Species, Life> {
    protected List<Life> lifes = new List<Life>();

    public IList<Life> Lifes { get { return lifes; } }
    public IEnumerable<Life> this[ModelType a] {
        get {
            return lifes.Where(v => v.CurrentKey.model == a);
        }
    }

    public IEnumerable<Life> this[TextureType b] {
        get {
            return lifes.Where(v => v.CurrentKey.tex == b);
        }
    }
    public IEnumerable<Life> this[ModelType a, TextureType b] {
        get {
            return lifes.Where(v => v.CurrentKey.model == a && v.CurrentKey.tex == b);
        }
    }
    public IEnumerable<Life> this[Species k] {
        get {
            return this[k.model, k.tex];
        }
    }

    public void Add(Species k, Life v) {
        lifes.Add(v);
    }

    public void Remove(Species k, Life v) {
        lifes.Remove(v);
    }
}
