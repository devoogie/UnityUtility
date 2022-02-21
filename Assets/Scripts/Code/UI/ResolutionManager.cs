
using UnityEngine;
using UnityEngine.Events;

public class ResolutionManager : MonoBehaviour,IManager
{
    [SerializeField] Canvas canvasMain;
    static System.Action OnResolutionChange = null;
    static System.Action OnOrientationChange = null;
    static ScreenOrientation orientation; // Current Screen Orientation
    static Vector2 resolution; // Current Resolution
    public static Vector2 Resolution{get{return resolution;}}
    public static float SkeletonFix = 1f;
    public static float Ratio;
    void Awake()
	{
        Initialize();
    }

    public void Initialize()
    {
        orientation = Screen.orientation;
        float rate = 1440f / 2560f;
        OnResetResolution();
        SkeletonFix = (Screen.width / (float)Screen.height) / rate;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        OnRectTransformDimensionsChange();
    }

    private void OnRectTransformDimensionsChange()
    {
        // Check for an orientation change.
        ScreenOrientation curOrientation = Screen.orientation;
        switch (curOrientation)
        {
            default:
                {
                    if (orientation != curOrientation)
                    {
                        orientation = curOrientation;
                        OnOrientationChange?.Invoke();
                    }
                    break;
                }
        }

        // Check for a resolution change.
        if ((resolution.x != Screen.width && resolution.x != Screen.height) || (resolution.y != Screen.height && resolution.y != Screen.width))
        {
            OnResetResolution();
            OnResolutionChange?.Invoke();
        }
    }
    private static void OnResetResolution()
    {
        resolution = new Vector2(Screen.width, Screen.height);
        Ratio = resolution.y / resolution.x;
    }

    public static void AddResolutionChangeListener(System.Action callback)
    {
        OnResolutionChange += (callback);
    }

    public static void RemoveResolutionChangeListener(System.Action callback)
    {
        OnResolutionChange -= (callback);
    }

    public static void AddOrientationChangeListener(System.Action callback)
    {
        OnOrientationChange += (callback);
    }

    public static void RemoveOrientationChangeListener(System.Action callback)
    {
        OnOrientationChange -= (callback);
    }

}