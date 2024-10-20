using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int playerLevel = 1;  // Starting level
    public int maxLevel = 100;   // Maximum possible level
    public Text levelText;       // UI text to display the level
    public int currentExp;       // Current experience points
    public int baseExp = 1000;   // Experience required for level 2
    public int[] expToLevelUp;   // Array to store experience needed for each level

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the level text UI
        UpdateLevelText();
        
        // Initialize the expToLevelUp array
        expToLevelUp = new int[maxLevel];
        
        // Set the experience needed for level 2
        expToLevelUp[1] = baseExp;
        
        // Calculate experience needed for each level beyond 2
        for(int i = 2; i < expToLevelUp.Length; i++)
        {
            expToLevelUp[i] = Mathf.FloorToInt(expToLevelUp[i - 1] * 1.05f); // Calculate using the previous level's value
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Simulate experience gain for testing purposes (remove or modify for your use case)
        if (Input.GetKeyDown(KeyCode.E))  // Example key to simulate experience gain
        {
            GainExperience(500);  // Adds 500 experience points (adjust as needed)
        }
    }

    // Method to handle experience gain
    public void GainExperience(int exp)
    {
        currentExp += exp;

        // Check if player can level up
        while (playerLevel < maxLevel && currentExp >= expToLevelUp[playerLevel])
        {
            LevelUp();
        }
    }

    // Method to handle leveling up
    private void LevelUp()
    {
        playerLevel++;
        currentExp -= expToLevelUp[playerLevel - 1];  // Subtract the experience needed for this level

        UpdateLevelText();  // Update the UI text to reflect the new level
    }

    // Method to update the level text UI
    private void UpdateLevelText()
    {
        levelText.text = "Level: " + playerLevel;
    }
}
