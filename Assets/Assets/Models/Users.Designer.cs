﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace Assets.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class AssetManagementNewEntities5 : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new AssetManagementNewEntities5 object using the connection string found in the 'AssetManagementNewEntities5' section of the application configuration file.
        /// </summary>
        public AssetManagementNewEntities5() : base("name=AssetManagementNewEntities5", "AssetManagementNewEntities5")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AssetManagementNewEntities5 object.
        /// </summary>
        public AssetManagementNewEntities5(string connectionString) : base(connectionString, "AssetManagementNewEntities5")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AssetManagementNewEntities5 object.
        /// </summary>
        public AssetManagementNewEntities5(EntityConnection connection) : base(connection, "AssetManagementNewEntities5")
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
        public ObjectSet<User> Users
        {
            get
            {
                if ((_Users == null))
                {
                    _Users = base.CreateObjectSet<User>("Users");
                }
                return _Users;
            }
        }
        private ObjectSet<User> _Users;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUsers(User user)
        {
            base.AddObject("Users", user);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="AssetManagementNewModel5", Name="User")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class User : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new User object.
        /// </summary>
        /// <param name="employeeID">Initial value of the EmployeeID property.</param>
        /// <param name="authorizedBy">Initial value of the AuthorizedBy property.</param>
        /// <param name="locationID">Initial value of the LocationID property.</param>
        /// <param name="ownerName">Initial value of the OwnerName property.</param>
        /// <param name="externalEmployee">Initial value of the ExternalEmployee property.</param>
        public static User CreateUser(global::System.String employeeID, global::System.String authorizedBy, global::System.String locationID, global::System.String ownerName, global::System.Boolean externalEmployee)
        {
            User user = new User();
            user.EmployeeID = employeeID;
            user.AuthorizedBy = authorizedBy;
            user.LocationID = locationID;
            user.OwnerName = ownerName;
            user.ExternalEmployee = externalEmployee;
            return user;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String EmployeeID
        {
            get
            {
                return _EmployeeID;
            }
            set
            {
                if (_EmployeeID != value)
                {
                    OnEmployeeIDChanging(value);
                    ReportPropertyChanging("EmployeeID");
                    _EmployeeID = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("EmployeeID");
                    OnEmployeeIDChanged();
                }
            }
        }
        private global::System.String _EmployeeID;
        partial void OnEmployeeIDChanging(global::System.String value);
        partial void OnEmployeeIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String AuthorizedBy
        {
            get
            {
                return _AuthorizedBy;
            }
            set
            {
                OnAuthorizedByChanging(value);
                ReportPropertyChanging("AuthorizedBy");
                _AuthorizedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AuthorizedBy");
                OnAuthorizedByChanged();
            }
        }
        private global::System.String _AuthorizedBy;
        partial void OnAuthorizedByChanging(global::System.String value);
        partial void OnAuthorizedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LocationID
        {
            get
            {
                return _LocationID;
            }
            set
            {
                OnLocationIDChanging(value);
                ReportPropertyChanging("LocationID");
                _LocationID = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LocationID");
                OnLocationIDChanged();
            }
        }
        private global::System.String _LocationID;
        partial void OnLocationIDChanging(global::System.String value);
        partial void OnLocationIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String OwnerName
        {
            get
            {
                return _OwnerName;
            }
            set
            {
                OnOwnerNameChanging(value);
                ReportPropertyChanging("OwnerName");
                _OwnerName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("OwnerName");
                OnOwnerNameChanged();
            }
        }
        private global::System.String _OwnerName;
        partial void OnOwnerNameChanging(global::System.String value);
        partial void OnOwnerNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean ExternalEmployee
        {
            get
            {
                return _ExternalEmployee;
            }
            set
            {
                OnExternalEmployeeChanging(value);
                ReportPropertyChanging("ExternalEmployee");
                _ExternalEmployee = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ExternalEmployee");
                OnExternalEmployeeChanged();
            }
        }
        private global::System.Boolean _ExternalEmployee;
        partial void OnExternalEmployeeChanging(global::System.Boolean value);
        partial void OnExternalEmployeeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        #endregion

    
    }

    #endregion

    
}
