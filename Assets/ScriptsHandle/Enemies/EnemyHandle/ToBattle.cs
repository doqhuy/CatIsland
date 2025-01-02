using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattle : MonoBehaviour
{
	GeneralInformation GeneralInformation = GeneralInformation.Instance;

	private Transform player;
	public List<EnemyWithLv> enemies;
	public List<Equipment> equipments;
	public List<Item> items;
    public int CurrentZone;

	public bool IsEventActive = true;
	public bool AutoBattle = true;
	public float RadiusActive = 1.4f;
	//Live, Dead, Checking
	public bool CheckDead = false;
	public int id;
	public float SpawnTime = 10f;
    private void Start()
	{
		player = GameObject.FindWithTag("Player").transform;
	}
	public void ChangeScene()
	{

		SceneManager.LoadScene("BattleScene");
	}
	void Update()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		// Kiểm tra xem người chơi có tiếp cận NPC và nhấn nút J hay không
		if (distanceToPlayer < RadiusActive)
		{

			GeneralInformation.EnemyInBattle.Clear();
			GeneralInformation.Zone = CurrentZone;
			int EnemyCount = Random.Range(1, 4);
			for(int i=0; i<EnemyCount;i++)
			{
				EnemyWithLv Elv = enemies[Random.Range(0, enemies.Count)];
				Enemy cloneEnemy = Elv.Enemy.Clone();
				cloneEnemy.Level = Elv.Lv;
				GeneralInformation.EnemyInBattle.Add(cloneEnemy);
			}

			int EquipmentCount = Random.Range(0, 4);
			GeneralInformation.EquipmentDrop.Clear();
			for (int i=0; i< EquipmentCount; i++)
			{
				Equipment equipment = equipments[Random.Range(0, equipments.Count)];
				GeneralInformation.EquipmentDrop.Add(equipment);
			}

			int ItemCount = Random.Range(0, 4);
			GeneralInformation.ItemDrop.Clear();
			for (int i = 0; i < ItemCount; i++)
			{
				Item item = items[Random.Range(0, items.Count)];
				GeneralInformation.ItemDrop.Add(item);
			}
			BattleNow();
		}
	}

	public void BattleNow()
	{
		GameObject ParentObj = transform.parent.gameObject;
		EnemyOnMapSave enemyOnMapSave = ParentObj.GetComponent<EnemyOnMapSave>();

        if (GeneralInformation.Actioning == "Playing")
        {
            if (AutoBattle == true)
            {
                enemyOnMapSave.GetChildBattle(this.gameObject);
                SceneManager.LoadScene("BattleScene");
            }
            else if (Input.GetKeyDown(KeyCode.J) || ActionButton.Instance.IsPress)
            {
                ActionButton.Instance.ResetIsPress();
                enemyOnMapSave.GetChildBattle(this.gameObject);
                SceneManager.LoadScene("BattleScene");
            }
        }
    }	
}

[System.Serializable]
public class EnemyWithLv
{
	public Enemy Enemy;
	public int Lv;
}
