using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {

    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.
    GUITexture gT;

    void Awake()
    {

        gT = this.GetComponent<GUITexture>();
        // Set the texture so that it is the the size of the screen and covers it.
        gT.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }


    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        gT.color = Color.Lerp(gT.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        gT.color = Color.Lerp(gT.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (gT.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the gT.
            gT.color = Color.clear;
            gT.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene()
    {
        // Make sure the texture is enabled.
        gT.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (gT.color.a >= 0.95f)
            // ... reload the level.
            Application.LoadLevel(0);
    }
}