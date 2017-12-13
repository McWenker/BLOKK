using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechPanel : MonoBehaviour
{
	[SerializeField]
	Image selfImage;

	[SerializeField]
	Image speakerImage;

	[SerializeField]
	Text speakerName;

	[SerializeField]
	Text speechText;

	[SerializeField]
	bool isSpeaking;

	Sprite speakerSprite;

	string textValue;

	string speaker;

	public bool IsSpeaking
	{
		get
		{
			return this.isSpeaking;
		}

		set
		{
			this.isSpeaking = value;
			selfImage.enabled = isSpeaking;
			speakerImage.enabled = isSpeaking;
			speakerName.enabled = isSpeaking;
			speechText.enabled = isSpeaking;
		}
	}

	public Sprite SpeakerSprite
	{
		get
		{
			return this.speakerSprite;
		}

		set
		{
			this.speakerSprite = value;
			speakerImage.sprite = speakerSprite;
		}
	}

	public string Speaker
	{
		get
		{
			return this.speaker;
		}
		
		set
		{
			this.speaker = value;
			speakerName.text = speaker;
		}
	}

	public string TextValue
	{
		get
		{
			return this.textValue;
		}
		
		set
		{
			this.textValue = value;
			speechText.text = textValue;
		}
	}

	void Start()
	{
		isSpeaking = false;
	}
}
