using UnityEngine;
using System.Collections;

public class Juno_SubstanceController : MonoBehaviour {

	bool displaySubstance = true;

	public ProceduralMaterial subs_Acc;
	static string ribbon_Color		= "Ribbon_Color";

	public ProceduralMaterial subs_Body;
	static string body_Color		= "Garment_Color_Output_Color";
	static string body_Opacity 		= "Garment_Color_Opacity";
	static string body_BlendMode 	= "Garment_Color_Blending_Mode";

	public ProceduralMaterial subs_Ear;
	static string ear_Color 		= "Ear_Color_Output_Color";
	static string ear_Opacity 		= "Ear_Color_Opacity";

	public ProceduralMaterial subs_Eyes_L;
	public ProceduralMaterial subs_Eyes_R;
	static string eye_Color 		= "Eye_Color_Output_Color";
	static string eye_Opacity 		= "Eye_Color_Opacity";
	static string eye_BlendMode 	= "Eye_Color_Blending_Mode";
	static string eye_Sparkle 		= "Eye_Color_Sparkle";

	public ProceduralMaterial subs_Hair;
	static string hair_Color 		= "Hair_Color_Output_Color";
	static string hair_Opacity 		= "Hair_Blend_Opacity";

	public ProceduralMaterial subs_Head;
	static string eyebrows_Color 		= "EyeBrows_Color";
	static string eyebrows_Opacity 		= "EyeBrows_Opacity";
	static string eyebrows_BlendMode 	= "EyeBrows_Blending_Mode";
	static string face_grad_Range 		= "Face_Gradient_Range";
	static string face_grad_Color 		= "Face_Gradient_Color";
	static string face_grad_Opacity 	= "Face_Gradient_Opacity";

	public ProceduralMaterial subs_Skirt;
	static string skirt_Color 		= "Skirt_Color_Output_Color";
	static string skirt_Opacity 	= "Skirt_Color_Opacity";
	static string skirt_blendMode 	= "Skirt_Color_Blending_Mode";

	public ProceduralMaterial subs_Tail;
	static string tail_Color 	= "Tail_Color_Output_Color";
	static string tail_Opacity 	= "Tail_Color_Opacity";

	void Awake ()
	{
		ProceduralMaterial.substanceProcessorUsage = ProceduralProcessorUsage.All;
	}

	//////////////////////////////////

	void Start()
	{
		CacheAcc();
		CacheBody();
		CacheEar();
		CacheEyes();
		CacheHair();
		CacheHead();
		CacheSkirt();
		CacheTail();
	}

	void CacheAcc()
	{
		if(!subs_Acc) return;
		subs_Acc.cacheSize = ProceduralCacheSize.Tiny;
		subs_Acc.CacheProceduralProperty(ribbon_Color, true);
	}

	void CacheBody()
	{
		if(!subs_Body) return;
		subs_Body.cacheSize = ProceduralCacheSize.Tiny;
		subs_Body.CacheProceduralProperty(body_Color, true);
		subs_Body.CacheProceduralProperty(body_Opacity, true);
		subs_Body.CacheProceduralProperty(body_BlendMode, true);
	}

	void CacheEar()
	{
		if(!subs_Ear) return;
		subs_Ear.cacheSize = ProceduralCacheSize.Tiny;
		subs_Ear.CacheProceduralProperty(ear_Color, true);
		subs_Ear.CacheProceduralProperty(ear_Opacity, true);
	}
	
	void CacheEyes()
	{
		if( !subs_Eyes_L || !subs_Eyes_R ) return;

		subs_Eyes_L.cacheSize = ProceduralCacheSize.Tiny;
		subs_Eyes_L.CacheProceduralProperty(eye_Color, true);
		subs_Eyes_L.CacheProceduralProperty(eye_Opacity, true);
		subs_Eyes_L.CacheProceduralProperty(eye_BlendMode, true);
		subs_Eyes_L.CacheProceduralProperty(eye_Sparkle, true);

		subs_Eyes_R.cacheSize = ProceduralCacheSize.Tiny;
		subs_Eyes_R.CacheProceduralProperty(eye_Color, true);
		subs_Eyes_R.CacheProceduralProperty(eye_Opacity, true);
		subs_Eyes_R.CacheProceduralProperty(eye_BlendMode, true);
		subs_Eyes_R.CacheProceduralProperty(eye_Sparkle, true);
	}

