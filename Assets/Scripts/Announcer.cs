using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Announcer : MonoBehaviour {
    public Text TextAnnouncements;
    public float PauseBetweenWords = 0.0f;

    public AudioClip Voice1;
    public AudioClip Voice2;
    public AudioClip Voice3;
    public AudioClip Voice4;
    public AudioClip Voice5;
    public AudioClip Voice6;
    public AudioClip Voice7;
    public AudioClip Voice8;
    public AudioClip Voice9;
    public AudioClip Voice10;
    public AudioClip Voice11;
    public AudioClip Voice12;
    public AudioClip Voice13;
    public AudioClip Voice14;
    public AudioClip Voice15;

    public AudioClip VoiceApples;
    public AudioClip VoiceAre;
    public AudioClip VoiceAwesome;
    public AudioClip VoiceBananas;
    public AudioClip VoiceBegin;
    public AudioClip VoiceClick;
    public AudioClip VoiceCount;
    public AudioClip VoiceCounting;
    public AudioClip VoiceDone;
    public AudioClip VoiceEasy;
    public AudioClip VoiceFruits;
    public AudioClip VoiceGoing;
    public AudioClip VoiceGreat;
    public AudioClip VoiceHard;
    public AudioClip VoiceJob;
    public AudioClip VoiceKiwi;
    public AudioClip VoiceLemons;
    public AudioClip VoiceLets;
    public AudioClip VoiceMe;
    public AudioClip VoiceMedium;
    public AudioClip VoiceNice;
    public AudioClip VoiceOranges;
    public AudioClip VoicePeaches;
    public AudioClip VoicePerfect;
    public AudioClip VoicePineapples;
    public AudioClip VoicePlay;
    public AudioClip VoiceStrawberries;
    public AudioClip VoiceThe;
    public AudioClip VoiceThere;
    public AudioClip VoiceTo;
    public AudioClip VoiceToddler;
    public AudioClip VoiceWatermelons;
    public AudioClip VoiceWell;
    public AudioClip VoiceWith;

    static private Announcer _instance = null;

    private Dictionary<string, AudioClip> VoiceDictionary;
    private AudioSource AudioSource;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    void Start () {
        AudioSource = gameObject.GetComponent<AudioSource>();

        VoiceDictionary = new Dictionary<string, AudioClip>();
        VoiceDictionary.Add("1", Voice1);
        VoiceDictionary.Add("2", Voice2);
        VoiceDictionary.Add("3", Voice3);
        VoiceDictionary.Add("4", Voice4);
        VoiceDictionary.Add("5", Voice5);
        VoiceDictionary.Add("6", Voice6);
        VoiceDictionary.Add("7", Voice7);
        VoiceDictionary.Add("8", Voice8);
        VoiceDictionary.Add("9", Voice9);
        VoiceDictionary.Add("10", Voice10);
        VoiceDictionary.Add("11", Voice11);
        VoiceDictionary.Add("12", Voice12);
        VoiceDictionary.Add("13", Voice13);
        VoiceDictionary.Add("14", Voice14);
        VoiceDictionary.Add("15", Voice15);

        VoiceDictionary.Add("apples", VoiceApples);
        VoiceDictionary.Add("are", VoiceAre);
        VoiceDictionary.Add("awesome", VoiceAwesome);
        VoiceDictionary.Add("bananas", VoiceBananas);
        VoiceDictionary.Add("begin", VoiceBegin);
        VoiceDictionary.Add("click", VoiceClick);
        VoiceDictionary.Add("count", VoiceCount);
        VoiceDictionary.Add("counting", VoiceCounting);
        VoiceDictionary.Add("done", VoiceDone);
        VoiceDictionary.Add("easy", VoiceEasy);
        VoiceDictionary.Add("fruits", VoiceFruits);
        VoiceDictionary.Add("going", VoiceGoing);
        VoiceDictionary.Add("great", VoiceGreat);
        VoiceDictionary.Add("hard", VoiceHard);
        VoiceDictionary.Add("job", VoiceJob);
        VoiceDictionary.Add("kiwi", VoiceKiwi);
        VoiceDictionary.Add("lemons", VoiceLemons);
        VoiceDictionary.Add("lets", VoiceLets);
        VoiceDictionary.Add("me", VoiceMe);
        VoiceDictionary.Add("medium", VoiceMedium);
        VoiceDictionary.Add("nice", VoiceNice);
        VoiceDictionary.Add("oranges", VoiceOranges);
        VoiceDictionary.Add("peaches", VoicePeaches);
        VoiceDictionary.Add("perfect", VoicePerfect);
        VoiceDictionary.Add("pineapples", VoicePineapples);
        VoiceDictionary.Add("play", VoicePlay);
        VoiceDictionary.Add("strawberries", VoiceStrawberries);
        VoiceDictionary.Add("the", VoiceThe);
        VoiceDictionary.Add("there", VoiceThere);
        VoiceDictionary.Add("to", VoiceTo);
        VoiceDictionary.Add("toddler", VoiceToddler);
        VoiceDictionary.Add("watermelons", VoiceWatermelons);
        VoiceDictionary.Add("well", VoiceWell);
        VoiceDictionary.Add("with", VoiceWith);

        TextAnnouncements.text = "";
    }

    IEnumerator SpeakSentence(string Sentence, float WaitBeforeStart, float VoicePitch)
    {
        float WordDuration;
        Regex Regex = new Regex(@"(\S?[^\s.!?]+)");

        yield return new WaitForSeconds(WaitBeforeStart);

        foreach (Match Word in Regex.Matches(Sentence))
        {
            WordDuration = Say(Word.Value.ToLower(), VoicePitch);
            yield return new WaitForSeconds(PauseBetweenWords + WordDuration);
        }
    }

    public void Announce(string Sentence, float WaitBeforeStart = 0.0f, float VoicePitch = 1.0f)
    {
        StartCoroutine(SpeakSentence(Sentence, WaitBeforeStart, VoicePitch));
        TextAnnouncements.text = Sentence;
    }

    float Say(string Word, float Pitch)
    {
        AudioSource.pitch = Pitch;
        AudioSource.PlayOneShot(VoiceDictionary[Word]);
        return (VoiceDictionary[Word].length);
    }
}
