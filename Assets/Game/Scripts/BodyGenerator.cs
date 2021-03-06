﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyState
{
//    CuteLady,
    FatLady,
    FatMale,
    HotLady,
    HotMale,
//    SkinnyMale
}

public enum BodyFaceState
{
	One,
	Two,
	Three,
}

public enum BodySkinState
{
    White,
    Brown,
    Cream,
    Dark,
}

public enum BodyGlassesState
{
    None,
    Glasses,
}

public enum BodyHatState
{
    None,
    Hat
}

public enum BodyHatColorState
{
	Black,
	Red,
	Green,
	Blue,
	Gold,
	Silver,
}

public enum BodyHairState
{
	Blonde,
	Red,
	Black,
	Silver,
	Brown,
}


public enum BodyClothesState
{
	Red,
	Gold,
	Green,
	Black,
	Blue,
	Silver,
}

public class BodyConfig
{
    public BodyState Body;
	public BodyFaceState Face;
    public BodySkinState Skin;
    public BodyGlassesState Glasses;
	public BodyHatState Hat;
	public BodyHatColorState HatColor;
	public BodyHairState Hair;
    public BodyClothesState Clothes;

    public Sprite BodySprite;
    public Sprite HairSprite;
    public Sprite GlassesSprite;
    public Sprite HatSprite;
    public Sprite ClothesSprite;

	public string hashKey() {
		return Body + "_" +
		Glasses + "_" +
		Hat + "_" +
		Hair + "_" +
		Clothes;
	}
}

public class BodyGenerator : MonoBehaviour
{
	private Dictionary<string, BodyConfig> knownConfigs = new Dictionary<string, BodyConfig>();
//	private int maxClothesColors = 3; // between 1 and 6
//	private int minClothesIndex;

	private void Awake()
	{
//		var numClothes = System.Enum.GetValues (typeof (BodyClothesState));
//		minClothesIndex = (int)(UnityEngine.Random.value * (numClothes.Length - maxClothesColors));
	}

    public BodyConfig GetNextConfig()
    {
        var config = new BodyConfig();

		while (true) {
			config.Body = this.selectRandomFromEnum<BodyState> ();
			config.Face = this.selectRandomFromEnum<BodyFaceState> ();
			config.Skin = this.selectRandomFromEnum<BodySkinState> ();
			config.Glasses = this.selectRandomFromEnum<BodyGlassesState> ();
			config.Hat = this.selectRandomFromEnum<BodyHatState> ();
			config.Hair = this.selectRandomFromEnum<BodyHairState> ();
			config.HatColor = this.selectRandomFromEnum<BodyHatColorState> ();
			config.Clothes = this.selectRandomFromEnum<BodyClothesState> ();
//			config.Clothes = this.selectRandomFromEnum<BodyClothesState> (minClothesIndex, minClothesIndex + maxClothesColors);
//			config.Clothes = this.selectRandomFromArray<BodyClothesState> (this.allowedClothing);

			if (!knownConfigs.ContainsKey(config.hashKey()))
			{
				knownConfigs.Add(config.hashKey (), config);

//				string bodyPath = this.buildBodySpritePath(config);
//				string hairPath = this.buildHairSpritePath(config);
//				string clothesPath = this.buildClothesSpritePath(config);
//				string hatPath = this.buildHatSpritePath(config);
//				string glassesPath = this.buildGlassesSpritePath(config);

				config.BodySprite = Resources.Load<Sprite> (this.buildBodySpritePath (config));
				config.HairSprite = Resources.Load<Sprite> (this.buildHairSpritePath (config));
				config.ClothesSprite = Resources.Load<Sprite> (this.buildClothesSpritePath (config));
				config.HatSprite = Resources.Load<Sprite> (this.buildHatSpritePath (config));
				config.GlassesSprite = Resources.Load<Sprite> (this.buildGlassesSpritePath (config));

				return config;
			}
		}
    }

    BodyConfig GenerateBody()
    {
        BodyConfig config = GetNextConfig();
        //config.ChooseSprites();
        return config;
    }

	private T selectRandomFromEnum<T>(int minIndex = 0, int maxIndex = int.MaxValue)
	{
//		System.Array values = System.Enum.GetValues(typeof(T));
//		var max = values.Length;
//		if (max > maxIndex)
//			max = maxIndex;
//		int index = (int)(UnityEngine.Random.value * (max-minIndex)) + minIndex;
//		return (T)values.GetValue(index);

		System.Array values = System.Enum.GetValues(typeof(T));
		return (T)values.GetValue((int)(UnityEngine.Random.value * (values.Length)));
	}

