using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float points;

    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        points += Time.deltaTime;
        textMesh.text = points.ToString("0");
    }

    public void GetPoints(float entryPoints)
    {
        points += entryPoints;
    }
}
