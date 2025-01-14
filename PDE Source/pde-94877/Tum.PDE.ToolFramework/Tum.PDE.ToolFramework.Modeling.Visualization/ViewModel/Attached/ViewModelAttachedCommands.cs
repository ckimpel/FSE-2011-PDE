﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tum.PDE.ToolFramework.Modeling.Visualization.ViewModel.Attached
{
    /// <summary>
    /// This class is used to provide attached commands for base view models.
    /// 
    /// Supported Commands: Activated (whenever a mouse click occurs in the view), Loaded, Unloaded. (Loaded and Unloaded for UserControl)
    /// </summary>
    /// <example>
    /// <![CDATA[ vma:ViewModelAttachedCommands.IsActivated="{Binding ActivatedCommand}"]]>
    /// </example>
    /// <remarks>
    /// Basen on Cinch framework by Sacha Barber: http://cinch.codeplex.com/
    /// </remarks>
    public class ViewModelAttachedCommands
    {       
        #region Activated
        /// <summary>
        /// Dependency property which holds the ICommand for the IsActivated event
        /// </summary>
        public static readonly DependencyProperty ActivatedProperty =
            DependencyProperty.RegisterAttached("Activated", typeof(ICommand), typeof(ViewModelAttachedCommands),
                new UIPropertyMetadata(null, OnActivatedPropertyPropertyEventInfoChanged));

        /// <summary>
        /// Attached Property getter to retrieve the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <returns>ICommand</returns>
        public static ICommand GetActivated(DependencyObject source)
        {
            return (ICommand)source.GetValue(ActivatedProperty);
        }

        /// <summary>
        /// Attached Property setter to change the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <param name="command">ICommand</param>
        public static void SetActivated(DependencyObject source, ICommand command)
        {
            source.SetValue(ActivatedProperty, command);
        }

        /// <summary>
        /// This is the property changed handler for the IsActivated property.
        /// </summary>
        /// <param name="sender">Dependency Object</param>
        /// <param name="e">EventArgs</param>
        private static void OnActivatedPropertyPropertyEventInfoChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                // is there a better solution???

                element.PreviewMouseDown -= OnElementActivated;
                if (e.NewValue != null)
                    element.PreviewMouseDown += new MouseButtonEventHandler(OnElementActivated);
            }
        }

        private static void OnElementActivated(object sender, MouseButtonEventArgs e)
        {
            var dpo = (DependencyObject)sender;
            ICommand activatedCommand = GetActivated(dpo);
            if (activatedCommand != null)
                activatedCommand.Execute(GetCommandParameter(dpo));
        }
        #endregion

        #region Loaded
        /// <summary>
        /// Dependency property which holds the ICommand for the Loaded event
        /// </summary>
        public static readonly DependencyProperty LoadedProperty =
            DependencyProperty.RegisterAttached("Loaded",
                typeof(ICommand), typeof(ViewModelAttachedCommands),
                    new UIPropertyMetadata(null, OnLoadedEventInfoChanged));

        /// <summary>
        /// Attached Property getter to retrieve the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <returns>ICommand</returns>
        public static ICommand GetLoaded(DependencyObject source)
        {
            return (ICommand)source.GetValue(LoadedProperty);
        }

        /// <summary>
        /// Attached Property setter to change the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <param name="command">ICommand</param>
        public static void SetLoaded(DependencyObject source, ICommand command)
        {
            source.SetValue(LoadedProperty, command);
        }

        /// <summary>
        /// This is the property changed handler for the Loaded property.
        /// </summary>
        /// <param name="sender">Dependency Object</param>
        /// <param name="e">EventArgs</param>
        private static void OnLoadedEventInfoChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var uc = sender as UserControl;
            if (uc != null)
            {
                uc.Loaded -= OnUserControlLoaded;
                if (e.NewValue != null)
                {
                    uc.Loaded += OnUserControlLoaded;
                    // Workaround: depending on the properties of the element, it's possible the Loaded event was already raised
                    // This happens when the View is created before the ViewModel is applied to the DataContext.  In this
                    // case, raise the Loaded event as soon as we know about it.
                    if (uc.IsLoaded)
                        OnUserControlLoaded(sender, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// This is the handler for the Loaded event to raise the command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnUserControlLoaded(object sender, EventArgs e)
        {
            var dpo = (DependencyObject)sender;
            ICommand loadedCommand = GetLoaded(dpo);
            if (loadedCommand != null)
                loadedCommand.Execute(GetCommandParameter(dpo));
        }
        #endregion

        #region Unloaded
        /// <summary>
        /// Dependency property which holds the ICommand for the Unloaded event
        /// </summary>
        public static readonly DependencyProperty UnloadedProperty =
            DependencyProperty.RegisterAttached("Unloaded",
                typeof(ICommand), typeof(ViewModelAttachedCommands),
                    new UIPropertyMetadata(null, OnUnloadedEventInfoChanged));

        /// <summary>
        /// Attached Property getter to retrieve the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <returns>ICommand</returns>
        public static ICommand GetUnloaded(DependencyObject source)
        {
            return (ICommand)source.GetValue(UnloadedProperty);
        }

        /// <summary>
        /// Attached Property setter to change the ICommand
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <param name="command">ICommand</param>
        public static void SetUnloaded(DependencyObject source, ICommand command)
        {
            source.SetValue(UnloadedProperty, command);
        }

        /// <summary>
        /// This is the property changed handler for the Unloaded property.
        /// </summary>
        /// <param name="sender">Dependency Object</param>
        /// <param name="e">EventArgs</param>
        private static void OnUnloadedEventInfoChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            var uc = sender as UserControl;

            if (uc != null)
            {
                uc.Unloaded -= OnUserControlUnloaded;
                if (e.NewValue != null)
                    uc.Unloaded += OnUserControlUnloaded;
            }
        }

        /// <summary>
        /// This is the handler for the Unloaded event to raise the command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnUserControlUnloaded(object sender, EventArgs e)
        {
            var dpo = (DependencyObject)sender;
            ICommand deactivatedCommand = GetUnloaded(dpo);
            if (deactivatedCommand != null)
                deactivatedCommand.Execute(GetCommandParameter(dpo));
        }
        #endregion

        #region CommandParameter
        /// <summary>
        /// Parameter for the ICommand
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(ViewModelAttachedCommands), new UIPropertyMetadata(null));

        /// <summary>
        /// This retrieves the CommandParameter used for the command.
        /// </summary>
        /// <param name="source">Dependency object</param>
        /// <returns>Command Parameter passed to ICommand</returns>
        public static object GetCommandParameter(DependencyObject source)
        {
            return source.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// This changes the CommandParameter used with this command.
        /// </summary>
        /// <param name="source">Dependency Object</param>
        /// <param name="value">New Value</param>
        public static void SetCommandParameter(DependencyObject source, object value)
        {
            source.SetValue(CommandParameterProperty, value);
        }
        #endregion
    }
}
