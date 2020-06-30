<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.Test>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% string m_ro = Model.m_role;
       string ro = Model.role;
        using (Html.BeginForm()) {%>
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
            <% string[] roles = System.Web.Security.Roles.GetRolesForUser(Page.User.Identity.Name);
               if (roles.Count() == 1){
                   string rr = roles[0];
               %>
           <%: Html.Hidden("role",rr)%>
           <%}
               else
               {
                   Array.Sort(roles);


                   int index_r = Array.IndexOf(roles, ro);
                   string temp_r;
                   temp_r = roles[0];
                   roles[0] = roles[index_r];
                   roles[index_r] = temp_r; 
                   %>
               <div class="editor-label">
                <div class="display-label">Select The Editor Role</div>
            </div>
           <%=Html.DropDownList("role", new SelectList(roles))%>
           
           <%} %>
           <div class="editor-label">
                <div class="display-label">Select The Marking Role</div>
                <% string[] m_roles = System.Web.Security.Roles.GetAllRoles();
                   Array.Sort(m_roles);
           
                    
                    int index = Array.IndexOf(m_roles,m_ro);
                   string temp;
                   temp = m_roles[0];
                   m_roles[0] = m_roles[index];
                   m_roles[index] = temp; 
                    
                    %>

           <%=Html.DropDownList("m_role", new SelectList(m_roles))%>
           <%: Html.Hidden("valid","false") %>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Page/1") %>
    </div>

</asp:Content>

