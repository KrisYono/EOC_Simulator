using UnityEngine;
using UnityEngine.UIElements;

public class PageNavigationManager : MonoBehaviour
{
    private VisualElement mainMenu;
    private VisualElement choosingCharacter;
    private VisualElement positionInformation_EocDirector;

    private Button startButton;
    private Button backToMainMenuButton;
    private Button eocDirector;

    void Start()
    {
        // 获取根 VisualElement
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 获取 Main Menu 和 Choosing Character 容器
        mainMenu = root.Q<VisualElement>("MainMenu");
        choosingCharacter = root.Q<VisualElement>("ChoosingCharacter");
        positionInformation_EocDirector = root.Q<VisualElement>("PositionInformation_EOCDirector");

        // 获取按钮
        startButton = root.Q<Button>("StartButton");
        backToMainMenuButton = root.Q<Button>("BackToMainMenu");
        eocDirector = root.Q<Button>("EOC_Director");

        // 为按钮绑定点击事件
        startButton.clicked += ShowChoosingCharacterPage;
        backToMainMenuButton.clicked += ShowMainMenu;
        eocDirector.clicked += ShowInformationOfPosition;
    }

    private void ShowChoosingCharacterPage()
    {
        // 隐藏 Main Menu，显示 Choosing Character 页面
        mainMenu.style.display = DisplayStyle.None;
        choosingCharacter.style.display = DisplayStyle.Flex;
        positionInformation_EocDirector.style.display = DisplayStyle.None;
    }

    private void ShowMainMenu()
    {
        // 隐藏 Choosing Character 页面，显示 Main Menu
        choosingCharacter.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;
    }

    private void ShowInformationOfPosition()
    {
        positionInformation_EocDirector.style.display = DisplayStyle.Flex;
    }
}