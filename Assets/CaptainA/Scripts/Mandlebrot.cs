using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandlebrot : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale;
    public float angle;

    private Vector2 smoothPos;
    private float smoothScale;
    private float smoothAngle;

    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.03f);

        float aspect = (float)Screen.width / (float)Screen.height; //aspect ratio of the screen
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
    }

    private void HandleInputs()
    {
        // Zoom In
        if(Input.GetKey(KeyCode.UpArrow))
        {
            scale *= .99f;
        }

        // Zoom Out
        if(Input.GetKey(KeyCode.DownArrow))
        {
            scale *= 1.01f;
        }


        Vector2 dir = new Vector2(0.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x*c, dir.x*s);

        // Move Left
        if (Input.GetKey(KeyCode.A))
        {
            //pos.x -= 0.01f * scale;
            pos -= dir;
        }

        // Move Right
        if (Input.GetKey(KeyCode.D))
        {
            //pos.x += 0.01f * scale;
            pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);

        // Move Down
        if (Input.GetKey(KeyCode.S))
        {
            //pos.y -= 0.01f * scale;
            pos -= dir;
        }

        // Move Up
        if (Input.GetKey(KeyCode.W))
        {
            //pos.y += 0.01f * scale;
            pos += dir;
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= 0.01f;
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle += 0.01f;
        }
    }
}
