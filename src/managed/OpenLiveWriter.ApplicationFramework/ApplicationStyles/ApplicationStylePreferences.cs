// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Reflection;
using System.Diagnostics;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.ApplicationFramework;
using OpenLiveWriter.ApplicationFramework.Preferences ;

namespace OpenLiveWriter.ApplicationFramework.ApplicationStyles
{
    /// <summary>
    /// Appearance preferences.
    /// </summary>
    public class ApplicationStylePreferences : OpenLiveWriter.ApplicationFramework.Preferences.Preferences
    {
        #region Static & Constant Declarations

        /// <summary>
        /// The AppearancePreferences sub-key.
        /// </summary>
        private const string PREFERENCES_SUB_KEY = "Appearance";

        /// <summary>
        /// The ApplicationStyleTypeName key.
        /// </summary>
        private const string APPLICATION_STYLE_TYPE_NAME = "ApplicationStyleTypeName";

        #endregion Static & Constant Declarations

        #region Private Member Variables

        /// <summary>
        /// The ApplicationStyle Type.
        /// </summary>
        private Type applicationStyleType;

        #endregion

        #region Class Initialization & Termination

        /// <summary>
        /// Initializes a new instance of the AppearancePreferences class.
        /// </summary>
        public ApplicationStylePreferences(bool monitorChanges) : base(PREFERENCES_SUB_KEY, monitorChanges)
        {
        }

        public ApplicationStylePreferences() : this(false)
        {
        }

        #endregion Class Initialization & Termination

        #region Public Properties

        /// <summary>
        /// Gets or sets the ApplicationStyle Type.
        /// </summary>
        public Type ApplicationStyleType
        {
            get
            {
                return typeof(ApplicationStyleModern);

                // JJA: Decided to only support SkyBlue so we could make the
                // design of the sidebar more straightforward
                //return applicationStyleType;
            }
            set
            {
                if (applicationStyleType != value)
                {
                    applicationStyleType = value;
                    Modified();
                }
            }
        }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Loads preferences.
        /// </summary>
        protected override void LoadPreferences()
        {
            // Always use Modern (Editorial warm theme)
            applicationStyleType = typeof(ApplicationStyleModern);

            //	Set the new application style.
            if (ApplicationManager.ApplicationStyle.GetType() != applicationStyleType)
                ApplicationManager.ApplicationStyle = Activator.CreateInstance(applicationStyleType) as ApplicationStyle;
        }

        /// <summary>
        /// Saves preferences.
        /// </summary>
        protected override void SavePreferences()
        {
            string name = null;
            if ( applicationStyleType == typeof(ApplicationStyleSkyBlue) )
                name = "SkyBlue";
            else if ( applicationStyleType == typeof(ApplicationStyleLavender) )
                name = "Lavender";
            else if ( applicationStyleType == typeof(ApplicationStyleSienna) )
                name = "Sienna";
            else if ( applicationStyleType == typeof(ApplicationStyleSterling) )
                name = "Sterling";
            else if ( applicationStyleType == typeof(ApplicationStyleWintergreen) )
                name = "Wintergreen";
            else
            {
                Trace.Fail("Unexpected application style: " + applicationStyleType.Name);
                name = "SkyBlue";
            }

            SettingsPersisterHelper.SetString(APPLICATION_STYLE_TYPE_NAME, name);
        }

        #endregion Protected Methods
    }
}
