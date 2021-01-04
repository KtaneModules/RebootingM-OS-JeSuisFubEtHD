using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using System;
using rnd = UnityEngine.Random;

public class RMOS : MonoBehaviour
{
    public GameObject surface;
    public KMSelectable button;
    public KMBombModule module;
    public KMSelectable status;
    public KMBombInfo info;
    public KMAudio sfx;
    public KMBombModule mod;
    public int numcycle;
    public int index;
    public int rotindex;
    public int A;
    public int A1;
    public int B;
    public int C;
    public int Final;
    public int sn;
    public int base3value;
    public int base10value;
    public int[] rotindexes = { 0, 0, 0, 0 };
    public int[] indexes = { 0, 0, 0, 0 };
    public int[] Aindexes = { 0, 0 };
    public string[] blindcoloros = { "R", "G", "B", "Y", "C", "M", "W" };
    public string[] logcoloros = { "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta", "White" };
    public string[] blindAcoloros = {"Black", "Maroon", "Red", "Indigo", "Plum", "Rose", "Blue", "Violet", "Magenta", "Forest", "Olive", "Orange", "Teal", "Gray", "Salmon", "Azure",
    "Maya", "Pink", "Green", "Lime", "Yellow", "Jade", "Mint", "Cream", "Cyan", "Aqua", "White"};
    public string[] logmoves = {"T", "B", "R", "L", "BL", "TR", "BR", "TL"};
    public int[] reds = { 1, 0, 0, 1, 0, 1, 1 };
    public int[] greens = { 0, 1, 0, 1, 1, 0, 1 };
    public int[] blues = { 0, 0, 1, 0, 1, 1, 1 };
    public string[] base10 = { "0", "9", "18", "1", "10", "19", "2", "11", "20", "3", "12", "21", "4", "13", "22", "5", "14", "23", "6", "15", "24", "7", "16", "25", "8", "17", "26" };
    public int[] base3 = { 000, 100, 200, 001, 101, 201, 002, 102, 202, 010, 110, 210, 011, 111, 211, 012, 112, 212, 020, 120, 220, 021, 121, 221, 022, 122, 222 };
    public int[] colorValues = { 99, 75, 66, 15, 38, 93, 12, 89, 46, 33, 90, 76, 45, 81, 55, 19, 27, 91, 63, 71, 35, 42, 50, 14, 69, 21, 99 };
    public float[] Areds = { 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1, 0, 0.5f, 1 };
    public float[] Agreens = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    public float[] Ablues = { 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1, 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1, 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1 };
    public int[] table1 = { 60, 44, 86, 89, 48, 47, 54, 88,
        45, 71, 48, 65, 49, 93, 98, 87,
        68, 92, 83, 96, 77, 53, 99, 64,
        80, 70, 66, 63, 74, 98, 50, 48,
        54, 48, 83, 53, 81, 57, 61, 91,
        63, 52, 58, 54, 62, 50, 86, 90,
        46, 63, 70, 72, 83, 69, 58, 52};
    public int ttp;
    public bool pressed = false;
    public bool doin = false;
    public int time;
    public int[] answer = {0, 0};
    public bool colorblind = false;
    public TextMesh colortext;
    public TextMesh Acolortext;
    public AudioClip buttonP;
    public AudioClip TurnOn;
    public AudioSource sf;
    private static int _moduleIDCounter = 1;
    private int _moduleID;
    public bool started = false;
    public bool anim = false;
    public TextMesh[] systemtext;
    public GameObject Logo;
    public TextMesh[] welcome;
    public bool tp = false;
    public int tpdigit = 0;
    public string[] systxt = { ">c3RhcnRpbmc=", ">Y2hlY2tpbmc=", ">ZG9pbnNtdGg=", ">MHgwMDAwMDE=", ">MHhGRDRBQzU=", ">YW5hbHl6ZQ==", ">ZnJlbmNoZWdn", ">cmdibWF6ZQ==", ">Ym1salpRPT0=", ">ZWVlIGVlZWU=" };
    void Awake()
    
