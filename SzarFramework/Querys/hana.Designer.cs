﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SzarFramework.Querys {
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
    internal class hana {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal hana() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SzarFramework.Querys.hana", typeof(hana).Assembly);
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
        ///   Looks up a localized string similar to SELECT  &quot;TableID&quot;
        ///      , &quot;FieldID&quot;
        ///      , &quot;AliasID&quot;
        ///      , &quot;Descr&quot;
        ///      , &quot;TypeID&quot;
        ///      , &quot;EditType&quot;
        ///      , &quot;SizeID&quot;
        ///      , &quot;EditSize&quot;
        ///      , &quot;Dflt&quot;
        ///      , &quot;NotNull&quot;
        ///      , &quot;IndexID&quot;
        ///      , &quot;RTable&quot;
        ///      , &quot;RField&quot;
        ///      , &quot;Action&quot;
        ///      , &quot;Sys&quot;
        ///      , &quot;DfltDate&quot;
        ///      , &quot;RelUDO&quot;
        ///  FROM CUFD.
        /// </summary>
        internal static string cufdList {
            get {
                return ResourceManager.GetString("cufdList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select &quot;fieldid&quot; from CUFD where &quot;AliasID&quot; = &apos;{0}&apos; and &quot;TableID&quot; = &apos;{1}&apos;.
        /// </summary>
        internal static string getFieldID {
            get {
                return ResourceManager.GetString("getFieldID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select 
        ///       &quot;TableName&quot;
        ///       ,&quot;Descr&quot;
        ///       ,&quot;TblNum&quot;
        ///       ,&quot;ObjectType&quot;
        ///       ,&quot;UsedInObj&quot;
        ///       ,&quot;LogTable&quot;
        ///       ,&quot;Archivable&quot;
        ///       ,&quot;ArchivDate&quot; 
        ///from OUTB.
        /// </summary>
        internal static string outbList {
            get {
                return ResourceManager.GetString("outbList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select &quot;Code&quot;, &quot;NewFormSrf&quot; from OUDO where &quot;Code&quot; = &apos;{0}&apos;.
        /// </summary>
        internal static string udoList {
            get {
                return ResourceManager.GetString("udoList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT &quot;TableID&quot;
        ///      ,&quot;FieldID&quot;
        ///      ,&quot;IndexID&quot;
        ///      ,&quot;FldValue&quot;
        ///      ,&quot;Descr&quot;
        ///      ,&quot;FldDate&quot;
        ///  FROM UFD1 
        ///  where &quot;tableid&quot; = &apos;{0}&apos; 
        ///        and &quot;fieldid&quot; = (select &quot;FieldID&quot; from CUFD where &quot;TableID&quot; = &apos;{0}&apos; and &quot;AliasID&quot; = &apos;{1}&apos;).
        /// </summary>
        internal static string ufd1List {
            get {
                return ResourceManager.GetString("ufd1List", resourceCulture);
            }
        }
    }
}
