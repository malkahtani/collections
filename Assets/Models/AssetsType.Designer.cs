﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace Assets.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class AssetsTypeEnt : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new AssetsTypeEnt object using the connection string found in the 'AssetsTypeEnt' section of the application configuration file.
        /// </summary>
        public AssetsTypeEnt() : base("name=AssetsTypeEnt", "AssetsTypeEnt")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AssetsTypeEnt object.
        /// </summary>
        public AssetsTypeEnt(string connectionString) : base(connectionString, "AssetsTypeEnt")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AssetsTypeEnt object.
        /// </summary>
        public AssetsTypeEnt(EntityConnection connection) : base(connection, "AssetsTypeEnt")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Asset_Type> Asset_Type
        {
            get
            {
                if ((_Asset_Type == null))
                {
                    _Asset_Type = base.CreateObjectSet<Asset_Type>("Asset_Type");
                }
                return _Asset_Type;
            }
        }
        private ObjectSet<Asset_Type> _Asset_Type;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Asset_Type EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAsset_Type(Asset_Type asset_Type)
        {
            base.AddObject("Asset_Type", asset_Type);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="AsstesType", Name="Asset_Type")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Asset_Type : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Asset_Type object.
        /// </summary>
        /// <param name="ass_Typ_ID">Initial value of the Ass_Typ_ID property.</param>
        /// <param name="ass_Typ">Initial value of the Ass_Typ property.</param>
        public static Asset_Type CreateAsset_Type(global::System.Int32 ass_Typ_ID, global::System.String ass_Typ)
        {
            Asset_Type asset_Type = new Asset_Type();
            asset_Type.Ass_Typ_ID = ass_Typ_ID;
            asset_Type.Ass_Typ = ass_Typ;
            return asset_Type;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Ass_Typ_ID
        {
            get
            {
                return _Ass_Typ_ID;
            }
            set
            {
                if (_Ass_Typ_ID != value)
                {
                    OnAss_Typ_IDChanging(value);
                    ReportPropertyChanging("Ass_Typ_ID");
                    _Ass_Typ_ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Ass_Typ_ID");
                    OnAss_Typ_IDChanged();
                }
            }
        }
        private global::System.Int32 _Ass_Typ_ID;
        partial void OnAss_Typ_IDChanging(global::System.Int32 value);
        partial void OnAss_Typ_IDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Ass_Typ
        {
            get
            {
                return _Ass_Typ;
            }
            set
            {
                OnAss_TypChanging(value);
                ReportPropertyChanging("Ass_Typ");
                _Ass_Typ = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Ass_Typ");
                OnAss_TypChanged();
            }
        }
        private global::System.String _Ass_Typ;
        partial void OnAss_TypChanging(global::System.String value);
        partial void OnAss_TypChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Ass_Typ_Des
        {
            get
            {
                return _Ass_Typ_Des;
            }
            set
            {
                OnAss_Typ_DesChanging(value);
                ReportPropertyChanging("Ass_Typ_Des");
                _Ass_Typ_Des = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Ass_Typ_Des");
                OnAss_Typ_DesChanged();
            }
        }
        private global::System.String _Ass_Typ_Des;
        partial void OnAss_Typ_DesChanging(global::System.String value);
        partial void OnAss_Typ_DesChanged();

        #endregion
    
    }

    #endregion
    
}
