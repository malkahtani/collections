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
    public partial class Tests : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Tests object using the connection string found in the 'Tests' section of the application configuration file.
        /// </summary>
        public Tests() : base("name=Tests", "Tests")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Tests object.
        /// </summary>
        public Tests(string connectionString) : base(connectionString, "Tests")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Tests object.
        /// </summary>
        public Tests(EntityConnection connection) : base(connection, "Tests")
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
        public ObjectSet<Test> Tests
        {
            get
            {
                if ((_Tests == null))
                {
                    _Tests = base.CreateObjectSet<Test>("Tests");
                }
                return _Tests;
            }
        }
        private ObjectSet<Test> _Tests;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Tests EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTests(Test test)
        {
            base.AddObject("Tests", test);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ISE_TEST_1Model", Name="Test")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Test : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Test object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="duration">Initial value of the Duration property.</param>
        public static Test CreateTest(global::System.Int32 id, global::System.String name, global::System.Int16 duration)
        {
            Test test = new Test();
            test.ID = id;
            test.Name = name;
            test.Duration = duration;
            return test;
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
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
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
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                OnDurationChanging(value);
                ReportPropertyChanging("Duration");
                _Duration = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Duration");
                OnDurationChanged();
            }
        }
        private global::System.Int16 _Duration;
        partial void OnDurationChanging(global::System.Int16 value);
        partial void OnDurationChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_TF
        {
            get
            {
                return _N_Q_TF;
            }
            set
            {
                OnN_Q_TFChanging(value);
                ReportPropertyChanging("N_Q_TF");
                _N_Q_TF = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_TF");
                OnN_Q_TFChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_TF;
        partial void OnN_Q_TFChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_TFChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_MCSA
        {
            get
            {
                return _N_Q_MCSA;
            }
            set
            {
                OnN_Q_MCSAChanging(value);
                ReportPropertyChanging("N_Q_MCSA");
                _N_Q_MCSA = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_MCSA");
                OnN_Q_MCSAChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_MCSA;
        partial void OnN_Q_MCSAChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_MCSAChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_MCMA
        {
            get
            {
                return _N_Q_MCMA;
            }
            set
            {
                OnN_Q_MCMAChanging(value);
                ReportPropertyChanging("N_Q_MCMA");
                _N_Q_MCMA = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_MCMA");
                OnN_Q_MCMAChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_MCMA;
        partial void OnN_Q_MCMAChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_MCMAChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_FGS
        {
            get
            {
                return _N_Q_FGS;
            }
            set
            {
                OnN_Q_FGSChanging(value);
                ReportPropertyChanging("N_Q_FGS");
                _N_Q_FGS = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_FGS");
                OnN_Q_FGSChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_FGS;
        partial void OnN_Q_FGSChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_FGSChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_FGI
        {
            get
            {
                return _N_Q_FGI;
            }
            set
            {
                OnN_Q_FGIChanging(value);
                ReportPropertyChanging("N_Q_FGI");
                _N_Q_FGI = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_FGI");
                OnN_Q_FGIChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_FGI;
        partial void OnN_Q_FGIChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_FGIChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_MT
        {
            get
            {
                return _N_Q_MT;
            }
            set
            {
                OnN_Q_MTChanging(value);
                ReportPropertyChanging("N_Q_MT");
                _N_Q_MT = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_MT");
                OnN_Q_MTChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_MT;
        partial void OnN_Q_MTChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_MTChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_SA
        {
            get
            {
                return _N_Q_SA;
            }
            set
            {
                OnN_Q_SAChanging(value);
                ReportPropertyChanging("N_Q_SA");
                _N_Q_SA = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_SA");
                OnN_Q_SAChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_SA;
        partial void OnN_Q_SAChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_SAChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> N_Q_EA
        {
            get
            {
                return _N_Q_EA;
            }
            set
            {
                OnN_Q_EAChanging(value);
                ReportPropertyChanging("N_Q_EA");
                _N_Q_EA = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("N_Q_EA");
                OnN_Q_EAChanged();
            }
        }
        private Nullable<global::System.Int16> _N_Q_EA;
        partial void OnN_Q_EAChanging(Nullable<global::System.Int16> value);
        partial void OnN_Q_EAChanged();

        #endregion
    
    }

    #endregion
    
}
