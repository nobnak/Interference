using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LifeStat : MonoBehaviour {
    public event System.Action Changed;

    protected List<Life> lifes = new List<Life>();

    #region unity
    private void OnEnable() {
        Instance = this;
    }
    #endregion

    #region interface
    public static LifeStat Instance { get; protected set; }

    public IList<Life> Lifes { get { return lifes; } }
    public void Add(Life l) {
        lifes.Add(l);
        NotiryOnChanged();
    }
    public void Remove(Life l) {
        lifes.Remove(l);
    }
    #endregion

    #region member
    private void NotiryOnChanged() {
        if (Changed != null)
            Changed();
    }
    #endregion

#if UNITY_EDITOR
    [CustomEditor(typeof(LifeStat))]
    public class LifeStatEditor : Editor {

        protected LifeStat Target { get { return (LifeStat)target; } }

        #region unity
        private void OnEnable() {
            Target.Changed += Repaint;
        }
        private void OnDisable() {
            Target.Changed -= Repaint;
        }
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
        }
        #endregion
    }
#endif
}
