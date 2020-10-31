using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShowHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private HealthBar health;

	private void Start()
	{
		health = this.GetComponent<HealthBar>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			health.HealthUpdate(1 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			health.HealthUpdate(2 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			health.HealthUpdate(3 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			health.HealthUpdate(4 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			health.HealthUpdate(5 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			health.HealthUpdate(6 / 10f);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			health.HealthUpdate(7 / 10f);
		}
	}
}
