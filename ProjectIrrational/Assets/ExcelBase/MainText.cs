using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MainText : ScriptableObject
{
	public List<DialogDBEntity> DialogText; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogDBEntity> SelectText;
}
