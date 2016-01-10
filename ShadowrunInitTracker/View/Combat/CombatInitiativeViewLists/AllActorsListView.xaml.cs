using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
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
    public partial class AllActorsListView : UserControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AllActorsListView));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty EntryCollectionProperty =
            DependencyProperty.Register("EntryCollection", typeof(InitiativeEntryCollection), typeof(AllActorsListView));
        public InitiativeEntryCollection EntryCollection
        {
            get { return (InitiativeEntryCollection)GetValue(EntryCollectionProperty); }
            set { SetValue(EntryCollectionProperty, value); }
        }

        public AllActorsListView()
        {
            InitializeComponent();
        }
    }
}
