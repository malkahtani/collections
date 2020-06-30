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

namespace ISE_TEST_V1.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TestE_Role_Entities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TestE_Role_Entities object using the connection string found in the 'TestE_Role_Entities' section of the application configuration file.
        /// </summary>
        public TestE_Role_Entities() : base("name=TestE_Role_Entities", "TestE_Role_Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TestE_Role_Entities object.
        /// </summary>
        public TestE_Role_Entities(string connectionString) : base(connectionString, "TestE_Role_Entities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TestE_Role_Entities object.
        /// </summary>
        public TestE_Role_Entities(EntityConnection connection) : base(connection, "TestE_Role_Entities")
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
        public ObjectSet<TestE_Role> TestE_Role
        {
            get
            {
                if ((_TestE_Role == null))
                {
                    _TestE_Role = base.CreateObjectSet<TestE_Role>("TestE_Role");
                }
                return _TestE_Role;
            }
        }
        private ObjectSet<TestE_Role> _TestE_Role;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the TestE_Role EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTestE_Role(TestE_Role testE_Role)
        {
            base.AddObject("TestE_Role", testE_Role);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ISE_TEST_1Model12", Name="TestE_Role")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class TestE_Role : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new TestE_Role object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="test_ID">Initial value of the Test_ID property.</param>
        /// <param name="roleName">Initial value of the RoleName property.</param>
        public static TestE_Role CreateTestE_Role(global::System.Int32 id, global::System.Int32 test_ID, global::System.String roleName)
        {
            TestE_Role testE_Role = new TestE_Role();
            testE_Role.ID = id;
            testE_Role.Test_ID = test_ID;
            testE_Role.RoleName = roleName;
            return testE_Role;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Test_ID
        {
            get
            {
                return _Test_ID;
            }
            set
            {
                OnTest_IDChanging(value);
                ReportPropertyChanging("Test_ID");
                _Test_ID = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Test_ID");
                OnTest_IDChanged();
            }
        }
        private global::System.Int32 _Test_ID;
        partial void OnTest_IDChanging(global::System.Int32 value);
        partial void OnTest_IDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                OnRoleNameChanging(value);
                ReportPropertyChanging("RoleName");
                _RoleName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("RoleName");
                OnRoleNameChanged();
            }
        }
        private global::System.String _RoleName;
        partial void OnRoleNameChanging(global::System.String value);
        partial void OnRoleNameChanged();

        #endregion
    
    }

    #endregion
    
}
