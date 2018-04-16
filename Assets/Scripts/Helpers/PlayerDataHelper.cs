using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataHelper {

	public static PlayerData createNewPlayer(string name)
    {
        PlayerData newPlayer = new PlayerData();
        newPlayer.playerName = name;
        initPlayerSaveFile(newPlayer);
        return newPlayer;
    }

    public static void initPlayerSaveFile(PlayerData playerData)
    {
        playerData.saveName = SaveDataHelper.createSaveFile(playerData);
    }

    public static void updatePlayerSave(PlayerData playerData)
    {
        playerData.saveName = SaveDataHelper.updateSaveFile(playerData, playerData.saveName);
    }
}