	private T selectRandomFromArray<T>(T[] array)
	{
		return array[(int)(UnityEngine.Random.value * array.Length)];
	}

	private string buildBodySpritePath(BodyConfig config)
	{
		return "Characters/" + this.bodyTypeToPath (config.Body)
			+ "/bases/face" + this.faceTypeToPath(config.Face) + this.skinTypeToPath(config.Skin);
	}

	private string buildHairSpritePath(BodyConfig config)
	{
		int hairIndex = (int)(UnityEngine.Random.value * 4) + 1;
		return "Characters/" + this.bodyTypeToPath(config.Body)
			+ "/hairs/hair" + hairIndex + this.hairColorTypeToPath(config.Hair);
	}

	private string buildClothesSpritePath(BodyConfig config)
	{
		return "Characters/" + this.bodyTypeToPath(config.Body)
			+ "/" + this.clothesDir(config) + "/" + this.clothesColorToName(config.Clothes);
	}

	private string buildHatSpritePath(BodyConfig config)
	{
		return "Characters/" + this.bodyTypeToPath(config.Body)
				+ "/hats/hat" + this.generateHatIndex(config) + this.hatColorToPath(config.HatColor);
	}

	private string buildGlassesSpritePath(BodyConfig config)
	{
		return "Characters/" + this.bodyTypeToPath(config.Body)
			+ "/glasses/" + this.glassesTypeToName(config.Glasses);
	}

	private int generateHatIndex(BodyConfig config)
	{
		int numHats = 2;
//		if (config.HatColor == BodyHatColorState.Black)
//			numHats++;

		return (int)(UnityEngine.Random.value * (numHats - 1)) + 1;
	}

	private string clothesDir(BodyConfig config) {
		if(config.Body == BodyState.HotLady ||
			config.Body == BodyState.FatLady)
			return "dresses";
		else
			return "suits";
	}

	private string bodyTypeToPath(BodyState state)
	{
		switch (state) {
//		case BodyState.CuteLady:
//			return "cutelady";
		case BodyState.FatLady:
			return "fatlady";
		case BodyState.HotLady:
			return "hotlady";
		case BodyState.FatMale:
			return "fatgent";
//		case BodyState.SkinnyMale:
//			return "skinnygent";
		case BodyState.HotMale:
			return "hotgent";
		default:
			return "";
		}
	}

	private string faceTypeToPath(BodyFaceState state)
	{
		switch (state) {
		case BodyFaceState.One:
			return "1";
		case BodyFaceState.Two:
			return "2";
		case BodyFaceState.Three:
			return "3";
		default:
			return "";
		}
	}

	private string skinTypeToPath(BodySkinState state)
	{
		switch (state) {
		case BodySkinState.White:
			return "white";
		case BodySkinState.Cream:
			return "cream";
		case BodySkinState.Brown:
			return "brown";
		case BodySkinState.Dark:
			return "dark";
		default:
			return "";
		}
	}

	private string clothesColorToName(BodyClothesState state)
	{
		switch (state) {
		case BodyClothesState.Blue:
			return "blue";
		case BodyClothesState.Red:
			return "red";
		case BodyClothesState.Black:
			return "black";
		case BodyClothesState.Green:
			return "green";
		case BodyClothesState.Gold:
			return "gold";
		case BodyClothesState.Silver:
			return "silver";
		default:
			return "";
		}
	}

	private string hatColorToPath(BodyHatColorState state)
	{
		switch (state) {
		case BodyHatColorState.Black:
			return "black";
		case BodyHatColorState.Red:
			return "red";
		case BodyHatColorState.Green:
			return "green";
		case BodyHatColorState.Blue:
			return "blue";
		case BodyHatColorState.Gold:
			return "gold";
		case BodyHatColorState.Silver:
			return "silver";
		default:
			return "";
		}
	}

	private string glassesTypeToName(BodyGlassesState state)
	{
		switch (state) {
		case BodyGlassesState.Glasses:
			return "glasses";
		default:
			return "";
		}
	}

	private string hairColorTypeToPath(BodyHairState state)
	{
		switch (state) {
		case BodyHairState.Blonde:
			return "blonde";
		case BodyHairState.Black:
			return "black";
		case BodyHairState.Red:
			return "red";
		case BodyHairState.Silver:
			return "silver";
		case BodyHairState.Brown:
			return "brown";
		default:
			return "";
		}
	}
}