	void CacheHair()
	{
		if(!subs_Hair) return;
		subs_Hair.cacheSize = ProceduralCacheSize.Tiny;
		subs_Hair.CacheProceduralProperty(hair_Color, true);
		subs_Hair.CacheProceduralProperty(hair_Opacity, true);
	}

	void CacheHead()
	{
		if(!subs_Head) return;
		subs_Head.cacheSize = ProceduralCacheSize.Tiny;
		subs_Head.CacheProceduralProperty(eyebrows_Color, true);
		subs_Head.CacheProceduralProperty(eyebrows_Opacity, true);
		subs_Head.CacheProceduralProperty(eyebrows_BlendMode, true);
		subs_Head.CacheProceduralProperty(face_grad_Range, true);
		subs_Head.CacheProceduralProperty(face_grad_Color, true);
		subs_Head.CacheProceduralProperty(face_grad_Opacity, true);
	}

	void CacheSkirt()
	{
		if(!subs_Skirt) return;
		subs_Skirt.cacheSize = ProceduralCacheSize.Tiny;
		subs_Skirt.CacheProceduralProperty(skirt_Color, true);
		subs_Skirt.CacheProceduralProperty(skirt_Opacity, true);
		subs_Skirt.CacheProceduralProperty(skirt_blendMode, true);
	}

	void CacheTail()
	{
		if(!subs_Tail) return;
		subs_Tail.cacheSize = ProceduralCacheSize.Tiny;
		subs_Tail.CacheProceduralProperty(tail_Color, true);
		subs_Tail.CacheProceduralProperty(tail_Opacity, true);
	}

	//////////////////////////////////
	// BlendMode = 0_Multiply, 1_Screen, 2_Color, 3_Difference

	void OnGUI()
	{
		int rectWidth = 100;
		int rectPosX = Screen.width-rectWidth-20;
		int rectPosY = -20;
		int offsetHeight = 30;
		int buttonHeight = 25;

		Rect rect = new Rect(rectPosX, rectPosY+=offsetHeight, rectWidth, 60);
		if( GUI.Button( rect, "Display\n Substance\n Preset" ) )
		{
			displaySubstance = !displaySubstance;
		}
		if(!displaySubstance) return;

		rectPosY+=40;

		DisplaySubstancePresets( rectPosX, ref rectPosY, offsetHeight, rectWidth, buttonHeight );
	}

	//////////////////////////
	// BlendMode = 0_Multiply, 1_Screen, 2_Color, 3_Difference

	void DisplaySubstancePresets( int rectPosX, ref int rectPosY, int offsetHeight, int rectWidth, int buttonHeight )
	{

		Rect rect = new Rect(rectPosX, rectPosY+=offsetHeight, rectWidth, buttonHeight);
		if( GUI.Button( rect, "Default Color" ) )
		{
			Color tempColor = Color.white;

			UpdateAcc( Color.red );
			UpdateBody( tempColor, 0.0f, 0);
			UpdateEar( tempColor, 0.0f);
			UpdateEyes( tempColor, 0.0f, 2, 1.0f);
			UpdateHair( tempColor, 0.0f);
			UpdateHead( tempColor, 0.0f, 2, 0.35f, Color.red, 0.0f);
			UpdateSkirt( tempColor, 0.0f, 2);
			UpdateTail( tempColor, 0.0f);
		}
		
		rect = new Rect(rectPosX, rectPosY+=offsetHeight, rectWidth, buttonHeight);
		if( GUI.Button( rect, "Juno Impact" ) )
		{
			Color tempColor = Color.red;
			float clothOpacity = 0.75f;

			UpdateAcc( tempColor);
			UpdateBody( tempColor, clothOpacity, 0);
			UpdateEar( tempColor, 1.0f);
			UpdateEyes( tempColor, 0.7f, 1, 1.0f);
			UpdateHair( tempColor, 1.0f);
			UpdateHead( tempColor, 1.0f, 2, 0.35f, tempColor, 1.0f);
			UpdateSkirt( tempColor, clothOpacity, 0);
			UpdateTail( tempColor, 1.0f);
		}
		
		rect = new Rect(rectPosX, rectPosY+=offsetHeight, rectWidth, buttonHeight);
		if( GUI.Button( rect, "Yandere Juno" ) )
		{
			Color tempColor = Color.blue;
			float clothOpacity = 0.9f;

			UpdateAcc( tempColor);
			UpdateBody( tempColor, clothOpacity, 0);
			UpdateEar( tempColor, 1.0f);
			UpdateEyes( tempColor, 0.6f, 0, 0.0f);
			UpdateHair( tempColor, 1.0f);
			UpdateHead( tempColor, 1.0f, 2, 0.35f, tempColor, 1.0f);
			UpdateSkirt( tempColor, clothOpacity, 0);
			UpdateTail( tempColor, 1.0f);
		}
	}

