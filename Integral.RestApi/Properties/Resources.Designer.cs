﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Integral.RestApi.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Integral.RestApi.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Guide {
            get {
                object obj = ResourceManager.GetObject("Guide", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;html class=&quot;main&quot; lang=&quot;en&quot;&gt;
        ///    &lt;head&gt;
        ///        &lt;title&gt;Integral-Homepage&lt;/title&gt;
        ///        &lt;link rel=&quot;stylesheet&quot; href=&quot;styles/style.css&quot;&gt;
        ///    &lt;/head&gt;
        ///
        ///    &lt;body&gt;
        ///        &lt;div class=&quot;main&quot;&gt;
        ///            &lt;div class=&quot;header&quot;&gt;
        ///                &lt;h1 class=&quot;title&quot;&gt;Integral&lt;/h1&gt;
        ///            &lt;/div&gt;
        ///    
        ///            &lt;div class=&quot;body&quot;&gt;
        ///                &lt;div class=&quot;image-button btn-download-app&quot;&gt;
        ///                    &lt;img src=&quot;images/download-app.svg&quot; class=&quot;image-button-image&quot; alt=&quot;Download App&quot;&gt;
        ///           [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Homepage {
            get {
                return ResourceManager.GetString("Homepage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] Integral_Client {
            get {
                object obj = ResourceManager.GetObject("Integral_Client", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
