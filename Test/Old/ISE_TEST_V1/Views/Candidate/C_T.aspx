<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.C_T_ViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	C_T
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>C_T</h2>
    <p><%: Model.CID %></p>
    <p><%: Model.TID %></p>
    <p><%: Model.Time_Isu %></p>
     <p><%: Model.Time_to_set %></p>
      <p><%: Model.is_submited %></p>
</asp:Content>


