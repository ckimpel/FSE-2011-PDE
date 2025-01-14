﻿using System.Collections.ObjectModel;

using Tum.PDE.LanguageDSL.Visualization.Commands;
using Tum.PDE.LanguageDSL.Visualization.ViewModel.Data;


namespace Tum.PDE.LanguageDSL.Visualization.ViewModel.Menu
{
    /// <summary>
    /// This class provides a view model for an ui menu item representation.
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        private string itemText;
        private string itemIconUrl;
        private ObservableCollection<MenuItemViewModel> itemChildren;
        private DelegateCommand itemCommand = null;
        private bool isEnabled = true;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="viewModelStore">The store this view model belongs to.</param>
        public MenuItemViewModel(ViewModelStore viewModelStore)
            : this(viewModelStore, "")
        {
        }

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="viewModelStore">The store this view model belongs to.</param>
        /// <param name="text">Text of the menu item.</param>
        public MenuItemViewModel(ViewModelStore viewModelStore, string text)
            : this(viewModelStore, text, null)
        {
        }

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="viewModelStore">The store this view model belongs to.</param>
        /// <param name="text">Text of the menu item.</param>
        /// <param name="iconUrl">Icon of the menu item.</param>
        public MenuItemViewModel(ViewModelStore viewModelStore, string text, string iconUrl)
            : base(viewModelStore)
        {
            itemText = text;
            itemIconUrl = iconUrl;
            itemChildren = new ObservableCollection<MenuItemViewModel>();
        }

        /// <summary>
        /// Gets or sets the text to display in the menu item.
        /// </summary>
        public string Text
        {
            get { return this.itemText; }
            set 
            { 
                this.itemText = value;
                OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the url of the icon to display in the menu item. Can be null.
        /// </summary>
        public string IconUrl
        {
            get { return this.itemIconUrl; }
            set
            {
                this.itemIconUrl = value;
                OnPropertyChanged("IconUrl");
            }
        }

        /// <summary>
        /// Gets or sets whether this menu item is enabled or not. True by default.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Gets the child items of this menu item.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> Children
        {
            get
            {
                return itemChildren;
            }
        }

        /// <summary>
        /// Gets or set command of this menu item.
        /// </summary>
        public DelegateCommand Command
        {
            get { return itemCommand; }
            set 
            { 
                itemCommand = value; 
            }
        }
    }
}
