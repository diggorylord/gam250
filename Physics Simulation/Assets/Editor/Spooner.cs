using UnityEditor;
using UnityEngine;

public class Spooner : ScriptableWizard 
{
	public string filename;
	public GameObject objectToSpoon;
	public float reloadTime;
	public int spoonAmount;

	[MenuItem("Assets/Create/Spooner/ObjectSpooner")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard<Spooner> ("Spoon Object", "Spoon", "Cancel");
	}

	private void OnWizardCreate()
	{
		var asset = ScriptableObject.CreateInstance<SpoonData> ();
		asset.objectToSpoon = objectToSpoon;
		asset.reloadTime = reloadTime;
		asset.spoonAmount = spoonAmount;

		AssetDatabase.CreateAsset (asset, "Assets/Data/" + filename + ".asset");
		AssetDatabase.SaveAssets ();
	}

	private void OnWizardOtherButton()
	{
		Close ();
	}
}