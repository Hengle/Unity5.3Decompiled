﻿namespace UnityEditor.Audio
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEditor;
    using UnityEngine;

    public class AudioMixerEffectPlugin : IAudioEffectPlugin
    {
        internal AudioMixerController m_Controller;
        internal AudioMixerEffectController m_Effect;
        internal MixerParameterDefinition[] m_ParamDefs;

        public override bool GetFloatBuffer(string name, out float[] data, int numsamples)
        {
            this.m_Effect.GetFloatBuffer(this.m_Controller, name, out data, numsamples);
            return true;
        }

        public override bool GetFloatParameter(string name, out float value)
        {
            value = this.m_Effect.GetValueForParameter(this.m_Controller, this.m_Controller.TargetSnapshot, name);
            return true;
        }

        public override bool GetFloatParameterInfo(string name, out float minRange, out float maxRange, out float defaultValue)
        {
            foreach (MixerParameterDefinition definition in this.m_ParamDefs)
            {
                if (definition.name == name)
                {
                    minRange = definition.minRange;
                    maxRange = definition.maxRange;
                    defaultValue = definition.defaultValue;
                    return true;
                }
            }
            minRange = 0f;
            maxRange = 1f;
            defaultValue = 0.5f;
            return false;
        }

        public override int GetSampleRate()
        {
            return AudioSettings.outputSampleRate;
        }

        public override bool IsPluginEditableAndEnabled()
        {
            return (AudioMixerController.EditingTargetSnapshot() && !this.m_Effect.bypass);
        }

        public override bool SetFloatParameter(string name, float value)
        {
            this.m_Effect.SetValueForParameter(this.m_Controller, this.m_Controller.TargetSnapshot, name, value);
            return true;
        }
    }
}

