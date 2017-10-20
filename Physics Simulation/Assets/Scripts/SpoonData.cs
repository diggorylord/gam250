using UnityEngine;

[CreateAssetMenu(fileName = "SpoonData", menuName = "Menyoo/Spooner/Spoon", order = 1)]
public class SpoonData : ScriptableObject
{
	[SerializeField]
	public GameObject objectToSpoon;
	[SerializeField]
	public float reloadTime;
	[SerializeField]
	public int spoonAmount;
}