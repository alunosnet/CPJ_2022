using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
/// <summary>
/// Player data prefs and online scores
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    //singleton
    public static PlayerDataManager instance;
    
    public Highscore[] highscoresList;
    public Text[] txtPontuacoes;

    public int XP;
    public int creditos;


    const string privateCode = "gkfb_AIkX0eoykmH3HCZaQhgJFx1S6rEuDld6iave2yA";
    const string publicCode = "62602de18f40bbb378cda400";
    const string webURL = "http://dreamlo.com/lb/";
    public struct Highscore
    {
        public string username;
        public int xp;

        public Highscore(string _username, int _xp)
        {
            username = _username;
            xp = _xp;
        }
    }
    void OnEnable(){
        loadCreditos();
        loadXP();
    }
    void OnDisable(){
        saveCreditos();
        saveXP();
    }
    void Awake()
    {
        //repetido
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void EncontraObjetoMensagens() {
        

    }

    void Start()
    {
        DownloadHighscores();
        EncontraObjetoMensagens();
    }
    public bool nomeExiste(string nome)
    {
        foreach (Highscore hg in highscoresList)
        {
            if (hg.username == nome) return true;
        }
        return false;
    }

    #region onlineScores

    public void AddNewHighscore(string username, int xp)
    {
        if (username == "-") return;
        StartCoroutine(UploadNewHighscoreV2(username, xp));
    }
   
    //Descontinuado
    IEnumerator UploadNewHighscore(string username, int xp)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + xp);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error uploading: " + www.error);
        }
    }
    IEnumerator UploadNewHighscoreV2(string username, int xp)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + xp);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError == false)
            print("Upload Successful: "+username + " - "+xp);
        else
        {
            print("Error uploading: " + webRequest.error);
        }
    }    
   
    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabaseV2");
    }
    IEnumerator DownloadHighscoresFromDatabaseV2()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
        //WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return webRequest.SendWebRequest();

        
        if (webRequest.isNetworkError==false)
        {
            FormatHighscores(webRequest.downloadHandler.text);
            if (txtPontuacoes != null && txtPontuacoes.Length > 0)
                preencheTXT();
        }
        else
        {
            print("Error Downloading: " + webRequest.error);
        }
    }
    //Descontinuado
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            if (txtPontuacoes != null && txtPontuacoes.Length > 0)
                preencheTXT();
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }
    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            if (entryInfo.Length < 2) break;
            string username = entryInfo[0];
            int xp = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, xp);
            //print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }
    public void preencheTXT()
    {
        for (int i = 0; i < txtPontuacoes.Length; i++)
        {
            if (i < highscoresList.Length)
                txtPontuacoes[i].text = highscoresList[i].username + " € " + highscoresList[i].xp;
            else
                txtPontuacoes[i].text = "-";
        }
    }
    public void deleteHighscore(string nome)
    {
        StartCoroutine(iDeleteHighscoreV2(nome));
    }
    IEnumerator iDeleteHighscoreV2(string username)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(webURL + privateCode + "/delete/" + UnityWebRequest.EscapeURL(username));
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError==false)
            print("Delete Successful");
        else
            print("Error deleting: " + webRequest.error);
    }
    //Descontinuado
    IEnumerator iDeleteHighscore(string username)
    {
        WWW www = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(username));
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Delete Successful");
        else
            print("Error deleting: " + www.error);
    }
    #endregion
    #region playerData
    public void setNome(string nome)
    {
        PlayerPrefs.SetString("nome", nome);
        PlayerPrefs.Save();
    }
    public string getNome()
    {
        return PlayerPrefs.GetString("nome", string.Empty);
    }
    public void saveXP()
    {
        PlayerPrefs.SetInt("xp", XP);
        PlayerPrefs.Save();
    }
    public void loadXP()
    {
        XP=PlayerPrefs.GetInt("xp",0);
    }
    public int getXP(){
        return XP;
    }
    public void resetXP() {
        //Debug.Log("Reseting xp to zero!");
        PlayerPrefs.SetInt("xp", 0);
        PlayerPrefs.Save();
        
    }
    public void addXP(int value){
        XP += value;
        //mostrar o XP ganho
       
        SistemaMensagens.instance.ShowMessage(String.Format("{0} xp", value.ToString()));
 
    }
    public void saveCreditos(){
        PlayerPrefs.SetInt("creditos",creditos);
        PlayerPrefs.Save();
    }
    public void loadCreditos(){
        creditos=PlayerPrefs.GetInt("creditos",0);
    }
    public void addCreditos(int value){
        creditos+=value;
    }
    public void setUltimaArena(int id)
    {
        PlayerPrefs.SetInt("ultimaarena", id);
        PlayerPrefs.Save();
    }
    public void getUltimaArena()
    {
        PlayerPrefs.GetInt("ultimaarena", 0);
    }
    
    #endregion
}
