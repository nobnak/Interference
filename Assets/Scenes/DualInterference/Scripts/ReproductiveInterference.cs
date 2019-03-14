using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductiveInterference : MonoBehaviour {

    public float depth = 10f;

    public Life prefab;
    public Camera cmain;
    
    #region unity
    private void Update() {
        if (Input.GetMouseButton(0)) {
            var forward = cmain.transform.forward;

            var worldRay = cmain.ScreenPointToRay(Input.mousePosition);
            var len = Vector3.Dot(forward, worldRay.direction);
            var worldPos = worldRay.GetPoint(depth / len);

            var life = Instantiate(prefab);
            var tr = life.transform;
            tr.SetParent(transform, false);
            tr.position = worldPos;

            var key = new Species(
                (ModelType)Random.Range(0, (int)ModelType.END),
                (TextureType)Random.Range(0, (int)TextureType.END));
            life.CurrentKey = key;

            life.gameObject.SetActive(true);
        }
    }
    #endregion
}
