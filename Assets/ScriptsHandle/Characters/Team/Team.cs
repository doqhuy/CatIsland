using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
	public List<Character> Teamate;
	public List<Character> ClaimedCharacter;
	public CharacterDefaultData CharacterDefaultData;

	public bool CheckInTeam(Character character)
	{
        foreach (Character other in Teamate)
		{
			if(other!=null)
			if(other.Name == character.Name)
			{ return true; }
		}
        return false;
	}
    public void RemoveFromTeam(int index)
	{
		Teamate[index].IsInTeam = false;
		Teamate[index] = null;
	}

	bool CheckCharacterAdded(Character character)
	{
		if(ClaimedCharacter.Contains(character)) return true;
		return false;
	}

	public void AddCharacter(Character character)
	{

        CharacterDefaultData.SetDefaultData(character);
		if(!CheckCharacterAdded(character))
		{
            ClaimedCharacter.Add(character);
        }
    }

    public void AddToTeam(int index, Character character)
	{
		if (Teamate[index] != null)
		{
			Teamate[index].IsInTeam = false;
		}
		Teamate[index] = character;
		Teamate[index].IsInTeam = true;
	}
}
