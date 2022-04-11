using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Uchievements
{
    public class UchievementView
    {
        public Label Label;
        public Image Icon;
        public ProgressBar ProgressBar;

        public UchievementView(VisualElement tree)
        {
            Label = tree.Q<Label>("label");
            Icon = tree.Q<Image>("image");
            ProgressBar = tree.Q<ProgressBar>("progress");
        }

        public void Show(UchievementData data)
        {
            Label.text = data.Name;

            var level = UchievementUtils.GetLevel(data);
            Icon.image = UchievementsResourcesLoader.GetIcon(level);

            var maxProgress = data.GetPointsForReward();
            var progressbarLabel = data.GetLabelForProgressbar();
            ProgressBar.title = progressbarLabel;
            ProgressBar.value = data.Progress / maxProgress*100;
        }


        public void HideProgressBar()
        {
            ProgressBar.style.visibility = Visibility.Hidden;
        }
    }
}