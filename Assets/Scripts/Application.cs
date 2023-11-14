using UnityEngine;

public static class Application
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void OnLoad()
    {
#if UNITY_STANDALONE

        // On desktop force the window to be 75% of the screen height and 9:16 aspect ratio
        var height = (int)(Screen.currentResolution.height * 0.75);
        var width = (int)(height * 0.5625);
        Screen.SetResolution(width, height, FullScreenMode.Windowed);
#else
        UnityEngine.Application.targetFrameRate = 60;
#endif

    }
}