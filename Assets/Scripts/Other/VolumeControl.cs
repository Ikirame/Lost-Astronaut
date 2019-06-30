// file: VolumeControl.cs
using UnityEngine;
// Include UnityEngine.Audio for the AudioMixer
using UnityEngine.Audio;
using System.Collections;

/* ----------------------------------------
 * class to demonstrate how to change the sound volume of Audio Mixers through GUI and script.
 * This script should be attached to the GUI game object.
 */ 
public class VolumeControl : MonoBehaviour
{
	// A variable where to assign our Audio Mixer to
	public AudioMixer myMixer;


	/* ----------------------------------------
	 * A function for changing the Volume of the music
	 * The function receives a float value as the new volume level
	 */
	public void ChangeMusicVol(float vol)
	{
		// Assigns to the exposed parameter 'MusicVolume' a new volume level, converted from linear to decibels
		myMixer.SetFloat ("MusicVolume", Mathf.Log10(vol) * 20f);
	}

	/* ----------------------------------------
	 * A function for changing the Volume of sound effects
	 * The function receives a float value as the new volume level
	 */
	public void ChangeFxVol(float vol)
	{
		// Assigns to the exposed parameter 'FxVolume' a new volume level, converted from linear to decibels
		myMixer.SetFloat ("FxVolume", Mathf.Log10(vol) * 20f);
	}

	/* ----------------------------------------
	 * A function for changing the Overall Volume 
	 * The function receives a float value as the new volume level
	 */
	public void ChangeOverallVol(float vol)
	{
		// Assigns to the exposed parameter 'OverallVolume' a new volume level, converted from linear to decibels
		myMixer.SetFloat ("OverallVolume", Mathf.Log10(vol) * 20f);
	}
}

