﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OngekiFumenEditor.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
    public sealed partial class ProgramSetting : global::System.Configuration.ApplicationSettingsBase {
        
        private static ProgramSetting defaultInstance = ((ProgramSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ProgramSetting())));
        
        public static ProgramSetting Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\Dumps")]
        public string DumpFileDirPath {
            get {
                return ((string)(this["DumpFileDirPath"]));
            }
            set {
                this["DumpFileDirPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsFullDump {
            get {
                return ((bool)(this["IsFullDump"]));
            }
            set {
                this["IsFullDump"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsNotifyUserCrash {
            get {
                return ((bool)(this["IsNotifyUserCrash"]));
            }
            set {
                this["IsNotifyUserCrash"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UpgradeProcessPriority {
            get {
                return ((bool)(this["UpgradeProcessPriority"]));
            }
            set {
                this["UpgradeProcessPriority"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GraphicsCompatability {
            get {
                return ((bool)(this["GraphicsCompatability"]));
            }
            set {
                this["GraphicsCompatability"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OutputGraphicsLog {
            get {
                return ((bool)(this["OutputGraphicsLog"]));
            }
            set {
                this["OutputGraphicsLog"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool GraphicsLogSynchronous {
            get {
                return ((bool)(this["GraphicsLogSynchronous"]));
            }
            set {
                this["GraphicsLogSynchronous"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DisableShowSplashScreenAfterBoot {
            get {
                return ((bool)(this["DisableShowSplashScreenAfterBoot"]));
            }
            set {
                this["DisableShowSplashScreenAfterBoot"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool EnableMultiInstances {
            get {
                return ((bool)(this["EnableMultiInstances"]));
            }
            set {
                this["EnableMultiInstances"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsFirstTimeOpenEditor {
            get {
                return ((bool)(this["IsFirstTimeOpenEditor"]));
            }
            set {
                this["IsFirstTimeOpenEditor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WindowSizePositionLastTime {
            get {
                return ((string)(this["WindowSizePositionLastTime"]));
            }
            set {
                this["WindowSizePositionLastTime"] = value;
            }
        }
    }
}
