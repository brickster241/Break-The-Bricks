using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioInfo {
    public AudioType audioType;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}


public enum AudioType {
    MAIN_MENU,
    BUTTON_CLICK,
    BRICK_EXPLOSION,
    LEVEL_BACKGROUND,
    LEVEL_COMPLETE,
    LEVEL_FAILED,
    LEVEL_PAUSED,
    BRICK_HIT
}

public enum BrickType {
    OSCILLATING,
    NONOSCILLATING,
    BOMB
}

public class Constants
{
    public const float SET_DIRECTION_BALLS_INTERVAL = 0.2f;
    public const float DISPLAY_BLINK_BRICK_INTERVAL = 0.1f;
    public const string BALL_TAG = "Ball";
    public const string GAME_OVER_TAG = "Game-Over";
    public const string TOP_WALL_TAG = "Top-Wall";
    public const string VERTICAL_WALL_TAG = "Vertical-Wall";
    public const string SQUARE_BRICK_TAG = "Square-Brick";
    public const string GROUND_TAG = "Ground";
    public static Color NON_OSCILLATING_OUTLINE_ORIGINAL_COLOR = new Color(0.706f, 1f, 0.706f);
    public static Color OSCILLATING_OUTLINE_BLINK_COLOR = new Color(0.9f, 0.9f, 1f);
    public static Color OSCILLATING_OUTLINE_ORIGINAL_COLOR = new Color(0.706f, 0.706f, 1f);
    public static Color NON_OSCILLATING_OUTLINE_BLINK_COLOR = new Color(0.9f, 1f, 0.9f);
    public static Color LEVEL_BUTTON_ENABLED_COLOR = Color.yellow;
    public static Color OSCILLATING_BRICK_COLOR = Color.blue;
    public static Color NON_OSCILLATING_BRICK_COLOR = Color.green;
    public static Color LEVEL_BUTTON_DISABLED_COLOR = new Color(0.4f, 0.7f, 0.7f);
}
