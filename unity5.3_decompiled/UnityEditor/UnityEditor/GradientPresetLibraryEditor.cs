﻿namespace UnityEditor
{
    using System;
    using UnityEngine;

    [CustomEditor(typeof(GradientPresetLibrary))]
    internal class GradientPresetLibraryEditor : Editor
    {
        private GenericPresetLibraryInspector<GradientPresetLibrary> m_GenericPresetLibraryInspector;

        public void OnDestroy()
        {
            if (this.m_GenericPresetLibraryInspector != null)
            {
                this.m_GenericPresetLibraryInspector.OnDestroy();
            }
        }

        private void OnEditButtonClicked(string libraryPath)
        {
            GradientPicker.Show(new Gradient());
            GradientPicker.instance.currentPresetLibrary = libraryPath;
        }

        public void OnEnable()
        {
            this.m_GenericPresetLibraryInspector = new GenericPresetLibraryInspector<GradientPresetLibrary>(this.target, "Gradient Preset Library", new Action<string>(this.OnEditButtonClicked));
            this.m_GenericPresetLibraryInspector.presetSize = new Vector2(72f, 16f);
            this.m_GenericPresetLibraryInspector.lineSpacing = 4f;
        }

        public override void OnInspectorGUI()
        {
            this.m_GenericPresetLibraryInspector.itemViewMode = PresetLibraryEditorState.GetItemViewMode("Gradient");
            if (this.m_GenericPresetLibraryInspector != null)
            {
                this.m_GenericPresetLibraryInspector.OnInspectorGUI();
            }
        }
    }
}

