using ShadowrunInitTracker.Model;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for PhasePassListBox.xaml
    /// </summary>
    public partial class PassListView : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PassListView));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty InitiativePassProperty =
            DependencyProperty.Register("InitiativePass", typeof(InitiativePass), typeof(PassListView));
        public InitiativePass InitiativePass
        {
            get { return (InitiativePass)GetValue(InitiativePassProperty); }
            set { SetValue(InitiativePassProperty, value); }
        }

        public PassListView()
        {
            InitializeComponent();
        }
    }
}
