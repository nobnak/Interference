using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LifeStat : MonoBehaviour {
    public event System.Action Changed;

    protected MDDB lifes = new MDDB();

    #region unity
    private void OnEnable() {
        Instance = this;
    }
    #endregion

    #region interface
    public static LifeStat Instance { get; protected set; }

    public void Add(Life l) {
        lifes.Add(l.CurrentKey, l);
        NotiryOnChanged();
    }
    public void Remove(Life l) {
        lifes.Remove(l.CurrentKey, l);
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

            var lifes = Target.lifes;
            foreach (var m in Species.MODEL_TYPE_LIST) {
                var indent = 0;
                var us = lifes[m].ToArray();
                EditorGUILayout.LabelField(string.Format(
                    "{0}: n={1}", 
                    m.ToString(),
                    us.Count()
                    ));

                indent++;
                foreach (var t in Species.TEXTURE_TYPE_LIST) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        EditorGUILayout.LabelField(string.Format(
                            "{0}{1}={2}",
                            new string(' ', indent * 4),
                            us.Where(v => v.CurrentKey.tex == t).Count(),
                            t.ToString()
                            ));
                    }
                }
            }
        }
        #endregion
    }
#endif
}
