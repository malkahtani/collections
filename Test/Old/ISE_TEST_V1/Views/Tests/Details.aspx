<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.Test>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">ID</div>
        <div class="display-field"><%: Model.ID %></div>
        
        <div class="display-label">Test Name</div>
        <div class="display-field"><%: Model.Name %></div>
        
        <div class="display-label">Test Description</div>
        <div class="display-field"><%: Model.Description %></div>
        
        <div class="display-label">Test Duration in min</div>
        <div class="display-field"><%: Model.Duration %></div>
        
        <div class="display-label">Number of True and False Questions</div>
        <div class="display-field"><%: Model.N_Q_TF %></div>
        
        <div class="display-label">Number of multiple choice single answer Questions</div>
        <div class="display-field"><%: Model.N_Q_MCSA %></div>
        
        <div class="display-label">Number of multiple choice multiple answers Questions</div>
        <div class="display-field"><%: Model.N_Q_MCMA %></div>
        
        <div class="display-label">Number of Fill The Gap (String) Questions</div>
        <div class="display-field"><%: Model.N_Q_FGS %></div>
        
        <div class="display-label">Number of Fill The Gap (Integer) Questions</div>
        <div class="display-field"><%: Model.N_Q_FGI %></div>
        
        <div class="display-label">Number of Matching Table Questions</div>
        <div class="display-field"><%: Model.N_Q_MT %></div>
        
        <div class="display-label">Number of Short Answer Questions</div>
        <div class="display-field"><%: Model.N_Q_SA %></div>
        
        <div class="display-label">Number of Essay Questions</div>
        <div class="display-field"><%: Model.N_Q_EA %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

