<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.C_T_R>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        <%ISE_TEST_V1.Models.C_T_R_Sup ct = new ISE_TEST_V1.Models.C_T_R_Sup();

          ISE_TEST_V1.Models.Test t = ct.get_test(Model.T_ID);
          ISE_TEST_V1.Models.Candidate c = ct.get_can(Model.C_ID); %>
        <div class="display-label">ID</div>
        <div class="display-field"><%: Model.ID %></div>
        
        <div class="display-label">Test Name</div>
        <div class="display-field"><%: t.Name %></div>
        
        <div class="display-label">Candidate Name</div>
        <div class="display-field"><%: c.Name %></div>
        
        
        
        <div class="display-label">Date_Isu</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.Date_Isu) %></div>
        
        <div class="display-label">Date_to_Set</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.Date_to_Set) %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Page/1") %>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>

