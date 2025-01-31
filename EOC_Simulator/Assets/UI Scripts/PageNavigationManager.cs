using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PageNavigationManager : MonoBehaviour
{
    private VisualElement mainMenu;
    private VisualElement choosingCharacter;
    private Dictionary<string, VisualElement> positionInformationPanels;

    private Button startButton;
    private Button backToMainMenuButton;
    private Dictionary<string, Button> characterButtons;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 获取 UI 元素
        mainMenu = root.Q<VisualElement>("MainMenu");
        choosingCharacter = root.Q<VisualElement>("ChoosingCharacter");

        // 初始化 Position 信息面板
        positionInformationPanels = new Dictionary<string, VisualElement>
        {
            { "EOC_Director", root.Q<VisualElement>("PositionInformation_EOCDirector") },
            { "Planning_Chief", root.Q<VisualElement>("PositionInformation_PlanningChief") },
            { "Operations_Chief", root.Q<VisualElement>("PositionInformation_OperationsChief") }
            // 可以继续扩展其他角色的信息面板
        };

        // 获取按钮
        startButton = root.Q<Button>("StartButton");
        backToMainMenuButton = root.Q<Button>("BackToMainMenu");

        characterButtons = new Dictionary<string, Button>
        {
            { "EOC_Director", root.Q<Button>("EOC_Director") },
            { "Planning_Chief", root.Q<Button>("Planning_Chief") },
            { "Operations_Chief", root.Q<Button>("Operations_Chief") }
        };

        // 绑定事件
        startButton.clicked += ShowChoosingCharacterPage;
        backToMainMenuButton.clicked += ShowMainMenu;

        foreach (var character in characterButtons)
        {
            character.Value.clicked += () => ShowInformationOfPosition(character.Key);
        }
    }

    private void ShowChoosingCharacterPage()
    {
        mainMenu.style.display = DisplayStyle.None;
        choosingCharacter.style.display = DisplayStyle.Flex;

        // 进入角色选择时隐藏所有 Position 信息
        foreach (var panel in positionInformationPanels.Values)
        {
            panel.style.display = DisplayStyle.None;
        }
    }

    private void ShowMainMenu()
    {
        choosingCharacter.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;
    }

    private void ShowInformationOfPosition(string characterName)
    {
        // 隐藏所有信息面板
        foreach (var panel in positionInformationPanels.Values)
        {
            panel.style.display = DisplayStyle.None;
        }

        // 显示当前角色的信息面板
        if (positionInformationPanels.ContainsKey(characterName))
        {
            positionInformationPanels[characterName].style.display = DisplayStyle.Flex;
        }
    }
}