	//////////////////////////

	void UpdateAcc( Color newColor )
	{
		if(!subs_Acc) return;
		subs_Acc.SetProceduralColor(ribbon_Color, newColor);
		subs_Acc.RebuildTextures();
	}

	void UpdateBody( Color newColor, float opacity, int blendMode )
	{
		if(!subs_Body) return;
		subs_Body.SetProceduralColor(body_Color, newColor);
		subs_Body.SetProceduralFloat(body_Opacity ,opacity);
		subs_Body.SetProceduralEnum(body_BlendMode ,blendMode);
		subs_Body.RebuildTextures();
	}

	void UpdateEar( Color newColor, float opacity )
	{
		if(!subs_Ear) return;
		subs_Ear.SetProceduralColor(ear_Color, newColor);
		subs_Ear.SetProceduralFloat(ear_Opacity ,opacity);
		subs_Ear.RebuildTextures();
	}

	void UpdateEyes( Color newColor, float opacity, int blendMode, float sparkle )
	{
		if( !subs_Eyes_L || !subs_Eyes_R ) return;

		subs_Eyes_L.SetProceduralColor(eye_Color, newColor);
		subs_Eyes_L.SetProceduralFloat(eye_Opacity ,opacity);
		subs_Eyes_L.SetProceduralEnum(eye_BlendMode ,blendMode);
		subs_Eyes_L.SetProceduralFloat(eye_Sparkle ,sparkle);

		subs_Eyes_R.SetProceduralColor(eye_Color, newColor);
		subs_Eyes_R.SetProceduralFloat(eye_Opacity ,opacity);
		subs_Eyes_R.SetProceduralEnum(eye_BlendMode ,blendMode);
		subs_Eyes_R.SetProceduralFloat(eye_Sparkle ,sparkle);

		subs_Eyes_L.RebuildTextures();
		subs_Eyes_R.RebuildTextures();
	}

	void UpdateHair( Color newColor, float opacity )
	{
		if(!subs_Hair) return;
		subs_Hair.SetProceduralColor(hair_Color, newColor);
		subs_Hair.SetProceduralFloat(hair_Opacity ,opacity);
		subs_Hair.RebuildTextures();
	}

	void UpdateHead( Color newColor, float opacity, int blendMode, float grad_Range, Color grad_Color, float grad_Opacity )
	{
		if(!subs_Head) return;
		subs_Head.SetProceduralColor(eyebrows_Color, newColor);
		subs_Head.SetProceduralFloat(eyebrows_Opacity ,opacity);
		subs_Head.SetProceduralEnum(eyebrows_BlendMode ,blendMode);

		subs_Head.SetProceduralFloat(face_grad_Range ,grad_Range);
		subs_Head.SetProceduralColor(face_grad_Color, grad_Color);
		subs_Head.SetProceduralFloat(face_grad_Opacity ,grad_Opacity);
		subs_Head.RebuildTextures();
	}

	void UpdateSkirt( Color newColor, float opacity, int blendMode )
	{
		if(!subs_Skirt) return;
		subs_Skirt.SetProceduralColor(skirt_Color, newColor);
		subs_Skirt.SetProceduralFloat(skirt_Opacity ,opacity);
		subs_Skirt.SetProceduralEnum(skirt_blendMode ,blendMode);
		subs_Skirt.RebuildTextures();
	}

	void UpdateTail( Color newColor, float opacity )
	{
		if(!subs_Tail) return;
		subs_Tail.SetProceduralColor(tail_Color, newColor);
		subs_Tail.SetProceduralFloat(tail_Opacity ,opacity);
		subs_Tail.RebuildTextures();
	}
}