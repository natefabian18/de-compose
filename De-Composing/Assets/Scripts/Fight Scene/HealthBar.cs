using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float HealthPercent = 1; //a percent as a number between zero and one
	public GameObject HealthScaler;
	public float TotalHealth;

	private void Start()
	{
		HealthPercent = Mathf.Clamp(HealthPercent, 0, 1);
		HealthScaler.transform.localScale = new Vector3(HealthPercent, HealthScaler.transform.localScale.y, HealthScaler.transform.localScale.z);
	}

	public void HealthUpdate(float HealthPercentIn) {
		Debug.Log($"subtracking {HealthPercentIn}");
		HealthPercent = HealthPercent - HealthPercentIn;
		HealthPercent = Mathf.Clamp(HealthPercent, 0, 1);
		HealthScaler.transform.localScale = new Vector3(HealthPercent, HealthScaler.transform.localScale.y, HealthScaler.transform.localScale.z);
	}

	public void staticHealthUpdate(float health) {
		HealthPercent = health;
	}

}
