using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TesteWake : MonoBehaviour
{
    public GameObject spawnee;

    void Start()
    {
        var spawned = Instantiate(spawnee, new Vector2(0, 0), Quaternion.identity);
        var spriteShape = spawned.GetComponent<SpriteShapeController>();
        spriteShape.spline.RemovePointAt(1);
        var pos = spriteShape.spline.GetPosition(1);
        spriteShape.spline.InsertPointAt(1, new Vector3(pos.x - 3, pos.y));
        spriteShape.spline.SetTangentMode(1, ShapeTangentMode.Continuous);
        spriteShape.spline.SetLeftTangent(1, new Vector3(pos.x - 4, pos.y - 10));
        //spriteShape.spline.SetRightTangent(1, new Vector3(pos.x + 20, pos.y + 20));
        Debug.Log(spriteShape.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
