﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DimmingContol.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1920 * 1080")]
        public string WidthHeight {
            get {
                return ((string)(this["WidthHeight"]));
            }
            set {
                this["WidthHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>192.168.0.100</string>
  <string>192.168.0.101</string>
  <string>192.168.0.102</string>
  <string>192.168.0.103</string>
  <string>192.168.0.104</string>
  <string>192.168.0.105</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection IP {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["IP"]));
            }
            set {
                this["IP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>255.255.255.0</string>
  <string>255.255.255.0</string>
  <string>255.255.255.0</string>
  <string>255.255.255.0</string>
  <string>255.255.255.0</string>
  <string>255.255.255.0</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection SubMask {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["SubMask"]));
            }
            set {
                this["SubMask"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>192.168.0.1</string>
  <string>192.168.0.1</string>
  <string>192.168.0.1</string>
  <string>192.168.0.1</string>
  <string>192.168.0.1</string>
  <string>192.168.0.1</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Gateway {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Gateway"]));
            }
            set {
                this["Gateway"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>5000</string>
  <string>5000</string>
  <string>5000</string>
  <string>5000</string>
  <string>5000</string>
  <string>5000</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Port {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Port"]));
            }
            set {
                this["Port"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>정릉 터널</string>
  <string>홍지문 터널 통합</string>
  <string>홍지문 터널 문화촌</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ControllerName {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ControllerName"]));
            }
            set {
                this["ControllerName"] = value;
            }
        }
    }
}
