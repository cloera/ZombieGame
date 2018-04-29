using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : Enemy {

	public ItemPickup[] rareItemDropList;

	protected override void Dead ()
	{
		RareItemDrop ();

		base.Dead ();
	}

	protected void RareItemDrop()
	{
		if (rareItemDropList.Length > 0) {

			ItemDropSettings settings = GameManager.getItemSettings ();

			int luckRoll = Random.Range (0, 100);

			if (luckRoll > 100 - settings.lootChance - 20) {
				int randomPick = Random.Range (0, rareItemDropList.Length);
				ItemPickup pick = rareItemDropList [randomPick];

				Instantiate (pick, transform.position, pick.transform.rotation);
			}

		}
	}

}
