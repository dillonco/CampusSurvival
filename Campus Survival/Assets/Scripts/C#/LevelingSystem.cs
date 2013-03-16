/*-----------------------------------------------------------------------------
 * Leveling System for Campus Survival
 * Created: March 13, 2013
 * By Dillon
 * How to use
 * Call LevelingSystem.AdjustScore(int points);
 * Where points is a positive or negative int
-----------------------------------------------------------------------------*/

using UnityEngine;
using System;
using System.Text;


[RequireComponent(typeof(AudioSource))]
public class LevelingSystem : MonoBehaviour {
	// For audio file
    public AudioClip[] nextLevelSound;
	//Max Level
    public int maxLevel = 15;
	//Experiance needed for each level
    public int[] nextLevelScore = { 0, 1000, 3000, 6000, 10000, 15000, 25000, 30000, 40000, 50000, 60000 };

	// if there is no max level, then this is the amount needed to go past
	// the last amount set above
    public int percentComplete = 100000;
	//The current score
    private int score = 0;
    //The player's current level.
    private int level = 1;
	//The Minimum level
    private const int MinimumLevel = 1;

    ///The player's score.
    public int Score {
        get {
            return score;
        }

        set {
            score = value;
		}
    }

    //Adjust the score by the specified number of points. Negative values
    //will subtract points.
    //<param name="points" />Number of points to adjust the current score
    public void AdjustScore(int points) {
        score += points;
    }

    // Adjust the current level by the specified number of levels. Negative
    // values will subtract levels. Does not adjust the score to match. The
    // new level will be clamped to within the maximum permitted level.
    //<param name="levels" />Number of levels to adjust the current level
    public void AdjustLevel(int levels) {
        level = Mathf.Clamp(level + levels, MinimumLevel, maxLevel);
    }

    // The player's current level. Specifying a new level will ensure that the
    // new level is clamped to the maximum permitted level.
    public int Level {
        get {
            return level;
        }

        set {
            level = Mathf.Clamp(value, MinimumLevel, maxLevel);
        }
    }

    // Play the audio for level up sound.
    public void PlayNextLevelSound() {
        int levelUpIndex = Mathf.Clamp(level, MinimumLevel, 
nextLevelSound.Length - 1) - 1;
        if (nextLevelSound[levelUpIndex] == null) {
            return;
        }

        this.audio.PlayOneShot(nextLevelSound[levelUpIndex]);
    }

    //Checks for completion of the current level and advances to the next
    // level if the score is high enough.
    public virtual void CheckForLevelUp() {
        // if we have reached the maximum level, do nothing
        if (level >= maxLevel) {
            return;
        }

        // check for the next required score
        int nextReqScore = 0;
        // if there are no more scores in the level score progression array
        //      switch over to linear progression
        //      otherwise, use the non-linear progression
        if (level >= nextLevelScore.Length) {
            nextReqScore = (level - nextLevelScore.Length + 1) *
percentComplete;
        }
        else {
            nextReqScore = nextLevelScore[level];
        }

        // if we have the required score to level up, advance to the next level
        if (score >= nextReqScore) {
            level = Math.Min(level + 1, maxLevel);
            PlayNextLevelSound();
        }
    }

    void Update() {
        CheckForLevelUp();
    }
}