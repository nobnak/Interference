using nobnak.Gist.MathAlgorithms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReproductiveInterference : MonoBehaviour {

    public float depth = 10f;
    public float radius = 1f;
    [Range(0f, 1f)]
    public float noise = 0.1f;

    public Life prefab;
    public Camera cmain;

    protected IList<Life> lifes;

    #region unity
    private void OnEnable() {
        lifes = LifeStat.Instance.Lifes;
    }
    private void Update() {
        if (Input.GetMouseButton(0)) {
            var forward = cmain.transform.forward;

            var worldRay = cmain.ScreenPointToRay(Input.mousePosition);
            var len = Vector3.Dot(forward, worldRay.direction);
            var worldPos = worldRay.GetPoint(depth / len);
            var posOffset = (Vector3)Random.insideUnitCircle * (radius * 0.5f);
            worldPos += posOffset;

            var key = GetKey(worldPos);

            var life = Instantiate(prefab);
            life.CurrentKey = key;
            var tr = life.transform;
            tr.SetParent(transform, false);
            tr.position = worldPos;

            life.gameObject.SetActive(true);
        }
    }
    #endregion

    #region member
    private Species GetKey(Vector3 worldPos) {
        var sqr = radius * radius;
        var nears = lifes
            .Where(v => (v.transform.position - worldPos).sqrMagnitude < sqr);
        var count = nears.Count();

        int modelIndex = -1;
        int colorIndex = -1;
        if (count > 0 && !Interfered()) {
            var modelTabs = new Tabs<int, float>();
            foreach (var n in nears.Select(v => v.CurrentKey))
                modelTabs[n.model]++;

            var max = modelTabs.Values.Max();
            int valueIndex;
            if (RouletteWheelSelection.Sample(out valueIndex, 100, max, modelTabs.ValuesAsList))
                modelIndex = modelTabs.KeysAsList[valueIndex];

            var colors = nears.Where(v => v.CurrentKey.model == modelIndex);
            var colorCount = colors.Count();
            if (colorCount > 0 && !Interfered()) {
                var colorTabs = new Tabs<int, float>();
                foreach (var n in colors.Select(v => v.CurrentKey))
                    colorTabs[n.color]++;

                var maxColor = colorTabs.Values.Max();
                int colorTabIndex;
                if (RouletteWheelSelection.Sample(out colorTabIndex, 100, maxColor, colorTabs.ValuesAsList))
                    colorIndex = colorTabs.KeysAsList[colorTabIndex];
            }
        }

        if (modelIndex < 0)
            modelIndex = Random.Range(0, LifeStrage.Instance.models.Length);
        var model = LifeStrage.Instance.GetModel(modelIndex);
        if (colorIndex < 0)
            colorIndex = Random.Range(0, model.colors.Length);

        var spec = new Species(modelIndex, colorIndex);
        Debug.LogFormat("Chosen key={0} neighbors={1}", spec, count);
        return spec;
    }
    protected bool Interfered() {
        return Random.value < noise;
    }
    #endregion
}
