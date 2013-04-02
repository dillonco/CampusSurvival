/*-----------------------------------------------------------------------------
 * Leveling System for Campus Survival
 * Created: March 13, 2013
 * By Dillon
 * How to use
 * Call LevelingSystem.AdjustScore(int points, string player);
 * Options for player are either "p1" or "p2"
 * Where points is a positive or negative int
 * Example:
 * 			//Adjust Player 2's level for the kill
			LevelingSystem levelingSystem = GetComponent<LevelingSystem>();
			levelingSystem.AdjustScore(1000, "p2");
			Debug.LogError("1000 points to p2");
-----------------------------------------------------------------------------*/

using UnityEngine;
using System;
using System.Text;


[RequireComponent(typeof(AudioSource))]
public class LevelingSystem : MonoBehaviour {
	// For audio file
    public AudioClip nextLevelSound;
	//Max Level
    public int maxLevel = 15;
	//Experiance needed for each level
    public int[] nextLevelScore = { 0, 1000, 3000, 6000, 10000, 15000, 25000, 30000, 40000, 50000, 60000 };

	// if there is no max level, then this is the amount needed to go past
	// the last amount set above
    public int percentComplete = 100000;
	//The current score
    private int p1score = 0;
	private int p2score = 0;

    //The player's current level.
    private int p1level = 1;
	private int p2level = 1;
	//The Minimum level
    private const int MinimumLevel = 1;

    ///The player's score.
    public int p1Score {
        get {
            return p1score;
        }

        set {
            p1score = value;
		}
    }
	///The player's score.
    public int p2Score {
        get {
            return p2score;
        }

        set {
            p2score = value;
		}
    }

    //Adjust the score by the specified number of points. Negative values
    //will subtract points.
    //<param name="points" />Number of points to adjust the current score
    public void AdjustScore(int points, string player) {
        if (player == "p1") { p1score += points; }
		else if(player == "p2") p2score += points;
    }

//    // Adjust the current level manually
//    public void AdjustLevel(int levels) {
//        level = Mathf.Clamp(level + levels, MinimumLevel, maxLevel);
//    }

    // The player's current level. Specifying a new level will ensure that the
    // new level is clamped to the maximum permitted level.
    public int p1Level {
        get {
            return p1level;
        }

        set {
            p1level = Mathf.Clamp(value, MinimumLevel, maxLevel);
        }
    }
	public int p2Level {
        get {
            return p2level;
        }

        set {
            p2level = Mathf.Clamp(value, MinimumLevel, maxLevel);
        }
    }

    // Play the audio for level up sound.
    public void PlayNextLevelSound() {
        this.audio.PlayOneShot(nextLevelSound);
    }

    //Checks for completion of the current level and advances to the next
    // level if the score is high enough.
    public virtual void CheckForP1LevelUp() {
        // if we have reached the maximum level, do nothing
        if (p1level >= maxLevel) {
            return;
        }

        // check for the next required score
        int nextReqScore = 0;
        // if there are no more scores in the level score progression array
        //      switch over to linear progression
        //      otherwise, use the non-linear progression
        if (p1level >= nextLevelScore.Length) {
            nextReqScore = (p1level - nextLevelScore.Length + 1) *
percentComplete;
        }
        else {
            nextReqScore = nextLevelScore[p1level];
        }

        // if we have the required score to level up, advance to the next level
        if (p1score >= nextReqScore) {
            p1level = Math.Min(p1level + 1, maxLevel);
            PlayNextLevelSound();
			//INSERT CHANGES TO GLOBAL HEALTH, SPEED AND FIRERATE HERE
			Debug.LogError("Player level up");
        }
    }
	 public virtual void CheckForP2LevelUp() {
        // if we have reached the maximum level, do nothing
        if (p2level >= maxLevel) {
            return;
        }

        // check for the next required score
        int nextReqScore = 0;
        // if there are no more scores in the level score progression array
        //      switch over to linear progression
        //      otherwise, use the non-linear progression
        if (p2level >= nextLevelScore.Length) {
            nextReqScore = (p2level - nextLevelScore.Length + 1) *
percentComplete;
        }
        else {
            nextReqScore = nextLevelScore[p2level];
        }

        // if we have the required score to level up, advance to the next level
        if (p2score >= nextReqScore) {
            p2level = Math.Min(p2level + 1, maxLevel);
            PlayNextLevelSound();
			//INSERT CHANGES TO GLOBAL HEALTH, SPEED AND FIRERATE HERE
			Debug.LogError("Player level up");
        }
    }

    void Update() {
        CheckForP1LevelUp();
		CheckForP2LevelUp();
    }
	void OnGUI() {
		GUILayout.Label("Player 1 level: " + p1Level);
		GUILayout.Label("Player 2 level: " + p2Level);
	}
}