using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour, IDataPersistence
{
    private InputManager inputManager;
    public Resolution[] resolutions;
    public int resIndex;

    public int[] sizes;
    public string[] sizeEngTexts;
    public string[] sizeNLTexts;
    public int sizeIndex;
    public string[] languages;
    public int languageIndex;

    public bool askingInput = false;
    public float sensValueUIDelay = 2f;
    public string curName;
    
    public Resolution curRes;
    public int resWidth;
    public int resHeight;
    public bool english;
    public bool dutch;
    public bool subtitles;
    public bool vSync;
    public bool game;
    public bool control;

    [Header("Other Settings")]
    public bool fullScreen;
    public bool pauseMenu = false;

    [Header("Menu's")]
    public GameObject mainSettings;
    public GameObject controlSettings;
    public GameObject inputMenu;
    public GameObject settings;

    [Header("UI objects")]
    public Slider sensXSlider;
    public Slider sensYSlider;
    public TextMeshProUGUI resolutionUI;
    public Toggle fullScreenToggle;
    public Toggle vSyncToggle;
    public Toggle subtitlesToggle;
    public TextMeshProUGUI sizeUI;
    public TextMeshProUGUI languageUI;
    public TextMeshProUGUI curText;
    
    

    [Header("Text (for languages)")]
    //Main Settings
    public TextMeshProUGUI gameText;
    public TextMeshProUGUI sensXText;
    public TextMeshProUGUI sensYText;
    public TextMeshProUGUI resText;
    public TextMeshProUGUI fullScreenText;
    public TextMeshProUGUI vSyncText;
    public TextMeshProUGUI subtitlesText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI languageText;

    //Control Settings
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI forwardText;
    public TextMeshProUGUI backwardText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI sprintText;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI switchClothText;
    public TextMeshProUGUI clothesOnText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI saveText;

    private void Awake() {
        //resolutions stuff
        resolutions = Screen.resolutions;


        //checks if the scene is Main Menu, if not. The settings menu will be a pause menu
        if(SceneManager.GetActiveScene().name != "MainMenu") {
            pauseMenu = false;
        }
    }

    private void Start() {
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {
        if(askingInput && inputMenu != null) {
            if(Input.anyKeyDown) {
                KeyCode curKey = checkKey();
                int? curNum = isNumber(curKey);
                
                for(int i = 0; i < inputManager.keyNames.Length; i++) {
                    if(inputManager.keyNames[i] == curName) {
                        inputManager.keys[i] = curKey;
                    }
                }
                inputManager.UpdateKeys();

                if(curNum == null) {
                    curText.text = curKey.ToString();
                } else { curText.text = curNum.ToString(); }
                
            }
        }

        resWidth = curRes.width;
        resHeight = curRes.height;
    }
    
    private KeyCode checkKey() {
        //check each key possible to see which key was just pressed
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode)) {
                askingInput = false;
                inputMenu.SetActive(false);
                settings.SetActive(true);
                Debug.Log("Key clicked: " + kcode);
                return kcode;    
            } 
        }   
        return KeyCode.None; 
    }
    private int? isNumber(KeyCode key) {
        //check if the key pressed is a number
        for(int i = 48; i < 58; i++) {
            if((KeyCode)i == key) {
                if(i != 58) {
                    return i - 48;
                } else {
                    return 0;
                }
            }
        } 
        return null;
    }

    public void SetFullScreen(Toggle toggle) {
        fullScreen = toggle.isOn;
        Screen.fullScreen = fullScreen;
        GameManager.instance.fullScreen = fullScreen;
    }

    public void SetVSync(Toggle toggle) {
        if(toggle.isOn) {
            QualitySettings.vSyncCount = 1;
            vSync = true;
        } else {
            QualitySettings.vSyncCount = 0;
            vSync = false;
        }
    }

    public void SetSubtitles(Toggle toggle) {
        subtitles = toggle.isOn;
    }

    public void ChangeSens(Slider slider) {
        if(slider.name =="SensX Slider") {
            GameManager.instance.inputManager.sensX = slider.value;
        } else if(slider.name == "SensY Slider") {
            GameManager.instance.inputManager.sensY = slider.value;
        }
        curText = slider.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        curText.enabled = true;
        curText.text = Mathf.Round(slider.value).ToString();
        StartCoroutine(dissapearText(curText, sensValueUIDelay));
    }

    public void SetRes(string dir) {
        if(dir == "Left") {
            resIndex--;
            if(resIndex < 0) {
                resIndex = 0;
            } 
        } else if(dir == "Right") {
            resIndex++;
            if(resIndex > resolutions.Length - 1) {
                resIndex = resolutions.Length - 1;
            }
        }

        resolutionUI.text = resolutions[resIndex].width + " x " + resolutions[resIndex].height;
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, fullScreen);
        curRes = Screen.currentResolution;
        GameManager.instance.curRes = Screen.currentResolution;
    }

    public void SetSize(string dir) {
        if(dir == "Left") {
            sizeIndex--;
            if(sizeIndex < 0) {
                sizeIndex = 0;
            } 
        } else if(dir == "Right") {
            sizeIndex++;
            if(sizeIndex > sizes.Length - 1) {
                sizeIndex = sizes.Length - 1;
            }
        }

        sizeUI.text = sizeEngTexts[sizeIndex].ToString();
        GameManager.instance.subtitleSize = sizes[sizeIndex];
    }

    public void SetLanguage(string dir) {
        if(dir == "Left") {
            languageIndex--;
            if(languageIndex < 0) {
                languageIndex = 0;
            } 
        } else if(dir == "Right") {
            languageIndex++;
            if(languageIndex > languages.Length - 1) {
                languageIndex = languages.Length - 1;
            }
        }

        languageUI.text = languages[languageIndex];
        GameManager.instance.language = languages[languageIndex];
        UpdateMenu();
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }

    public void askInput(string name) {
        askingInput = true;
        curName = name;
        inputMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void SaveGame() {
        game = true;
        control = false;
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
    }

    IEnumerator dissapearText(TextMeshProUGUI curDissapearText, float delay) {
        yield return new WaitForSeconds(delay);
        curDissapearText.enabled = false;
    }
    
    public void UpdateMenu() {
        if(GameManager.instance.language == "English") {
            //main settings
            gameText.text = "Game";
            sensXText.text = "Mouse Sensitivity X";
            sensYText.text = "Mouse Sensitivity Y";
            resText.text = "Resolution";
            fullScreenText.text = "Fullscreen";
            vSyncText.text = "V-Sync";
            subtitlesText.text = "Subtitles";
            sizeText.text = "Size";
            sizeUI.text = sizeEngTexts[sizeIndex];
            languageText.text = "Language";
            languageUI.text = "English";

            //control settings
            controlsText.text = "Controls";
            forwardText.text = "Forward:";
            backwardText.text = "Backward:";
            leftText.text = "Left: ";
            rightText.text = "Right: ";
            sprintText.text = "Sprint:";
            pauseText.text = "Pause:";
            switchClothText.text = "switch Clothes:" ;
            clothesOnText.text = "Clothes On:";
            interactText.text = "Interact:";

        } else if(GameManager.instance.language == "Nederlands") {
            //Main Settings
            gameText.text = "Spel";
            sensXText.text = "Muis Sensitivity X";
            sensYText.text = "Muis Sensitivity Y";
            resText.text = "Resolutie";
            fullScreenText.text = "Fullscreen";
            vSyncText.text = "V-Sync";
            subtitlesText.text = "Ondertiteling";
            sizeText.text = "Grootte";
            sizeUI.text = sizeNLTexts[sizeIndex];
            languageText.text = "Taal";
            languageUI.text = "Nederlands";

            //Control Settings
            controlsText.text = "Controls";
            forwardText.text = "Voor:";
            backwardText.text = "Achter:";
            leftText.text = "Links:";
            rightText.text = "Rechts:";
            sprintText.text = "Ren:";
            pauseText.text = "Pauseer:";
            switchClothText.text = "Wissel kleren:";
            clothesOnText.text = "Kleren aan:";
            interactText.text = "Interactie:";
        }

        sensXSlider.value = GameManager.instance.inputManager.sensX;
        sensYSlider.value = GameManager.instance.inputManager.sensY;
        fullScreenToggle.isOn = fullScreen;
        vSyncToggle.isOn = vSync;
        subtitlesToggle.isOn = subtitles;
        resolutionUI.text = resolutions[resIndex].width.ToString() + " x " + resolutions[resIndex].height.ToString(); 
    }

    public void GameSettings(Button other) {

        if(!game) {
            Button but = controlsText.transform.parent.GetComponent<Button>();
            mainSettings.SetActive(true);
            controlSettings.SetActive(false);
            game = true;
            control = false;

            Color darkgray = new Color(0.1411765f, 0.1411765f, 0.1411765f, 1f);
            var colors = other.colors;
            colors.normalColor = darkgray;
            colors.highlightedColor = darkgray;
            colors.pressedColor = darkgray;
            colors.selectedColor = darkgray;
            other.colors = colors;


            colors = but.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(245f, 245f, 245f, 255f);
            colors.pressedColor = new Color(200f, 200f, 200f, 255f);
            colors.selectedColor = new Color(245f, 245f, 245f, 255f);
            but.colors = colors;

            controlsText.color = Color.black;
            gameText.color = Color.white;
        }
    }

    public void ControlSettings(Button other) {

        if(!control) {
            Button but = gameText.transform.parent.GetComponent<Button>();
            controlSettings.SetActive(true);
            mainSettings.SetActive(false);
            control = true;
            game = false;

            Color darkgray = new Color(0.1411765f, 0.1411765f, 0.1411765f, 1f);
            var colors = other.colors;
            colors.normalColor = darkgray;
            colors.highlightedColor = darkgray;
            colors.pressedColor = darkgray;
            colors.selectedColor = darkgray;
            other.colors = colors;


            colors = but.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(245f, 245f, 245f, 255f);
            colors.pressedColor = new Color(200f, 200f, 200f, 255f);
            colors.selectedColor = new Color(245f, 245f, 245f, 255f);
            but.colors = colors;

            controlsText.color = Color.white;
            gameText.color = Color.black;
        }
    }

    public void LoadData(GameData data) {
        this.subtitles = data.subtitles;
        this.curRes.width = data.resolutionWidth;
        this.curRes.height = data.resolutionHeight;
        this.fullScreen = data.fullScreen;
        this.vSync = data.vSync;
        
        if (vSync) 
        { QualitySettings.vSyncCount = 1; } 
        else { QualitySettings.vSyncCount = 0; }

        if(curRes.width == 0 && curRes.height == 0) {
            curRes = Screen.currentResolution;

        } else {
            Screen.SetResolution(curRes.width, curRes.height, fullScreen);
        } 
        for(int i = 0; i < resolutions.Length; i++) {
            if(resolutions[i].width == curRes.width && resolutions[i].height == curRes.height) {
                resIndex = i;
            }
        }

        for(int i = 0; i < sizes.Length; i++) {
            if(sizes[i] == data.subtitleSize) {
                sizeIndex = i;
            }
        }

        Invoke("UpdateMenu", 0.1f);
    }

    public void SaveData(ref GameData data) {
        data.subtitles = this.subtitles;
        data.resolutionWidth = curRes.width;
        data.resolutionHeight = curRes.height;
        data.fullScreen = this.fullScreen;
        data.vSync = this.vSync;
    }
}
