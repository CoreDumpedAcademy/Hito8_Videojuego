using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPlaySceneText : MonoBehaviour
{
    //Main screen static text
    public TextMeshProUGUI MainS_progressText;

    public TextMeshProUGUI Options_resetText;
    public TextMeshProUGUI Options_settingsText;
    public TextMeshProUGUI Options_saveAndExitButton;
    public TextMeshProUGUI Options_saveButton;    
    public TextMeshProUGUI Options_quitButton;

    public TextMeshProUGUI Options_resetPanel_attention;
    public TextMeshProUGUI Options_resetPanel_warning;
    public TextMeshProUGUI Options_resetPanel_prodSpeed;
    public TextMeshProUGUI Options_resetPanel_resetButton;
    public TextMeshProUGUI Options_resetPanel_backButton;

    public TextMeshProUGUI Store_storeButtonText;
    public TextMeshProUGUI Store_storeName;
    public TextMeshProUGUI Store_exitButton;
    public TextMeshProUGUI Store_buyButton;
    public TextMeshProUGUI Store_infoName;
    public TextMeshProUGUI Store_infoPrice;

    public TextMeshProUGUI BuffPanel_continueButton;

    public void LoadTextSource() {
        SPlayScene playSceneText = GameText.text.PlayScene;

        MainS_progressText.text = playSceneText.Game;

        SPlayScene.SOptions optionsText = playSceneText.Options;

        Options_resetText.text = optionsText.Reset;
        Options_settingsText.text = optionsText.Settings;
        Options_saveAndExitButton.text = optionsText.SaveAndQuit;
        Options_saveButton.text = optionsText.Save;
        Options_quitButton.text = optionsText.QuitGame;

        SPlayScene.SOptions.SResetPanel resetPanelText = optionsText.ResetPanel;

        Options_resetPanel_attention.text = resetPanelText.Attention;
        Options_resetPanel_warning.text = resetPanelText.WarningText;
        Options_resetPanel_prodSpeed.text = resetPanelText.ProductionSpeed;
        Options_resetPanel_resetButton.text = optionsText.Reset;
        Options_resetPanel_backButton.text = resetPanelText.GoBack;

        SPlayScene.SStore storePanelText = playSceneText.Store;

        Store_storeButtonText.text = storePanelText.StoreName;
        Store_exitButton.text = storePanelText.Exit;
        Store_buyButton.text = storePanelText.Buy;
        Store_infoName.text = storePanelText.NameIndicator;
        Store_infoPrice.text = storePanelText.PriceIndicator;

        SPlayScene.SBuffPanel buffPanelText = playSceneText.BuffPanel;

        BuffPanel_continueButton.text = buffPanelText.Continue;
    }
}
