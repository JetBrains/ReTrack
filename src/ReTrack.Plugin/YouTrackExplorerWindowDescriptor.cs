using JetBrains.ReSharper.Features.Altering.Resources;
using JetBrains.ReSharper.Features.Finding.Resources;
using JetBrains.UI.Application;

namespace ReTrack
{
    using System.Drawing;
    using System.Windows.Forms;
    using JetBrains.Application;
    using JetBrains.DataFlow;
    using JetBrains.ProjectModel;
    using JetBrains.UI.Controls;
    using JetBrains.UI.Extensions;
    using JetBrains.UI.RichText;
    using JetBrains.UI.ToolWindowManagement;
    using JetBrains.Util;

    [ToolWindowDescriptor(
        ProductNeutralId = "YouTrackExplorer",
        Text = "YouTrack Explorer",
        VisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Solution,
        Type = ToolWindowType.SingleInstance,
        Icon = typeof(FeaturesFindingThemedIcons.SearchOptionsPage))] // TODO added an icon so R# doesn't crash on us, has to be replaced with a fancy one
    public class YouTrackExplorerWindowDescriptor : ToolWindowDescriptor
    {
        public YouTrackExplorerWindowDescriptor(IApplicationDescriptor applicationDescriptor)
            : base(applicationDescriptor)
        {
        }
    }

    [SolutionComponent]
    public class YouTrackExplorerWindowRegistrar
    {
        private readonly Lifetime lifetime;
        private readonly ToolWindowClass toolWindowClass;
        private readonly IUIApplication application;

        public YouTrackExplorerWindowRegistrar(Lifetime lifetime, ToolWindowManager toolWindowManager,
            YouTrackExplorerWindowDescriptor descriptor, IUIApplication application)
        {
            this.lifetime = lifetime;
            this.application = application;

            toolWindowClass = toolWindowManager.Classes[descriptor];
            toolWindowClass.RegisterEmptyContent(
                lifetime,
                lt =>
                {
                    // initialize the default ('empty') content for the tool window
                    var label = new RichTextLabel(application) {BackColor = SystemColors.Control, Dock = DockStyle.Fill};
                    label.RichTextBlock.Add(new RichText("Nothing Here", new TextStyle(FontStyle.Bold)));
                    label.RichTextBlock.Parameters = new RichTextBlockParameters(8, ContentAlignment.MiddleCenter);
                    return label.BindToLifetime(lt);
                });
        }


        public void Show()
        {
            ToolWindowInstance instance = toolWindowClass.RegisterInstance(
                lifetime,
                StringUtil.MakeTitle("YouTrack Explorer"), // title of your window; tip: StringUtil.MakeTitle
                null, // return a System.Drawing.Image to be displayed
                (lt, twi) =>
                {
                    var label = new RichTextLabel(application) {BackColor = SystemColors.Control, Dock = DockStyle.Fill};
                    label.RichTextBlock.Add(new RichText("My Content", new TextStyle(FontStyle.Bold)));
                    label.RichTextBlock.Parameters = new RichTextBlockParameters(8, ContentAlignment.MiddleCenter);
                    return label.BindToLifetime(lt);
                });
            instance.EnsureControlCreated().Show();
        }
    }
}