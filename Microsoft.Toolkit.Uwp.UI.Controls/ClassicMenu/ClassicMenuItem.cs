﻿using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Microsoft.Toolkit.Uwp.UI.Controls
{
    /// <summary>
    /// Classic Menu Item is the items main container for Class Menu control
    /// </summary>
    public class ClassicMenuItem : ItemsControl
    {
        /// <summary>
        /// ClassMenuItem header text
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(ClassicMenuItem), new PropertyMetadata(default(string)));

        private ClassicMenu ParentMenu => this.FindAscendant<ClassicMenu>();

        private Button _flyoutButton;

        /// <summary>
        /// Gets or sets the title to appear in the title bar
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassicMenuItem"/> class.
        /// </summary>
        public ClassicMenuItem()
        {
            DefaultStyleKey = typeof(ClassicMenuItem);
            IsFocusEngagementEnabled = true;
        }

        /// <summary>
        /// this method is used to show the menu for current item
        /// </summary>
        public void ShowMenu()
        {
            _flyoutButton?.Flyout?.ShowAt(_flyoutButton);
        }

        /// <inheritdoc />
        protected override void OnApplyTemplate()
        {
            _flyoutButton = GetTemplateChild("FlyoutButton") as Button;

            if (_flyoutButton != null && Items != null && Items.Any())
            {
                var menuFlyout = new MenuFlyout();
                menuFlyout.Placement = ParentMenu.Orientation == Orientation.Horizontal
                    ? FlyoutPlacementMode.Bottom
                        : FlyoutPlacementMode.Right;

                foreach (var item in Items)
                {
                    var menuItem = item as MenuFlyoutItemBase;
                    if (menuItem != null)
                    {
                        menuFlyout.Items?.Add(menuItem);
                    }
                }

                _flyoutButton.Flyout = menuFlyout;
            }

            base.OnApplyTemplate();
        }
    }
}