    {
        _moduleID = _moduleIDCounter++;
        Acolortext.color = new Color(0, 0, 0, 0);
        sf = GetComponent<AudioSource>();
        sf.clip = buttonP;
        indexes[0] = rnd.Range(0, 7);
        indexes[1] = rnd.Range(0, 7);
        indexes[2] = rnd.Range(0, 7);
        indexes[3] = rnd.Range(0, 7);
        Aindexes[0] = rnd.Range(1, 27);
        Aindexes[1] = rnd.Range(1, 27);
        rotindexes[0] = rnd.Range(0, 8);
        rotindexes[1] = rnd.Range(0, 8);
        rotindexes[2] = rnd.Range(0, 8);
        rotindexes[3] = rnd.Range(0, 8);      
        module.OnActivate += delegate
        {
            started = true;
            Debug.LogFormat("[Rebooting M-OS #{0}] Shown colors: " + logcoloros[indexes[0]] + ", " + logcoloros[indexes[1]] + ", " + logcoloros[indexes[2]] + ", " + logcoloros[indexes[3]], _moduleID);
            Debug.LogFormat("[Rebooting M-OS #{0}] Moves are: {1}, {2}, {3}, {4}", _moduleID, logmoves[rotindexes[0]], logmoves[rotindexes[1]], logmoves[rotindexes[2]], logmoves[rotindexes[3]]);
            StartCoroutine(colorC());
            A = (table1[indexes[0] * 8 + rotindexes[0]] + table1[indexes[0] * 8 + rotindexes[1]] + table1[indexes[0] * 8 + rotindexes[2]] + table1[indexes[0] * 8 + rotindexes[3]]
                + table1[indexes[1] * 8 + rotindexes[0]] + table1[indexes[1] * 8 + rotindexes[1]] + table1[indexes[1] * 8 + rotindexes[2]] + table1[indexes[1] * 8 + rotindexes[3]]
                + table1[indexes[2] * 8 + rotindexes[0]] + table1[indexes[2] * 8 + rotindexes[1]] + table1[indexes[2] * 8 + rotindexes[2]] + table1[indexes[2] * 8 + rotindexes[3]]
                + table1[indexes[3] * 8 + rotindexes[0]] + table1[indexes[3] * 8 + rotindexes[1]] + table1[indexes[3] * 8 + rotindexes[2]] + table1[indexes[3] * 8 + rotindexes[3]]) % 1000;
            Debug.LogFormat("[Rebooting M-OS #{0}] Value A is {1}", _moduleID, A);
            while (A < 0)
            {
                A += 1000;
            }
            A1 = A;
            var E = 0;
            E = GetComponent<KMBombInfo>().GetSerialNumberNumbers().Sum();
            for (int i = 0; i < 4; i++)
            {
                if (indexes[i] == 0)
                {
                    A1 = (A1 + E) % 1000;
                }
                if (indexes[i] == 1)
                {
                    A1 = (A1 - E) % 1000;
                }
                if (indexes[i] == 2)
                {
                    A1 = (A1 * (i + 1)) % 1000;
                }
                if (indexes[i] == 3)
                {
                    A1 = ((2 * A1) + ((i + 1) * E)) % 1000;
                }
                if (indexes[i] == 4)
                {
                    A1 = ((A1 * (i + 1)) - E) % 1000;
                }
                if (indexes[i] == 5)
                {
                    A1 = ((E * E) - A1) % 1000;
                }
                if (indexes[i] == 6)
                {
                    A1 = (2 * Convert.ToInt32(Mathf.Pow((i + 1), 2f)) - (A1 * E)) % 1000;
                }
                while (A1 < 0)
                {
                    A1 += 1000;
                }
            }
            B = A1;
            B = B % 1000;
            while (B < 0)
            {
                B += 1000;
            }
            Debug.LogFormat("[Rebooting M-OS #{0}] Value B is {1}", _moduleID, B);
            ttp = (A + B) % 10;         
            Debug.LogFormat("[Rebooting M-OS #{0}] Background colors was: {1}, {2}", _moduleID, blindAcoloros[Aindexes[0]], blindAcoloros[Aindexes[1]]);
            sn = GetComponent<KMBombInfo>().GetSerialNumberNumbers().Last();
            if (sn == 0)
            {
                sn += 10;
            }
            base3value = base3[Aindexes[0]] * 1000 + base3[Aindexes[1]];
            base10value = Convert.ToInt32(((base3value - base3value % 100000) / 100000) * (Mathf.Pow(3, 5))) + Convert.ToInt32(((base3value % 100000 - base3value % 10000) / 10000) * Mathf.Pow(3, 4)) + Convert.ToInt32(((base3value % 10000 - base3value % 1000) / 1000) * Mathf.Pow(3, 3)) + Convert.ToInt32(((base3value % 1000 - base3value % 100) / 100) * Mathf.Pow(3, 2)) + Convert.ToInt32(((base3value % 100 - base3value % 10) / 10) * Mathf.Pow(3, 1)) + base3value % 10;
            base10value = (base10value * sn) % 27;
            for (int i = 0; i < 27; i++)
            {
                if (base10[i] == base10value.ToString())
                {
                    C = colorValues[i];
                }
            }
            Debug.LogFormat("[Rebooting M-OS #{0}] Value C is {1}", _moduleID, C);
            if (A == 0)
            {
                A = 1;
            }
            if (B == 0)
            {
                B = 1;
            }
            switch (sn % 10)
            {
                case 0:
                    Final = A + B + C;
                    break;
                case 1:
                    Final = Mathf.Abs(B - C) * A;
                    break;
                case 2:
                    Final = Convert.ToInt32(Mathf.Pow(B, 2)) - (A + C);
                    break;
                case 3:
                    Final = A * 2 - (B % C);
                    break;
                case 4:
                    Final = (A * B) % C;
                    break;
                case 5:
                    Final = (3 * C) - (A + B);
                    break;
                case 6:
                    Final = ((B - (B % 2)) / 2 - A) * C;
                    break;
                case 7:
                    Final = -A + Mathf.Abs(B - C);
                    break;
                case 8:
                    Final = A * B * C;
                    break;
                case 9:
                    Final = A - B - C;
                    break;
            }
            Final = Mathf.Abs(Final);
            if (Final >= 1000)
            {
                Final = Final % 1000;
            }
            while (Final < 100)
            {
                Final += 100;
            }
            Debug.LogFormat("[Rebooting M-OS #{0}] Correct setting is {1}", _moduleID, Final);
        };     
    }
    // Use this for initialization
    void Start()
    {
        status.OnInteract += delegate { StartCoroutine(statusP()); return false; };
        button.OnInteract += delegate {button.AddInteractionPunch(); interact();  return false; };
    }

