<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.Test>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
                       
            <div class="editor-label">
                <div class="display-label">Test Name</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <div class="display-label">Test Description</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Description) %>
                <%: Html.ValidationMessageFor(model => model.Description) %>
            </div>
            
            <div class="editor-label">
               <div class="display-label">Test Duration in min</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Duration) %>
                <%: Html.ValidationMessageFor(model => model.Duration) %>
            </div>
            
            <div class="editor-label">
               <div class="display-label">Number of True and False Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_TF) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_TF) %>
            </div>
            
            <div class="editor-label">
               <div class="display-label">Number of multiple choice single answer Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_MCSA) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_MCSA) %>
            </div>
            
            <div class="editor-label">
                 <div class="display-label">Number of multiple choice multiple answers Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_MCMA) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_MCMA) %>
            </div>
            
            <div class="editor-label">
                 <div class="display-label">Number of Fill The Gap (String) Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_FGS) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_FGS) %>
            </div>
            
            <div class="editor-label">
                <div class="display-label">Number of Fill The Gap (Integer) Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_FGI) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_FGI) %>
            </div>
            
            <div class="editor-label">
                <div class="display-label">Number of Matching Table Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_MT) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_MT) %>
            </div>
            
            <div class="editor-label">
                <div class="display-label">Number of Short Answer Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_SA) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_SA) %>
            </div>
            
            <div class="editor-label">
                 <div class="display-label">Number of Essay Questions</div>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.N_Q_EA) %>
                <%: Html.ValidationMessageFor(model => model.N_Q_EA) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