    // Update is called once per frame
    void interact()
    {
        if (anim)
        {
            return;
        }
        if (answer[1] < 3)
        {
            switch (tp)
            {
                case false:
                    answer[0] += time * (int)Mathf.Pow(10, 2 - answer[1]);
                    break;
                case true:
                    answer[0] += tpdigit * (int)Mathf.Pow(10, 2 - answer[1]);
                    break;
            }
        
            if (answer[1] < 2)
            {
                sf.pitch = (rnd.Range(0.9f, 1.1f));
                sf.Play();
            }
            answer[1] += 1;
        }
        if (answer[1] == 3)
        {
            if ((((answer[0] - answer[0] % 100) / 100 == (answer[0] % 100 - answer[0] % 10) / 10) && ((answer[0] - answer[0] % 100) / 100 == answer[0] % 10)) && (answer[0] != Final))
            {
                sf.pitch = (rnd.Range(0.9f, 1.1f));
                sf.Play();
                answer[0] = 0;
                answer[1] = 0;
                if (!colorblind)
                {
                    colorblind = true;
                }
                else if(colorblind)
                {
                    colorblind = false;
                }
            }
            else
            {
                anim = true;
                StartCoroutine(Anim());
            }
        }
    }
    void Update()
    {
        time = (int)info.GetTime() % 10;
        if(doin)
        {
            button.gameObject.SetActive(false);
        }
        if (!colorblind)
        {
            colortext.color = new Color(0, 0, 0, 0);
        }
        if (colorblind)
        {
            colortext.color = new Color(0, 0, 0, 225);
        }
    }
    IEnumerator statusP()
    {      
        if (doin || !started || anim)
        {
            yield break;
        }
            if ((time == ttp) || pressed)
            {
                status.AddInteractionPunch();
            if (!pressed)
            {
                sfx.PlaySoundAtTransform("SLpressed", transform);
            }
            else
            {
                sfx.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.TitleMenuPressed, transform);
            }
            doin = true;
                button.gameObject.SetActive(false);
            if (colorblind)
            {
                Acolortext.color = new Color(0, 0, 0, 225);
            }
                surface.GetComponent<MeshRenderer>().material.color = new Color(Areds[Aindexes[0]], Agreens[Aindexes[0]], Ablues[Aindexes[0]]);
            Acolortext.text = blindAcoloros[Aindexes[0]];
                yield return new WaitForSeconds(.5f);
                surface.GetComponent<MeshRenderer>().material.color = new Color(Areds[Aindexes[1]], Agreens[Aindexes[1]], Ablues[Aindexes[1]]);
            Acolortext.text = blindAcoloros[Aindexes[1]];
            yield return new WaitForSeconds(.5f);
                surface.GetComponent<MeshRenderer>().material.color = new Color(.12f, .12f, .12f);
                pressed = true;
            doin = false;
            Acolortext.color = new Color(0, 0, 0, 0);
            button.gameObject.SetActive(true);
        }
            else
            {
                mod.HandleStrike();
                status.AddInteractionPunch();
            }
    }
    public IEnumerator colorC()
    {
        for (int i = 0; i < 4; i++)
        {

            rotindex = rotindexes[i];
            index = indexes[i];
            colortext.text = blindcoloros[index];
            button.GetComponent<MeshRenderer>().material.color = new Color(reds[index], greens[index], blues[index]);
            if (rotindex == 0)
            {
                button.transform.localPosition = new Vector3(rnd.Range(-0.06f, 0.02f), 0.0058f, -0.06f);
                while (button.transform.localPosition.z <= 0.06f)
                {
                    button.transform.localPosition += new Vector3(0f, 0f, 0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 1)
            {
                button.transform.localPosition = new Vector3(rnd.Range(-0.06f, 0.02f), 0.0058f, 0.06f);
                while (button.transform.localPosition.z >= -0.06f)
                {
                    button.transform.localPosition += new Vector3(0f, 0f, -0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 2)
            {
                button.transform.localPosition = new Vector3(-0.06f, 0.0058f, rnd.Range(-0.06f, 0.03f));
                while (button.transform.localPosition.x <= 0.06f)
                {
                    button.transform.localPosition += new Vector3(0.001f, 0f, 0f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 3)
            {
                button.transform.localPosition = new Vector3(0.06f, 0.0058f, rnd.Range(-0.06f, 0.03f));
                while (button.transform.localPosition.x >= -0.06f)
                {
                    button.transform.localPosition += new Vector3(-0.001f, 0f, 0f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 4)
            {
                button.transform.localPosition = new Vector3(0.035f, 0.0058f, 0.035f);
                while (button.transform.localPosition.x >= -0.06f && button.transform.localPosition.z >= -0.06f)
                {
                    button.transform.localPosition += new Vector3(-0.001f, 0f, -0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 5)
            {
                button.transform.localPosition = new Vector3(-0.06f, 0.0058f, -0.06f);
                while (button.transform.localPosition.x <= 0.035f && button.transform.localPosition.z <= 0.035f)
                {
                    button.transform.localPosition += new Vector3(0.001f, 0f, 0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 6)
            {
                button.transform.localPosition = new Vector3(-0.06f, 0.0058f, 0.06f);
                while (button.transform.localPosition.x <= 0.06f && button.transform.localPosition.z >= -0.06f)
                {
                    button.transform.localPosition += new Vector3(0.001f, 0f, -0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }
            else if (rotindex == 7)
            {
                button.transform.localPosition = new Vector3(0.06f, 0.0058f, -0.06f);
                while (button.transform.localPosition.x >= -0.06f && button.transform.localPosition.z <= 0.06f)
                {
                    button.transform.localPosition += new Vector3(-0.001f, 0f, 0.001f);
                    yield return new WaitForSeconds(0.001f);
                }
            }

        }
        button.gameObject.SetActive(false);
        button.transform.localPosition -= new Vector3(0f, 0.002f, 0f);
        yield return new WaitForSeconds(1f);
        if (!doin && !anim)
        {
            button.gameObject.SetActive(true);

        }
        if (!anim)
        { 
        StartCoroutine(colorC());
        }
    }
    
    
    IEnumerator Anim()
    {
        Debug.LogFormat("[Rebooting M-OS #{0}] Rebooting module with {1} setting", _moduleID, answer[0]);
        sfx.PlaySoundAtTransform("turnOff1", transform);
        button.gameObject.SetActive(false);
        surface.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f);
        yield return new WaitForSeconds(3.5f);
        sf.volume = .3f;
        sf.clip = TurnOn;
        sf.pitch = 1;
        sf.Play();
        yield return new WaitForSeconds(3f);
        surface.GetComponent<MeshRenderer>().material.color = new Color(.12f, .12f, .12f);
        yield return new WaitForSeconds(2f);
        
            systemtext[0].text = systxt[rnd.Range(0, 10)];
            yield return new WaitForSeconds(.05f);
            systemtext[1].text = systemtext[0].text;
            systemtext[0].text = systxt[rnd.Range(0, 10)];
            yield return new WaitForSeconds(.05f);
            systemtext[2].text = systemtext[1].text;
            systemtext[1].text = systemtext[0].text;
            systemtext[0].text = systxt[rnd.Range(0, 10)];         
            yield return new WaitForSeconds(.05f);
            systemtext[3].text = systemtext[2].text;
            systemtext[2].text = systemtext[1].text;
            systemtext[1].text = systemtext[0].text;
            systemtext[0].text = systxt[rnd.Range(0, 10)];
            yield return new WaitForSeconds(.05f);
            systemtext[4].text = systemtext[3].text;
            systemtext[3].text = systemtext[2].text;
            systemtext[2].text = systemtext[1].text;
            systemtext[1].text = systemtext[0].text;
            systemtext[0].text = systxt[rnd.Range(0, 10)];
            yield return new WaitForSeconds(.05f);
        for (int i = 30; i > 0; i--)
        {
            systemtext[4].text = systemtext[3].text;
            systemtext[3].text = systemtext[2].text;
            systemtext[2].text = systemtext[1].text;
            systemtext[1].text = systemtext[0].text;
            systemtext[0].text = systxt[rnd.Range(0, 10)];
            yield return new WaitForSeconds(.05f);
        }
        foreach (TextMesh sys in systemtext)
        {
            sys.text = "";
        }
        yield return new WaitForSeconds(3f);
        if (answer[0] != Final)
        {
            Debug.LogFormat("[Rebooting M-OS #{0}] Incorrect reboot setting! Fatal error appeared.", _moduleID);
            sf.Stop();
            sf.clip = buttonP;
            sf.volume = 1f;
            module.HandleStrike();
            sfx.PlaySoundAtTransform("Glitch", transform);
            for (int i=0; i<20; i++)
            {
                surface.GetComponent<MeshRenderer>().material.color = new Color(Areds[rnd.Range(0, 27)], Agreens[rnd.Range(0, 27)], Ablues[rnd.Range(0, 27)]);
                yield return new WaitForSeconds(0.1f);
            }
            surface.GetComponent<MeshRenderer>().material.color = new Color(.12f, .12f, .12f);
            answer[0] = 0;
            answer[1] = 0;
            anim = false;
            button.gameObject.SetActive(true);
            StartCoroutine(colorC());
        }
        else if(answer[0] == Final)
        {
            Debug.LogFormat("[Rebooting M-OS #{0}] Module rebooted, all fine.", _moduleID);
            sf.Stop();
            sf.clip = buttonP;
            sf.volume = 1f;
            sfx.PlaySoundAtTransform("Defused", transform);
            module.HandlePass();
            Logo.SetActive(true);
            StartCoroutine(cycle());
            
        }
    }
    IEnumerator cycle()
    {
        foreach (TextMesh welcome in welcome)
        {
            welcome.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        foreach (TextMesh welcome in welcome)
        {
            welcome.color = new Color(0f, 1f, 0f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        foreach (TextMesh welcome in welcome)
        {
            welcome.color = new Color(1f, 1f, 0f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        foreach (TextMesh welcome in welcome)
        {
            welcome.color = new Color(0f, 0f, 1f, 1f);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(cycle());
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "!{0} submit ### (to submit 3 numbers). !{0} press sl on # (to press status light at specific time). !{0} press sl (to press status light).";
#pragma warning restore 414
    private IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.ToLowerInvariant().Trim();
        var split = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (split.Length == 2 && split[0].StartsWith("submit") && split[1].Length == 3 && !anim)
        {
            var set = split.Skip(1).Join("");
            if (set.Any(letter => !letter.EqualsAny('0', '1', '2', '3', '4', '5', '6', '7', '8', '9')))
                yield break;
            tp = true;
            yield return null;
            int sed = Convert.ToInt32(set);
            tpdigit = sed;
            tpdigit = (sed - (sed % 100)) / 100;
            yield return new WaitForSeconds(0.1f);
            while (!button.isActiveAndEnabled)
            {
                yield return new WaitForSeconds(0.01f);
            }
            button.OnInteract();
            tpdigit = ((sed % 100) - (sed % 10)) / 10;
            yield return new WaitForSeconds(0.5f);
            while (!button.isActiveAndEnabled)
            {
                yield return new WaitForSeconds(0.01f);
            }
            button.OnInteract();
            tpdigit = sed % 10;
            yield return new WaitForSeconds(0.5f);
            while (!button.isActiveAndEnabled)
            {
                yield return new WaitForSeconds(0.01f);
            }
            button.OnInteract();
            tp = false;
        }
        else if (split.Length == 2 && split[0].StartsWith("press") && split[1].StartsWith("sl") && !anim)
        {
            yield return null;
            yield return new WaitForSeconds(0.1f);
            status.OnInteract();
        }
        else if (split.Length == 4 && split[0].StartsWith("press") && split[1].StartsWith("sl") && split[2].StartsWith("on") && split[3].Length == 1 && !anim)
        {
            yield return null;
            var exp = split.Skip(3).Join("");
            if (exp.Any(letter => !letter.EqualsAny('0', '1', '2', '3', '4', '5', '6', '7', '8', '9')))
                yield break;
            yield return new WaitForSeconds(0.1f);
            while (time != Convert.ToInt32(exp))
            {
                yield return new WaitForSeconds(0.1f);
            }
            status.OnInteract();
        }
    }
    }
